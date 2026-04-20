using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace ATS_Injector
{
    internal class Helper
    {
        public static readonly string AI_ATS_Question =
            "Using the Job description below, create a resume that has all the needed experiance, knowledge, and understanding.\r\n" +
            "Add needed soft skill, and hard skill that are used for the tools, technology, and required experience.\r\n" +
            "Create only the Professional Summary, techninical skills, professional experiance and certification sections.\r\n" +
            "in the mentioned sections above, create at least 50 sentences all togthere, with bullet points at the start of each sentence, where all of the skills, tools, framework, technology, experiance level, demonstation, and capabilities are used.\r\n" +
            "In each sentence, do not include any stars or bullet points or the symbol '*'\r\n at any point of time. Do not include tabs or other escape characters." +
            "From that resume remove everything that is not relevant to what an ATS scanner would look for.\r\n" +
            "Make the resume education, experiance, and knowledge look real, do not add things in parantheses, or say in related field or related technology or the like.\r\n" +
            "When creating bullet points, use only the '*' character.\r\n" +
            "\r\n" +
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

        public class DuplicateMatchForm : Form
        {
            private RichTextBox rtbLeft;
            private RichTextBox rtbRight;

            public DuplicateMatchForm(string[] leftContent, string[] rightContent)
            {
                InitializeComponent(leftContent, rightContent);

                // Highlight common lines
                string[] commonLines = leftContent.Intersect(rightContent, StringComparer.OrdinalIgnoreCase).ToArray();
                HighlightKeywords(rtbLeft, commonLines);
                HighlightKeywords(rtbRight, commonLines);
            }

            private void HighlightKeywords(RichTextBox rtb, string[] words)
            {
                foreach (string word in words)
                {
                    if (string.IsNullOrWhiteSpace(word)) continue;
                    int startIndex = 0;
                    while (startIndex < rtb.TextLength)
                    {
                        int wordPos = rtb.Find(word, startIndex, RichTextBoxFinds.None);
                        if (wordPos != -1)
                        {
                            rtb.SelectionStart = wordPos;
                            rtb.SelectionLength = word.Length;
                            rtb.SelectionBackColor = Color.Yellow;
                            startIndex = wordPos + word.Length;
                        }
                        else break;
                    }
                }
                rtb.SelectionStart = 0;
                rtb.SelectionLength = 0;
            }

            private void InitializeComponent(string[] leftContent, string[] rightContent)
            {
                // 1. Form Settings
                this.Text = "Duplicate job description";
                this.MinimumSize = new Size(800, 600); // Prevent window from getting too small
                this.Size = new Size(1280, 950);
                this.StartPosition = FormStartPosition.CenterScreen;
                this.Padding = new Padding(15); // Overall outer spacing

                string labelTxt = "Found previously entered Job Description. Left was previously pasted, right is current job description. Yellow is duplicate(s) found.\r\n" +
    "Select OK if not a duplicate, continue with ATS injection. Select Cancel to not inject anything into PDF.";

                // 2. Header Label (Stays at the top)
                Label headerLabel = new Label()
                {
                    Text = labelTxt,
                    Dock = DockStyle.Top,
                    Height = 60,
                    Font = new Font("Segoe UI", 11, FontStyle.Bold),
                    TextAlign = ContentAlignment.MiddleLeft
                };

                // 3. Layout Table (Handles the 50/50 split and resizing)
                TableLayoutPanel tlpCenter = new TableLayoutPanel()
                {
                    Dock = DockStyle.Fill,
                    ColumnCount = 2,
                    RowCount = 1,
                    Padding = new Padding(0, 10, 0, 10)
                };
                tlpCenter.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
                tlpCenter.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));

                rtbLeft = new RichTextBox { Dock = DockStyle.Fill, Text = string.Join(Environment.NewLine, leftContent), Margin = new Padding(0, 0, 10, 0) };
                rtbRight = new RichTextBox { Dock = DockStyle.Fill, Text = string.Join(Environment.NewLine, rightContent), Margin = new Padding(10, 0, 0, 0) };

                tlpCenter.Controls.Add(rtbLeft, 0, 0);
                tlpCenter.Controls.Add(rtbRight, 1, 0);

                // 4. Button Panel (Keeps buttons on bottom right)
                FlowLayoutPanel buttonPanel = new FlowLayoutPanel()
                {
                    Dock = DockStyle.Bottom,
                    FlowDirection = FlowDirection.RightToLeft, // Buttons start from right side
                    Height = 50,
                    Padding = new Padding(0, 5, 0, 0)
                };

                Button btnCancel = new Button()
                {
                    Text = "Cancel, already applied",
                    DialogResult = DialogResult.Cancel,
                    Size = new Size(200, 40),
                    Margin = new Padding(5)
                };

                Button btnOk = new Button()
                {
                    Text = "New Job Description",
                    DialogResult = DialogResult.OK,
                    Size = new Size(200, 40),
                    Margin = new Padding(5)
                };

                buttonPanel.Controls.Add(btnCancel); // Added first because of RightToLeft
                buttonPanel.Controls.Add(btnOk);

                // 5. Add to Form (Order matters for DockStyle.Fill)
                this.Controls.Add(tlpCenter);
                this.Controls.Add(headerLabel);
                this.Controls.Add(buttonPanel);

                this.AcceptButton = btnOk;
                this.CancelButton = btnCancel;
            }
        }

    }
}
