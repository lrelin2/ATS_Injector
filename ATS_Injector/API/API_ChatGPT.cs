using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ATS_Injector.API
{
    internal class API_ChatGPT
    {
        private string ApiKey;
        private const string ModelId = "gpt-5.4-mini"; //"gpt-5-mini";
        private static HttpClient _httpClient = null;
        private static readonly string url = "https://api.openai.com/v1/chat/completions";
        private static readonly string Error503 = "\"message\": \"This model is currently experiencing high demand. Spikes in demand are usually temporary. Please try again later.\",";
        private static readonly string Error429 = "\"message\": \"You exceeded your current quota, please check your plan and billing details. For more information on this error, head to: https://ai.google.dev/gemini-api/docs/rate-limits. To monitor your current usage, head to:";


        public API_ChatGPT(string ApiKey)
        {
            this.ApiKey = ApiKey;
            if (_httpClient == null)
            {
                _httpClient = new HttpClient { BaseAddress = new Uri(url) };
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ApiKey);
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/121.0.0.0 Safari/537.36");
                _httpClient.Timeout = TimeSpan.FromSeconds(30);
            }
        }

        public async Task<string> SendPrompt(string userPrompt)
        {

            int timeOut = 30;
            string returnStr = string.Empty;

            var payload = new OpenAIRequest
            {
                model = ModelId,
                store = true,
                messages = new List<MessageOpenAI>
                    {
                        new MessageOpenAI { role = "user", content = userPrompt }
                    }
            };

            using (var cts = new CancellationTokenSource(TimeSpan.FromSeconds(timeOut)))
            {
                try
                {

                    using (var request = new HttpRequestMessage(HttpMethod.Post, url))
                    {
                        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", this.ApiKey);
                        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        request.Content = JsonContent.Create(payload);

                        var response = await _httpClient.SendAsync(request, cts.Token);

                        if (response.IsSuccessStatusCode)
                        {
                            string responseBody = await response.Content.ReadAsStringAsync(cts.Token);
                            using (var doc = JsonDocument.Parse(responseBody))
                            {
                                var root = doc.RootElement;

                                // Check for the standard successful response path
                                if (root.TryGetProperty("choices", out var choices) && choices.GetArrayLength() > 0)
                                {
                                    returnStr = choices[0]
                                        .GetProperty("message")
                                        .GetProperty("content")
                                        .GetString() ?? "No content found in the message.";
                                }
                                // Check for the error block specifically (to avoid getting a 'property not found' exception)
                                else if (root.TryGetProperty("error", out var error))
                                {
                                    returnStr = $"API Error: {error.GetProperty("message").GetString()}";
                                }
                                else
                                {
                                    returnStr = "Unexpected JSON structure received.";
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Error: {response.StatusCode}");
                            var errorBody = await response.Content.ReadAsStringAsync(cts.Token);
                            Console.WriteLine(errorBody);
                            returnStr = errorBody.ToString();
                        }

                    }
                }
                catch (OperationCanceledException)
                {
                    returnStr = Helper.TimeOutErrorMsg;
                    Console.WriteLine("Task was cancelled due to timeout.");
                }
                catch (HttpRequestException e)
                {
                    //Check for 503, pretty common service is down error message...
                    Console.WriteLine($"Request exception: {e.Message}");
                    if (e.StatusCode.Value.Equals(503))
                    {
                        returnStr = Helper.Http503;
                    }
                    else
                    {
                        returnStr = e.Message;
                    }
                }
            }
            if (returnStr.Contains(Error503))
                returnStr = Helper.Http503;
            else if (returnStr.Contains(Error429))
                returnStr = Helper.Http429;
            return returnStr;

        }
    }

    public class OpenAIRequest
    {
        public string model { get; set; } = "gpt-5.4-mini";
        public List<MessageOpenAI> messages { get; set; } = new();

        public bool store { get; set; }
    }

    public class MessageOpenAI
    {
        public string role { get; set; } = "user";
        public string content { get; set; } = string.Empty;
    }
}
