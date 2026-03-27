using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using static ATS_Injector.Helper;

namespace ATS_Injector
{
    internal class GeminiAPI
    {
        private string ApiKey;
        private const string ModelId = "gemini-3-flash-preview";

        public GeminiAPI(string ApiKey)
        {
            this.ApiKey = ApiKey;
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
                        using (var response = await MyHttpClient.Instance.PostAsync(Url, content, cts.Token))
                        {
                            string responseBody = await response.Content.ReadAsStringAsync(cts.Token);
                            if (response.IsSuccessStatusCode)
                            {
                                using var doc = JsonDocument.Parse(responseBody);

                                // Navigate the JSON tree to find the text response
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
                        returnStr = "The request timed out.";
                        Console.WriteLine("Task was cancelled due to timeout.");
                    }
                    catch (HttpRequestException e)
                    {
                        Console.WriteLine($"Request exception: {e.Message}");
                        returnStr = e.Message;
                    }
                }
            }
                
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
