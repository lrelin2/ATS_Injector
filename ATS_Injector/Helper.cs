using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Text;
using System.Web;

namespace ATS_Injector
{
    internal class Helper
    {
        public sealed class MyHttpClient
        {
            // The backing field
            private static readonly HttpClient _instance;

            // The static constructor runs exactly once, the first time the class is accessed
            static MyHttpClient()
            {
                _instance = new HttpClient();
                _instance.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/121.0.0.0 Safari/537.36");
                _instance.Timeout = TimeSpan.FromSeconds(30);
            }

            // Public property to access the client
            public static HttpClient Instance => _instance;

            // Private constructor to prevent "new MyHttpClient()"
            private MyHttpClient() { }
        }


        public static async Task<bool> CheckWebsiteAsync(string url)
        {
            bool retunringBool = false;
            if (IsValidUrl(url))
            {

                // Create a cancellation token that fires after your dynamic duration
                using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));
                try
                {
                    // Pass the token into the GetAsync method
                    HttpResponseMessage response = await MyHttpClient.Instance.GetAsync(url, cts.Token);
                    retunringBool = response.IsSuccessStatusCode;
                }
                catch (HttpRequestException)
                {
                    // Catches DNS issues, connection timeouts, or unreachable servers
                    retunringBool = false;
                }
                catch (Exception)
                {
                    // Catches other potential errors (e.g., malformed URI)
                    retunringBool = false;
                }
            }
            return retunringBool;
        }


        private static bool IsValidUrl(string url)
        {
            // Check if the string is null or empty first
            if (string.IsNullOrWhiteSpace(url)) return false;

            // TryCreate attempts to parse the string into a Uri object.
            // UriKind.Absolute ensures it has a scheme (like http:// or https://).
            return Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult)
                   && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }

        internal static bool RemoveLoginJunk(string url, out string newurl, out string complainMessage)
        {
            newurl = url;
            complainMessage = string.Empty;
            bool retrungingBool = false;
            //AI has a hard time figureing stuff out
            //Need a one liner to convert things from
            //https://www.linkedin.com/jobs/collections/recommended/?currentJobId=4359058081
            //into
            //https://www.linkedin.com/jobs/view/4359058081
            //as well as others

            if(string.IsNullOrEmpty(url) == false)
            {
                try
                {

                    // Uri.TryCreate handles malformed URLs gracefully
                    if (Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult))
                    {
                        // Normalize the host to lowercase to handle case-insensitivity
                        string host = uriResult.Host.ToLowerInvariant();

                        if(host.Contains("linkedin.com"))
                        {
                            if (url.ToLower().Contains(@"collections/recommended/"))
                            {
                                
                                // Parse the query string (e.g., ?currentJobId=4359058081)
                                var queryParams = HttpUtility.ParseQueryString(uriResult.Query);
                                string jobId = queryParams["currentJobId"];

                                if (!string.IsNullOrEmpty(jobId))
                                {
                                    // Reconstruct the "view" URL
                                    newurl = $"https://www.linkedin.com/jobs/view/{jobId}";
                                    retrungingBool = true;
                                }
                            }
                        }
                    }
                }
                catch (UriFormatException)
                {
                    // URL format is invalid
                }
            }

            return retrungingBool;
        }
    }
}
