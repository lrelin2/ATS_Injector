using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using static ATS_Injector.Helper;

namespace ATS_Injector
{
    internal class OpenRouter_Calude
    {
        private string ApiKey;
        private readonly HttpClient _httpClient;
        private static readonly string Error503 = "\"message\": \"This model is currently experiencing high demand. Spikes in demand are usually temporary. Please try again later.\",";

        public OpenRouter_Calude(string ApiKey)
        {
            this.ApiKey = ApiKey;
            _httpClient = MyHttpClient.Instance;
            _httpClient.BaseAddress = new Uri("https://openrouter.ai/api/v1/");
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {ApiKey}");
            // OpenRouter likes these for rankings
            _httpClient.DefaultRequestHeaders.Add("HTTP-Referer", "https://your-project-url.com");
            _httpClient.DefaultRequestHeaders.Add("X-Title", "ATS_Injector");
        }

        public async Task<string> SendPrompt(string userPrompt)
        {
            int timeOut = 30;
            var request = new OpenRouterRequest();
            string returnStr = string.Empty;
            request.messages.Add(new Message { role = "user", content = userPrompt });

            using (var cts = new CancellationTokenSource(TimeSpan.FromSeconds(timeOut)))
            {
                try
                {
                    using (var response = await _httpClient.PostAsJsonAsync("chat/completions", request, cts.Token))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var result = await response.Content.ReadFromJsonAsync<OpenRouterResponse>();
                            returnStr = result?.choices.FirstOrDefault()?.message.content ?? "No response.";
                        }
                        else
                        {
                            string responseBody = await response.Content.ReadAsStringAsync(cts.Token);
                            returnStr = $"Error ({response.StatusCode}): {responseBody}";
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
            return returnStr;
        }
    }

    public class OpenRouterRequest
    {
        public string model { get; set; } = "openrouter/free";
        public List<Message> messages { get; set; } = new();
        public double temperature { get; set; } = 0.7;
    }

    public class Message
    {
        public string role { get; set; } // "user" or "assistant"
        public string content { get; set; }
    }

    public class OpenRouterResponse
    {
        public List<Choice> choices { get; set; } = new();
    }

    public class Choice
    {
        public Message message { get; set; } = new();
    }
}