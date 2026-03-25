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
        //TODO
        //Initiail step, you MUST initialize the helperfeeback thing
        Helper.FeedBackHelper.InitalizeFeedBackHelper(FeedbackArea_txt, ManualJDPaste_txt, ATS_Injection_txt);

        //Check if user has entered valid path for thier resume

        //Check if Token has been entered at least once
        bool atLeastOneToken = CheckAPI_Tokens();
        if (atLeastOneToken)
        {
            History.InitGrid(dataGridView1);
        }

        //Temp debug -- adding the Rich text format Job descriptoin
        Helper._debug_RichTextArea(ManualJDPaste_txt);

        //Add code that will check if
        //ManualJDPaste_txt
        //has text or not

        Update_ProcessCreateAction_btn();
    }

    private void Update_ProcessCreateAction_btn()
    {
        //When called this function shall toggle and or update the ProcessCreateAction_btn button
        //rules are,
        //
        //if there is NO TEXT inside of ManualJDPaste_txt, then
        //1 - disable the button
        //2 - Set text to "process JD"
        //
        //If there is text inside of ManualJDPaste_txt, then
        //1 - Enable the button
        //2 - Set text to "Inject ATS keywords"

        if (ManualJDPaste_txt.Text.Length >= 5)
        {
            ProcessCreateAction_btn.Enabled = true;
        }
        else
        {
            ProcessCreateAction_btn.Enabled = false;
        }

        if(ATS_Injection_txt.Text.Length >= 5)
        {
            ATS_Injection_btn.Enabled = true;
        }
        else
        {
            ATS_Injection_btn.Enabled = false;
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

    private void Form1_FormClosing(object sender, FormClosingEventArgs e)
    {
        Helper._debug_RichTextArea(ManualJDPaste_txt);
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

    private void ProcessCreateAction_btn_Click(object sender, EventArgs e)
    {

        Task.Run(() =>
        {
            //When this function is called, it shall do one of two things
            //1 - Process the Job Description and send it to AI
            if (GetAPI_Token(API_AI_ID.Gemini, out string Token, out string errorMsg))
            {
                //at least we know token is written, pass it to the API thing now
                GeminiAPI API = new GeminiAPI(Token);
                //ProcessJD_btn.Enabled = false;
                //string prompt = "Explain DO-178C compliance levels.";
                string promptTry1 = Helper.AI_ATS_Question;
                Helper.FeedBackHelper.AppendFeedback($"Using {API_AI_ID.Gemini} AI to generate ATS friendly keywords and phrases.\r\n");

                string prompt = $"{promptTry1}\r\n{Helper.FeedBackHelper.GetTextManualJDPaste()}";

                // This runs the task on a ThreadPool thread and waits for the result
                string result = Task.Run(async () => await API.SendPrompt(prompt))
                                    .GetAwaiter()
                                    .GetResult();
                JD_Results = result;
                Helper.FeedBackHelper.SetATS_Injection(result);
                //Update_ProcessCreateAction_btn();
                Helper.FeedBackHelper.AppendFeedback($"Does the ATS injection look Acceptable at the bottom? \r\n");
            }
            else if (string.IsNullOrEmpty(errorMsg) == false)
            {
                FeedbackArea_txt.Text = $"An error occured retrieving your {API_AI_ID.Gemini} API token. Error stack:[{errorMsg}]";
            }
        });

        
    }

    #region old code, reuse when ready

    //private void ProcessJD_btn_Click(object sender, EventArgs e)
    //{
    //    //When clicked, you need to query the url, and then pass it over to
    //    //Gemini for now, add more options later
    //    string url = JobUrl_txt.Text.Trim();
    //    if (Helper.RemoveLoginJunk(url, out string newurl, out string complainMessage))
    //    {
    //        //if here, then url was modified, let the user know.
    //        DialogResult result = MessageBox.Show($"Change Url from {Environment.NewLine}[{url}] {Environment.NewLine}into {Environment.NewLine}[{newurl}] ?", "Personalised url removal", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

    //        if (result == DialogResult.Yes)
    //        {
    //            // User clicked Yes
    //            url = newurl;
    //        }
    //    }
    //    JD_Results = string.Empty;
    //    FeedbackArea_txt.Text = "Checking if JD website is valid, timeout is upto 10 seconds\r\n";

    //    bool isJD_source_available = Task.Run(async () =>
    //    {

    //        // This waits for the async method to finish on the background thread
    //        bool result = await Helper.CheckWebsiteAsync(url);

    //        return result;
    //    }).GetAwaiter().GetResult();

    //    if (isJD_source_available)
    //    {
    //        if (GetAPI_Token(API_AI_ID.Gemini, out string Token, out string errorMsg))
    //        {
    //            //at least we know token is written, pass it to the API thing now
    //            GeminiAPI API = new GeminiAPI(Token);
    //            ProcessJD_btn.Enabled = false;
    //            //string prompt = "Explain DO-178C compliance levels.";
    //            FeedbackArea_txt.AppendText($"Getting {API_AI_ID.Gemini} to download the Jod Description from link provided.\r\n");
    //            string prompt = $"Get the Job Description from this url:{url}.";

    //            // This runs the task on a ThreadPool thread and waits for the result
    //            string result = Task.Run(async () => await API.SendPrompt(prompt))
    //                                .GetAwaiter()
    //                                .GetResult();
    //            JD_Results = result;
    //            ProcessJD_btn.Enabled = true;
    //            CreatePDF_btn.Enabled = true;
    //            FeedbackArea_txt.AppendText($"Does this JD look correct? \r\n[{result}\r\n]\r\n");
    //        }
    //        else if (string.IsNullOrEmpty(errorMsg) == false)
    //        {
    //            FeedbackArea_txt.Text = $"An error occured retrieving your {API_AI_ID.Gemini} API token. Error stack:[{errorMsg}]";
    //        }
    //    }
    //    else
    //    {
    //        FeedbackArea_txt.AppendText("URL provided is not valid and/or down.\r\n");
    //    }
    //}

    //private void CreatePDF_btn_Click(object sender, EventArgs e)
    //{
    //    //When here, this means Job Description has been extracted.
    //    //Now go and make Gemini get the job talking points.
    //    if (GetAPI_Token(API_AI_ID.Gemini, out string Token, out string errorMsg))
    //    {
    //        //at least we know token is written, pass it to the API thing now
    //        GeminiAPI API = new GeminiAPI(Token);
    //        ProcessJD_btn.Enabled = false;
    //        //string prompt = "Explain DO-178C compliance levels.";
    //        FeedbackArea_txt.Text = $"Getting {API_AI_ID.Gemini} to create your experiance for the job.\r\n";
    //        string prompt = $"Create a list of achivements, experiance, roles that " +
    //            $"you would see in a qualified resume that match all relevant information about " +
    //            $"job duites, requirements and desired experiance from " +
    //            $"the following job description scrape:{JD_Results}. Do Not include your thinking, or process, just a stream of bullet points.";

    //        // This runs the task on a ThreadPool thread and waits for the result
    //        string result = Task.Run(async () => await API.SendPrompt(prompt))
    //                            .GetAwaiter()
    //                            .GetResult();
    //        JD_Results = result;
    //        ProcessJD_btn.Enabled = true;
    //        CreatePDF_btn.Enabled = true;
    //        FeedbackArea_txt.AppendText($"Does this JD look correct? \r\n[{result}\r\n]\r\n");
    //    }
    //    else if (string.IsNullOrEmpty(errorMsg) == false)
    //    {
    //        FeedbackArea_txt.Text = $"An error occured retrieving your {API_AI_ID.Gemini} API token. Error stack:[{errorMsg}]";
    //    }

    //}
    #endregion old code, reuse when ready
}
