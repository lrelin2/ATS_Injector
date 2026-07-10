using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ATSInjector.API
{
    internal class API_Twitter
    {
        private string ApiKey;
        private static HttpClient _httpClient = null;
        private static readonly string Error503 = "\"message\": \"This model is currently experiencing high demand. Spikes in demand are usually temporary. Please try again later.\",";

        public API_Twitter(string ApiKey)
        {
            this.ApiKey = ApiKey;
            if(_httpClient == null)
            {
                _httpClient = new HttpClient { BaseAddress = new Uri("https://api.x.ai/v1/") };
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {ApiKey}");
                _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/121.0.0.0 Safari/537.36");
                _httpClient.Timeout = TimeSpan.FromSeconds(30);
            }
        }

        public async Task<string> SendPrompt(string userPrompt)
        {
            int timeout = 30;

            var request = new TwitterRequest();
            request.messages = new List<TwitterMessage>();
            request.messages.Add(new TwitterMessage
            {
                role = "user",
                content = userPrompt
            });

            string returnStr = "";

            using (var cts = new CancellationTokenSource(TimeSpan.FromSeconds(timeout)))
            {
                HttpResponseMessage response = null;

                try
                {
                    using (response = await _httpClient.PostAsJsonAsync(
                        "chat/completions",
                        request,
                        cts.Token))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var result = await response.Content.ReadFromJsonAsync<GrokResponse>();

                            returnStr = result?.choices?
                                              .FirstOrDefault()?
                                              .message?
                                              .content
                                         ?? "No response.";
                        }
                        else
                        {
                            string body = await response.Content.ReadAsStringAsync();
                            returnStr = $"Error ({response.StatusCode}): {body}";
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Request timed out.");
                    returnStr = Helper.TimeOutErrorMsg;
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine(ex.Message);

                    if (response != null &&
                        (int)response.StatusCode == 503)
                    {
                        return Helper.Http503;
                    }

                    returnStr = ex.Message;
                }
            }

            return returnStr;
        }
    }

    public class TwitterRequest
    {
        public string model { get; set; } = "grok-4";

        public List<TwitterMessage> messages { get; set; } = new List<TwitterMessage>();

        public double temperature { get; set; } = 0.7;
    }

    public class TwitterMessage
    {
        public string role { get; set; }

        public string content { get; set; }
    }

    public class GrokResponse
    {
        public List<TwitterChoice> choices { get; set; }
    }

    public class TwitterChoice
    {
        public TwitterMessage message { get; set; }
    }
}