using System.ComponentModel;
using System.Security.Policy;
using static ATS_Injector.PopOutApp;

namespace ATS_Injector;

public partial class Form1 : Form
{
    private string JD_Results = string.Empty;

    public Form1()
    {
        InitializeComponent();

        bool atLeastOneToken = CheckAPI_Tokens();
        if (atLeastOneToken)
        {
            History.InitGrid(dataGridView1);
        }
    }

    private bool CheckAPI_Tokens()
    {
        bool atLeastOneEnabled = false;
        if (API_Token_Written(API_AI_ID.ChatGPT) == false)
        {
            ChatGPT_rdbtn.Checked = false;
            ChatGPT_rdbtn.Enabled = false;
        }
        else if (atLeastOneEnabled == false)
        {
            atLeastOneEnabled = true;
            ChatGPT_rdbtn.Checked = true;
        }

        if (API_Token_Written(API_AI_ID.Gemini) == false)
        {
            Gemini_rdbtn.Checked = false;
            Gemini_rdbtn.Enabled = false;
        }
        else if (atLeastOneEnabled == false)
        {
            atLeastOneEnabled = true;
            Gemini_rdbtn.Checked = true;
        }

        if (API_Token_Written(API_AI_ID.Claude) == false)
        {
            Claude_rdbtn.Checked = false;
            Claude_rdbtn.Enabled = false;
        }
        else if (atLeastOneEnabled == false)
        {
            atLeastOneEnabled = true;
            Claude_rdbtn.Checked = true;
        }

        return atLeastOneEnabled;
    }

    private void ProcessJD_btn_Click(object sender, EventArgs e)
    {
        //When clicked, you need to query the url, and then pass it over to
        //Gemini for now, add more options later
        string url = JobUrl_txt.Text.Trim();
        if(Helper.RemoveLoginJunk(url,out string newurl, out string complainMessage))
        {
            //if here, then url was modified, let the user know.
            DialogResult result = MessageBox.Show($"Change Url from {Environment.NewLine}[{url}] {Environment.NewLine}into {Environment.NewLine}[{newurl}] ?", "Personalised url removal", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // User clicked Yes
                url = newurl;
            }
        }
        JD_Results = string.Empty;
        FeedbackArea_txt.Text = "Checking if JD website is valid, timeout is upto 10 seconds\r\n";

        bool isJD_source_available = Task.Run(async () =>
        {

            // This waits for the async method to finish on the background thread
            bool result = await Helper.CheckWebsiteAsync(url);

            return result;
        }).GetAwaiter().GetResult();

        if (isJD_source_available)
        {
            if (GetAPI_Token(API_AI_ID.Gemini, out string Token, out string errorMsg))
            {
                //at least we know token is written, pass it to the API thing now
                GeminiAPI API = new GeminiAPI(Token);
                ProcessJD_btn.Enabled = false;
                //string prompt = "Explain DO-178C compliance levels.";
                FeedbackArea_txt.AppendText($"Getting {API_AI_ID.Gemini} to download the Jod Description from link provided.\r\n");
                string prompt = $"Get the Job Description from this url:{url}.";

                // This runs the task on a ThreadPool thread and waits for the result
                string result = Task.Run(async () => await API.SendPrompt(prompt))
                                    .GetAwaiter()
                                    .GetResult();
                JD_Results = result;
                ProcessJD_btn.Enabled = true;
                CreatePDF_btn.Enabled = true;   
                FeedbackArea_txt.AppendText($"Does this JD look correct? \r\n[{result}\r\n]\r\n");
            }
            else if (string.IsNullOrEmpty(errorMsg) == false)
            {
                FeedbackArea_txt.Text = $"An error occured retrieving your {API_AI_ID.Gemini} API token. Error stack:[{errorMsg}]";
            }
        }
        else
        {
            FeedbackArea_txt.AppendText("URL provided is not valid and/or down.\r\n");
        }
    }

    private void AddChatGPTToken_btn_Click(object sender, EventArgs e)
    {
        PopOutBox_automated(API_AI_ID.ChatGPT);
    }

    private void AddGeminiToken_btn_Click(object sender, EventArgs e)
    {
        PopOutBox_automated(API_AI_ID.Gemini);
    }

    private void CreatePDF_btn_Click(object sender, EventArgs e)
    {
        //When here, this means Job Description has been extracted.
        //Now go and make Gemini get the job talking points.
        if (GetAPI_Token(API_AI_ID.Gemini, out string Token, out string errorMsg))
        {
            //at least we know token is written, pass it to the API thing now
            GeminiAPI API = new GeminiAPI(Token);
            ProcessJD_btn.Enabled = false;
            //string prompt = "Explain DO-178C compliance levels.";
            FeedbackArea_txt.Text = $"Getting {API_AI_ID.Gemini} to create your experiance for the job.\r\n";
            string prompt = $"Create a list of achivements, experiance, roles that " +
                $"you would see in a qualified resume that match all relevant information about " +
                $"job duites, requirements and desired experiance from " +
                $"the following job description scrape:{JD_Results}. Do Not include your thinking, or process, just a stream of bullet points.";

            // This runs the task on a ThreadPool thread and waits for the result
            string result = Task.Run(async () => await API.SendPrompt(prompt))
                                .GetAwaiter()
                                .GetResult();
            JD_Results = result;
            ProcessJD_btn.Enabled = true;
            CreatePDF_btn.Enabled = true;
            FeedbackArea_txt.AppendText($"Does this JD look correct? \r\n[{result}\r\n]\r\n");
        }
        else if (string.IsNullOrEmpty(errorMsg) == false)
        {
            FeedbackArea_txt.Text = $"An error occured retrieving your {API_AI_ID.Gemini} API token. Error stack:[{errorMsg}]";
        }

    }
}
