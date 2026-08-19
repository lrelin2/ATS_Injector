using System.Drawing;
using System.Windows.Forms;

namespace ATSInjector
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.meta_Keywords = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.meta_Producer = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.meta_Author = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.meta_Creator = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.meta_Subject = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.meta_Title = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.DebugDontSaveHistory_chkbox = new System.Windows.Forms.CheckBox();
            this.AI_IntTimeout_txt = new System.Windows.Forms.TextBox();
            this.SettingsResumePath_txt = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.WarnOverWriteOutputFile_chkbx = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Twitter_rdbtn = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.AddTwitterToken_btn = new System.Windows.Forms.Button();
            this.Claude_rdbtn = new System.Windows.Forms.RadioButton();
            this.Gemini_rdbtn = new System.Windows.Forms.RadioButton();
            this.ChatGPT_rdbtn = new System.Windows.Forms.RadioButton();
            this.button5 = new System.Windows.Forms.Button();
            this.AddClaudeToken_btn = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.AddGeminiToken_btn = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.AddChatGPTToken_btn = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.ATS_Injection_btn = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.ManualJDPaste_txt = new System.Windows.Forms.RichTextBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.FeedbackArea_txt = new System.Windows.Forms.RichTextBox();
            this.ATS_Injection_txt = new System.Windows.Forms.RichTextBox();
            this.OutputFileName_txt = new System.Windows.Forms.TextBox();
            this.OutputFolderPath_txt = new System.Windows.Forms.TextBox();
            this.ProcessJD_btn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.MasterAIInjection_chkbox = new System.Windows.Forms.CheckBox();
            this.tabPage2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.MasterAIInjection_chkbox);
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Controls.Add(this.DebugDontSaveHistory_chkbox);
            this.tabPage2.Controls.Add(this.AI_IntTimeout_txt);
            this.tabPage2.Controls.Add(this.SettingsResumePath_txt);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.WarnOverWriteOutputFile_chkbx);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(678, 503);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Settings";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.meta_Keywords);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.meta_Producer);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.meta_Author);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.meta_Creator);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.meta_Subject);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.meta_Title);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Location = new System.Drawing.Point(16, 253);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(372, 180);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Edit Meta Data (leave empty to clone original)";
            // 
            // meta_Keywords
            // 
            this.meta_Keywords.Location = new System.Drawing.Point(61, 154);
            this.meta_Keywords.Name = "meta_Keywords";
            this.meta_Keywords.Size = new System.Drawing.Size(292, 20);
            this.meta_Keywords.TabIndex = 11;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 157);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(56, 13);
            this.label13.TabIndex = 10;
            this.label13.Text = "Keywords:";
            // 
            // meta_Producer
            // 
            this.meta_Producer.Location = new System.Drawing.Point(61, 129);
            this.meta_Producer.Name = "meta_Producer";
            this.meta_Producer.Size = new System.Drawing.Size(292, 20);
            this.meta_Producer.TabIndex = 9;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(7, 132);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 13);
            this.label12.TabIndex = 8;
            this.label12.Text = "Producer:";
            // 
            // meta_Author
            // 
            this.meta_Author.Location = new System.Drawing.Point(61, 103);
            this.meta_Author.Name = "meta_Author";
            this.meta_Author.Size = new System.Drawing.Size(292, 20);
            this.meta_Author.TabIndex = 7;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(18, 106);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 13);
            this.label11.TabIndex = 6;
            this.label11.Text = "Author:";
            // 
            // meta_Creator
            // 
            this.meta_Creator.Location = new System.Drawing.Point(61, 77);
            this.meta_Creator.Name = "meta_Creator";
            this.meta_Creator.Size = new System.Drawing.Size(292, 20);
            this.meta_Creator.TabIndex = 5;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(14, 80);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(44, 13);
            this.label10.TabIndex = 4;
            this.label10.Text = "Creator:";
            // 
            // meta_Subject
            // 
            this.meta_Subject.Location = new System.Drawing.Point(61, 51);
            this.meta_Subject.Name = "meta_Subject";
            this.meta_Subject.Size = new System.Drawing.Size(292, 20);
            this.meta_Subject.TabIndex = 3;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 54);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(46, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "Subject:";
            // 
            // meta_Title
            // 
            this.meta_Title.Location = new System.Drawing.Point(61, 25);
            this.meta_Title.Name = "meta_Title";
            this.meta_Title.Size = new System.Drawing.Size(292, 20);
            this.meta_Title.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(25, 28);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(30, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Title:";
            // 
            // DebugDontSaveHistory_chkbox
            // 
            this.DebugDontSaveHistory_chkbox.AutoSize = true;
            this.DebugDontSaveHistory_chkbox.Location = new System.Drawing.Point(212, 219);
            this.DebugDontSaveHistory_chkbox.Name = "DebugDontSaveHistory_chkbox";
            this.DebugDontSaveHistory_chkbox.Size = new System.Drawing.Size(149, 17);
            this.DebugDontSaveHistory_chkbox.TabIndex = 7;
            this.DebugDontSaveHistory_chkbox.Text = "Debug Don\'t Save History";
            this.DebugDontSaveHistory_chkbox.UseVisualStyleBackColor = true;
            // 
            // AI_IntTimeout_txt
            // 
            this.AI_IntTimeout_txt.Location = new System.Drawing.Point(141, 217);
            this.AI_IntTimeout_txt.Name = "AI_IntTimeout_txt";
            this.AI_IntTimeout_txt.Size = new System.Drawing.Size(65, 20);
            this.AI_IntTimeout_txt.TabIndex = 6;
            this.AI_IntTimeout_txt.Text = "30";
            // 
            // SettingsResumePath_txt
            // 
            this.SettingsResumePath_txt.Location = new System.Drawing.Point(111, 12);
            this.SettingsResumePath_txt.Name = "SettingsResumePath_txt";
            this.SettingsResumePath_txt.Size = new System.Drawing.Size(503, 20);
            this.SettingsResumePath_txt.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 219);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(133, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Timeout For AI processing:";
            // 
            // WarnOverWriteOutputFile_chkbx
            // 
            this.WarnOverWriteOutputFile_chkbx.AutoSize = true;
            this.WarnOverWriteOutputFile_chkbx.Location = new System.Drawing.Point(11, 193);
            this.WarnOverWriteOutputFile_chkbx.Name = "WarnOverWriteOutputFile_chkbx";
            this.WarnOverWriteOutputFile_chkbx.Size = new System.Drawing.Size(141, 17);
            this.WarnOverWriteOutputFile_chkbx.TabIndex = 4;
            this.WarnOverWriteOutputFile_chkbx.Text = "Warn when outfile exists";
            this.WarnOverWriteOutputFile_chkbx.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Your Resume Path:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Twitter_rdbtn);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.AddTwitterToken_btn);
            this.groupBox1.Controls.Add(this.Claude_rdbtn);
            this.groupBox1.Controls.Add(this.Gemini_rdbtn);
            this.groupBox1.Controls.Add(this.ChatGPT_rdbtn);
            this.groupBox1.Controls.Add(this.button5);
            this.groupBox1.Controls.Add(this.AddClaudeToken_btn);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.AddGeminiToken_btn);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.AddChatGPTToken_btn);
            this.groupBox1.Location = new System.Drawing.Point(9, 37);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(380, 135);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "AI Models";
            // 
            // Twitter_rdbtn
            // 
            this.Twitter_rdbtn.AutoSize = true;
            this.Twitter_rdbtn.Location = new System.Drawing.Point(14, 94);
            this.Twitter_rdbtn.Name = "Twitter_rdbtn";
            this.Twitter_rdbtn.Size = new System.Drawing.Size(89, 17);
            this.Twitter_rdbtn.TabIndex = 16;
            this.Twitter_rdbtn.TabStop = true;
            this.Twitter_rdbtn.Text = "Grok (Twitter)";
            this.Twitter_rdbtn.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(281, 94);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(89, 21);
            this.button1.TabIndex = 15;
            this.button1.Text = "Help";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.HelpTwitter_Click);
            // 
            // AddTwitterToken_btn
            // 
            this.AddTwitterToken_btn.Location = new System.Drawing.Point(175, 94);
            this.AddTwitterToken_btn.Name = "AddTwitterToken_btn";
            this.AddTwitterToken_btn.Size = new System.Drawing.Size(89, 21);
            this.AddTwitterToken_btn.TabIndex = 14;
            this.AddTwitterToken_btn.Text = "Add API Key";
            this.AddTwitterToken_btn.UseVisualStyleBackColor = true;
            this.AddTwitterToken_btn.Click += new System.EventHandler(this.AddTwitterToken_btn_Click);
            // 
            // Claude_rdbtn
            // 
            this.Claude_rdbtn.AutoSize = true;
            this.Claude_rdbtn.Location = new System.Drawing.Point(14, 71);
            this.Claude_rdbtn.Name = "Claude_rdbtn";
            this.Claude_rdbtn.Size = new System.Drawing.Size(125, 17);
            this.Claude_rdbtn.TabIndex = 13;
            this.Claude_rdbtn.TabStop = true;
            this.Claude_rdbtn.Text = "OpenRouter (Claude)";
            this.Claude_rdbtn.UseVisualStyleBackColor = true;
            // 
            // Gemini_rdbtn
            // 
            this.Gemini_rdbtn.AutoSize = true;
            this.Gemini_rdbtn.Location = new System.Drawing.Point(14, 45);
            this.Gemini_rdbtn.Name = "Gemini_rdbtn";
            this.Gemini_rdbtn.Size = new System.Drawing.Size(57, 17);
            this.Gemini_rdbtn.TabIndex = 12;
            this.Gemini_rdbtn.TabStop = true;
            this.Gemini_rdbtn.Text = "Gemini";
            this.Gemini_rdbtn.UseVisualStyleBackColor = true;
            // 
            // ChatGPT_rdbtn
            // 
            this.ChatGPT_rdbtn.AutoSize = true;
            this.ChatGPT_rdbtn.Location = new System.Drawing.Point(14, 19);
            this.ChatGPT_rdbtn.Name = "ChatGPT_rdbtn";
            this.ChatGPT_rdbtn.Size = new System.Drawing.Size(69, 17);
            this.ChatGPT_rdbtn.TabIndex = 11;
            this.ChatGPT_rdbtn.TabStop = true;
            this.ChatGPT_rdbtn.Text = "ChatGPT";
            this.ChatGPT_rdbtn.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(281, 71);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(89, 21);
            this.button5.TabIndex = 10;
            this.button5.Text = "Help";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.HelpClaude_Click);
            // 
            // AddClaudeToken_btn
            // 
            this.AddClaudeToken_btn.Location = new System.Drawing.Point(175, 71);
            this.AddClaudeToken_btn.Name = "AddClaudeToken_btn";
            this.AddClaudeToken_btn.Size = new System.Drawing.Size(89, 21);
            this.AddClaudeToken_btn.TabIndex = 9;
            this.AddClaudeToken_btn.Text = "Add API Key";
            this.AddClaudeToken_btn.UseVisualStyleBackColor = true;
            this.AddClaudeToken_btn.Click += new System.EventHandler(this.AddClaudeToken_btn_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(281, 45);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(89, 21);
            this.button3.TabIndex = 7;
            this.button3.Text = "Help";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.HelpGemini_Click);
            // 
            // AddGeminiToken_btn
            // 
            this.AddGeminiToken_btn.Location = new System.Drawing.Point(175, 45);
            this.AddGeminiToken_btn.Name = "AddGeminiToken_btn";
            this.AddGeminiToken_btn.Size = new System.Drawing.Size(89, 21);
            this.AddGeminiToken_btn.TabIndex = 6;
            this.AddGeminiToken_btn.Text = "Add API Key";
            this.AddGeminiToken_btn.UseVisualStyleBackColor = true;
            this.AddGeminiToken_btn.Click += new System.EventHandler(this.AddGeminiToken_btn_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(281, 19);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(89, 21);
            this.button2.TabIndex = 4;
            this.button2.Text = "Help";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.HelpChatGPT_Click);
            // 
            // AddChatGPTToken_btn
            // 
            this.AddChatGPTToken_btn.Location = new System.Drawing.Point(175, 19);
            this.AddChatGPTToken_btn.Name = "AddChatGPTToken_btn";
            this.AddChatGPTToken_btn.Size = new System.Drawing.Size(89, 21);
            this.AddChatGPTToken_btn.TabIndex = 3;
            this.AddChatGPTToken_btn.Text = "Add API Key";
            this.AddChatGPTToken_btn.UseVisualStyleBackColor = true;
            this.AddChatGPTToken_btn.Click += new System.EventHandler(this.AddChatGPTToken_btn_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.ATS_Injection_btn);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.tabControl2);
            this.tabPage1.Controls.Add(this.progressBar1);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.FeedbackArea_txt);
            this.tabPage1.Controls.Add(this.ATS_Injection_txt);
            this.tabPage1.Controls.Add(this.OutputFileName_txt);
            this.tabPage1.Controls.Add(this.OutputFolderPath_txt);
            this.tabPage1.Controls.Add(this.ProcessJD_btn);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(678, 503);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "ATS Injector Main";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // ATS_Injection_btn
            // 
            this.ATS_Injection_btn.Enabled = false;
            this.ATS_Injection_btn.Location = new System.Drawing.Point(16, 441);
            this.ATS_Injection_btn.Name = "ATS_Injection_btn";
            this.ATS_Injection_btn.Size = new System.Drawing.Size(105, 21);
            this.ATS_Injection_btn.TabIndex = 17;
            this.ATS_Injection_btn.Text = "Inject ATS";
            this.ATS_Injection_btn.UseVisualStyleBackColor = true;
            this.ATS_Injection_btn.Click += new System.EventHandler(this.ATS_Injection_btn_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 259);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Material to Inject";
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Controls.Add(this.tabPage5);
            this.tabControl2.Location = new System.Drawing.Point(11, 82);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(613, 172);
            this.tabControl2.TabIndex = 15;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.ManualJDPaste_txt);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(605, 146);
            this.tabPage4.TabIndex = 0;
            this.tabPage4.Text = "Paste JD Here";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // ManualJDPaste_txt
            // 
            this.ManualJDPaste_txt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ManualJDPaste_txt.Location = new System.Drawing.Point(3, 3);
            this.ManualJDPaste_txt.Name = "ManualJDPaste_txt";
            this.ManualJDPaste_txt.Size = new System.Drawing.Size(599, 140);
            this.ManualJDPaste_txt.TabIndex = 0;
            this.ManualJDPaste_txt.Text = "";
            this.ManualJDPaste_txt.TextChanged += new System.EventHandler(this.ManualJDPaste_txt_TextChanged);
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.textBox5);
            this.tabPage5.Controls.Add(this.label5);
            this.tabPage5.Controls.Add(this.textBox2);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(605, 146);
            this.tabPage5.TabIndex = 1;
            this.tabPage5.Text = "(Experimental ) JD URL";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(5, 30);
            this.textBox5.Multiline = true;
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(581, 112);
            this.textBox5.TabIndex = 2;
            this.textBox5.Text = "Parsed JD will go here";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "JD URL:";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(44, 9);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(544, 20);
            this.textBox2.TabIndex = 0;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(15, 468);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(609, 20);
            this.progressBar1.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Feedback:";
            // 
            // FeedbackArea_txt
            // 
            this.FeedbackArea_txt.Location = new System.Drawing.Point(11, 21);
            this.FeedbackArea_txt.Name = "FeedbackArea_txt";
            this.FeedbackArea_txt.Size = new System.Drawing.Size(613, 57);
            this.FeedbackArea_txt.TabIndex = 12;
            this.FeedbackArea_txt.Text = "";
            // 
            // ATS_Injection_txt
            // 
            this.ATS_Injection_txt.Location = new System.Drawing.Point(15, 275);
            this.ATS_Injection_txt.Name = "ATS_Injection_txt";
            this.ATS_Injection_txt.Size = new System.Drawing.Size(609, 137);
            this.ATS_Injection_txt.TabIndex = 10;
            this.ATS_Injection_txt.Text = "";
            // 
            // OutputFileName_txt
            // 
            this.OutputFileName_txt.Location = new System.Drawing.Point(228, 443);
            this.OutputFileName_txt.Name = "OutputFileName_txt";
            this.OutputFileName_txt.Size = new System.Drawing.Size(393, 20);
            this.OutputFileName_txt.TabIndex = 8;
            // 
            // OutputFolderPath_txt
            // 
            this.OutputFolderPath_txt.Location = new System.Drawing.Point(228, 418);
            this.OutputFolderPath_txt.Name = "OutputFolderPath_txt";
            this.OutputFolderPath_txt.Size = new System.Drawing.Size(393, 20);
            this.OutputFolderPath_txt.TabIndex = 6;
            // 
            // ProcessJD_btn
            // 
            this.ProcessJD_btn.Enabled = false;
            this.ProcessJD_btn.Location = new System.Drawing.Point(15, 416);
            this.ProcessJD_btn.Name = "ProcessJD_btn";
            this.ProcessJD_btn.Size = new System.Drawing.Size(105, 21);
            this.ProcessJD_btn.TabIndex = 11;
            this.ProcessJD_btn.Text = "Process JD";
            this.ProcessJD_btn.UseVisualStyleBackColor = true;
            this.ProcessJD_btn.Click += new System.EventHandler(this.ProcessJD_btn_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(152, 445);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Output Name:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(151, 420);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Output Folder:";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(686, 529);
            this.tabControl1.TabIndex = 0;
            // 
            // MasterAIInjection_chkbox
            // 
            this.MasterAIInjection_chkbox.AutoSize = true;
            this.MasterAIInjection_chkbox.Checked = true;
            this.MasterAIInjection_chkbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MasterAIInjection_chkbox.Location = new System.Drawing.Point(158, 194);
            this.MasterAIInjection_chkbox.Name = "MasterAIInjection_chkbox";
            this.MasterAIInjection_chkbox.Size = new System.Drawing.Size(163, 17);
            this.MasterAIInjection_chkbox.TabIndex = 9;
            this.MasterAIInjection_chkbox.Text = "Master AI Injection statement";
            this.MasterAIInjection_chkbox.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 529);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Job Description into ATS injection";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Shown);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabControl2.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private TabPage tabPage2;
        private TextBox AI_IntTimeout_txt;
        private TextBox SettingsResumePath_txt;
        private Label label7;
        private CheckBox WarnOverWriteOutputFile_chkbx;
        private Label label1;
        private GroupBox groupBox1;
        private RadioButton Claude_rdbtn;
        private RadioButton Gemini_rdbtn;
        private RadioButton ChatGPT_rdbtn;
        private Button button5;
        private Button AddClaudeToken_btn;
        private Button button3;
        private Button AddGeminiToken_btn;
        private Button button2;
        private Button AddChatGPTToken_btn;
        private TabPage tabPage1;
        private Button ATS_Injection_btn;
        private Label label6;
        private TabControl tabControl2;
        private TabPage tabPage4;
        private RichTextBox ManualJDPaste_txt;
        private TabPage tabPage5;
        private TextBox textBox5;
        private Label label5;
        private TextBox textBox2;
        private ProgressBar progressBar1;
        private Label label2;
        private RichTextBox FeedbackArea_txt;
        private RichTextBox ATS_Injection_txt;
        private TextBox OutputFileName_txt;
        private TextBox OutputFolderPath_txt;
        private Button ProcessJD_btn;
        private Label label4;
        private Label label3;
        private TabControl tabControl1;
        private RadioButton Twitter_rdbtn;
        private Button button1;
        private Button AddTwitterToken_btn;
        private CheckBox DebugDontSaveHistory_chkbox;
        private GroupBox groupBox2;
        private TextBox meta_Keywords;
        private Label label13;
        private TextBox meta_Producer;
        private Label label12;
        private TextBox meta_Author;
        private Label label11;
        private TextBox meta_Creator;
        private Label label10;
        private TextBox meta_Subject;
        private Label label9;
        private TextBox meta_Title;
        private Label label8;
        private CheckBox MasterAIInjection_chkbox;
    }

}

