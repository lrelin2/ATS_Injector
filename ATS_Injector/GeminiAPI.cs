using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using static ATS_Injector.Helper;

namespace ATS_Injector
{
    internal class GeminiAPI
    {
        private string ApiKey;
        private const string ModelId = "gemini-3-flash-preview";
        private static HttpClient _httpClient = null;
        private static readonly string Error503 = "\"message\": \"This model is currently experiencing high demand. Spikes in demand are usually temporary. Please try again later.\",";
        private static readonly string Error429 = "\"message\": \"You exceeded your current quota, please check your plan and billing details. For more information on this error, head to: https://ai.google.dev/gemini-api/docs/rate-limits. To monitor your current usage, head to:";

        //
        public GeminiAPI(string ApiKey)
        {
            this.ApiKey = ApiKey;
            //if (_httpClient == null)
            //{
            //    _httpClient = new HttpClient { BaseAddress = new Uri("https://openrouter.ai/api/v1/") };
            //    _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {ApiKey}");
            //    // OpenRouter likes these for rankings
            //    _httpClient.DefaultRequestHeaders.Add("HTTP-Referer", "https://your-project-url.com");
            //    _httpClient.DefaultRequestHeaders.Add("X-Title", "ATS_Injector");
            //    //_httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/121.0.0.0 Safari/537.36");
            //    _httpClient.Timeout = TimeSpan.FromSeconds(30);
            //}
            if(_httpClient == null)
            {
                _httpClient = new HttpClient();
                _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/121.0.0.0 Safari/537.36");
                _httpClient.Timeout = TimeSpan.FromSeconds(30);
            }
        }

        public async Task<string> SendPrompt(string userPrompt)
        {
            string Url = $"https://generativelanguage.googleapis.com/v1beta/models/{ModelId}:generateContent?key={ApiKey}";
            int timeOut = 30;
            // using var client = Helper.MyHttpClient.Instance;
            string returnStr = string.Empty;
            var payload = new
            {
                contents = new[] { new { parts = new[] { new { text = userPrompt } } } }
            };

            string jsonPayload = JsonSerializer.Serialize(payload);

            using (var cts = new CancellationTokenSource(TimeSpan.FromSeconds(timeOut)))
            {
                using (var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json"))
                {
                    try
                    {
                        using (var response = await _httpClient.PostAsync(Url, content, cts.Token))
                        {
                            string responseBody = await response.Content.ReadAsStringAsync(cts.Token);
                            if (response.IsSuccessStatusCode)
                            {
                                using var doc = JsonDocument.Parse(responseBody);
                                var root = doc.RootElement;
                                if (root.TryGetProperty("candidates", out var candidates) && candidates.GetArrayLength() > 0)
                                {
                                    returnStr = candidates[0]
                                        .GetProperty("content")
                                        .GetProperty("parts")[0]
                                        .GetProperty("text")
                                        .GetString() ?? "No text found in response.";
                                }
                            }
                            else
                            {
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
            }
            if(returnStr.Contains(Error503))
                returnStr = Helper.Http503;
            else if (returnStr.Contains(Error429))
                returnStr = Helper.Http429;
            return returnStr;
        }
    }

    public class ModelGemini
    {
        public static List<ModelGemini> Models { get; }
        public int ID { get; set; }
        public required string ModelName { get; set; }
        public int RateLimit { get; set; }
        public int RateLimitDaily { get; set; }

        static ModelGemini()
        {
            Models = new List<ModelGemini>
            {
                new ModelGemini { ID = 1, ModelName = "Gemini 2.5 Pro", RateLimit = 5, RateLimitDaily = 100 },
                new ModelGemini { ID = 2, ModelName = "Gemini 2.5 Flash", RateLimit = 10, RateLimitDaily = 250 },
                new ModelGemini { ID = 3, ModelName = "Gemini 2.5 Flash-Lite", RateLimit = 15, RateLimitDaily = 1000 }
            };
        }
    }
}