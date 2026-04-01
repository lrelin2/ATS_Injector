using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ATS_Injector
{
    internal class Helper
    {
        public static readonly string AI_ATS_Question =
            "Using the Job description below, create a resume that has all the needed experiance, knowledge, and technology used." +
            "Add at needed soft skill, and hard skill that are used for the tools, technology, and required experience." +
            "From that resume remove everything that is not relevant to what an ATS scanner would look for." +
            "Only provide the list of keywords and bullet points, without any explanation or additional information. When creating bullet points, use only the '*' character." +
            "The Job Descripion:";

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

        public class FeedBackHelper
        {
            // The backing field
#pragma warning disable CS8625 //Instance object will be initalized, otherwise the whole thing fails in spectacular fashion. 
            private static FeedBackHelper _instance = null;
#pragma warning restore CS8625
            private RichTextBox _FeedbackArea_txt;
            private RichTextBox _ManualJDPaste_txt;
            private RichTextBox _ATS_Injection_txt;

            public static void InitalizeFeedBackHelper(RichTextBox FeedbackArea_txt, RichTextBox ManualJDPaste_txt, RichTextBox ATS_Injection_txt)
            {
                _instance = new FeedBackHelper(FeedbackArea_txt, ManualJDPaste_txt, ATS_Injection_txt);
            }

            // The static constructor runs exactly once, the first time the class is accessed
            public FeedBackHelper(RichTextBox FeedbackArea_txt, RichTextBox ManualJDPaste_txt, RichTextBox ATS_Injection_txt)
            {
                _FeedbackArea_txt = FeedbackArea_txt;
                _ManualJDPaste_txt = ManualJDPaste_txt;
                _ATS_Injection_txt = ATS_Injection_txt;
            }

            public static void SetFeedback(string msg)
            {
                _instance._FeedbackArea_txt.Invoke((Action)delegate
                { _instance._FeedbackArea_txt.Text = msg; });
            }
            public static void SetdManualJDPaste(string msg)
            {
                _instance._ManualJDPaste_txt.Invoke((Action)delegate
                { _instance._ManualJDPaste_txt.Text = msg; });
            }
            public static void SetATS_Injection(string msg)
            {
                _instance._ATS_Injection_txt.Invoke((Action)delegate
                { _instance._ATS_Injection_txt.Text = msg; });
            }

            public static void AppendFeedback(string msg)
            {
                _instance._FeedbackArea_txt.Invoke((Action)delegate
                { _instance._FeedbackArea_txt.AppendText($"\r\n{msg}"); });
            }
            public static void AppendManualJDPaste(string msg)
            {
                _instance._ManualJDPaste_txt.Invoke((Action)delegate
                { _instance._ManualJDPaste_txt.AppendText($"\r\n{msg}"); });
            }
            public static void AppendATS_Injection(string msg)
            {
                _instance._ATS_Injection_txt.Invoke((Action)delegate
                { _instance._ATS_Injection_txt.AppendText($"\r\n{msg}"); });
            }

            public static string GetTextFeedback()
            {
                return (string)_instance._FeedbackArea_txt.Invoke((Func<string>)delegate
                { return _instance._FeedbackArea_txt.Text; });
            }
            public static string GetTextManualJDPaste()
            {
                return (string)_instance._ManualJDPaste_txt.Invoke((Func<string>)delegate
                { return _instance._ManualJDPaste_txt.Text; });
            }
            public static string GetTextATS_Injection()
            {
                return (string)_instance._ATS_Injection_txt.Invoke((Func<string>)delegate
                { return _instance._ATS_Injection_txt.Text; });
            }
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

        internal static void _debug_RichTextArea(RichTextBox textArea)
        {
            //create one liner help that will save the data to a flat file
            //then load it back up and such...
            string _filePath = Path.Combine(PopOutApp.GetATSFolder(), "debugPrevRichText.txt");

            if (textArea.Text.Length <= 5)
            {
                //empty text area, load previous stuff
                if (File.Exists(_filePath))
                {
                    textArea.Text = File.ReadAllText(_filePath, Encoding.UTF8);
                }
            }
            else
            {
                File.WriteAllText(_filePath, textArea.Text, Encoding.UTF8);
            }

        }
    }
}
