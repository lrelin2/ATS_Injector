namespace ATS_Injector;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        tabControl1 = new TabControl();
        tabPage1 = new TabPage();
        ATS_Injection_btn = new Button();
        label6 = new Label();
        tabControl2 = new TabControl();
        tabPage4 = new TabPage();
        ManualJDPaste_txt = new RichTextBox();
        tabPage5 = new TabPage();
        textBox5 = new TextBox();
        label5 = new Label();
        textBox2 = new TextBox();
        progressBar1 = new ProgressBar();
        label2 = new Label();
        FeedbackArea_txt = new RichTextBox();
        ProcessCreateAction_btn = new Button();
        ATS_Injection_txt = new RichTextBox();
        OutputFileName_txt = new TextBox();
        label4 = new Label();
        OutputFolderPath_txt = new TextBox();
        label3 = new Label();
        tabPage2 = new TabPage();
        WarnOverWriteOutputFile_rdbtn = new RadioButton();
        SettingsResumePath_txt = new TextBox();
        label1 = new Label();
        groupBox1 = new GroupBox();
        button5 = new Button();
        AddClaudeToken_btn = new Button();
        Claude_rdbtn = new RadioButton();
        button3 = new Button();
        AddGeminiToken_btn = new Button();
        Gemini_rdbtn = new RadioButton();
        button2 = new Button();
        AddChatGPTToken_btn = new Button();
        ChatGPT_rdbtn = new RadioButton();
        tabPage3 = new TabPage();
        dataGridView1 = new DataGridView();
        tabControl1.SuspendLayout();
        tabPage1.SuspendLayout();
        tabControl2.SuspendLayout();
        tabPage4.SuspendLayout();
        tabPage5.SuspendLayout();
        tabPage2.SuspendLayout();
        groupBox1.SuspendLayout();
        tabPage3.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
        SuspendLayout();
        // 
        // tabControl1
        // 
        tabControl1.Controls.Add(tabPage1);
        tabControl1.Controls.Add(tabPage2);
        tabControl1.Controls.Add(tabPage3);
        tabControl1.Dock = DockStyle.Fill;
        tabControl1.Location = new Point(0, 0);
        tabControl1.Name = "tabControl1";
        tabControl1.SelectedIndex = 0;
        tabControl1.Size = new Size(800, 610);
        tabControl1.TabIndex = 0;
        // 
        // tabPage1
        // 
        tabPage1.Controls.Add(ATS_Injection_btn);
        tabPage1.Controls.Add(label6);
        tabPage1.Controls.Add(tabControl2);
        tabPage1.Controls.Add(progressBar1);
        tabPage1.Controls.Add(label2);
        tabPage1.Controls.Add(FeedbackArea_txt);
        tabPage1.Controls.Add(ProcessCreateAction_btn);
        tabPage1.Controls.Add(ATS_Injection_txt);
        tabPage1.Controls.Add(OutputFileName_txt);
        tabPage1.Controls.Add(label4);
        tabPage1.Controls.Add(OutputFolderPath_txt);
        tabPage1.Controls.Add(label3);
        tabPage1.Location = new Point(4, 24);
        tabPage1.Name = "tabPage1";
        tabPage1.Padding = new Padding(3);
        tabPage1.Size = new Size(792, 582);
        tabPage1.TabIndex = 0;
        tabPage1.Text = "ATS Injector Main";
        tabPage1.UseVisualStyleBackColor = true;
        // 
        // ATS_Injection_btn
        // 
        ATS_Injection_btn.Enabled = false;
        ATS_Injection_btn.Location = new Point(19, 509);
        ATS_Injection_btn.Name = "ATS_Injection_btn";
        ATS_Injection_btn.Size = new Size(123, 24);
        ATS_Injection_btn.TabIndex = 17;
        ATS_Injection_btn.Text = "Inject ATS";
        ATS_Injection_btn.UseVisualStyleBackColor = true;
        ATS_Injection_btn.Click += ATS_Injection_btn_Click;
        // 
        // label6
        // 
        label6.AutoSize = true;
        label6.Location = new Point(24, 299);
        label6.Name = "label6";
        label6.Size = new Size(96, 15);
        label6.TabIndex = 16;
        label6.Text = "Material to Inject";
        // 
        // tabControl2
        // 
        tabControl2.Controls.Add(tabPage4);
        tabControl2.Controls.Add(tabPage5);
        tabControl2.Location = new Point(13, 95);
        tabControl2.Name = "tabControl2";
        tabControl2.SelectedIndex = 0;
        tabControl2.Size = new Size(715, 198);
        tabControl2.TabIndex = 15;
        // 
        // tabPage4
        // 
        tabPage4.Controls.Add(ManualJDPaste_txt);
        tabPage4.Location = new Point(4, 24);
        tabPage4.Name = "tabPage4";
        tabPage4.Padding = new Padding(3);
        tabPage4.Size = new Size(707, 170);
        tabPage4.TabIndex = 0;
        tabPage4.Text = "Paste JD Here";
        tabPage4.UseVisualStyleBackColor = true;
        // 
        // ManualJDPaste_txt
        // 
        ManualJDPaste_txt.Dock = DockStyle.Fill;
        ManualJDPaste_txt.Location = new Point(3, 3);
        ManualJDPaste_txt.Name = "ManualJDPaste_txt";
        ManualJDPaste_txt.Size = new Size(701, 164);
        ManualJDPaste_txt.TabIndex = 0;
        ManualJDPaste_txt.Text = "";
        // 
        // tabPage5
        // 
        tabPage5.Controls.Add(textBox5);
        tabPage5.Controls.Add(label5);
        tabPage5.Controls.Add(textBox2);
        tabPage5.Location = new Point(4, 24);
        tabPage5.Name = "tabPage5";
        tabPage5.Padding = new Padding(3);
        tabPage5.Size = new Size(707, 170);
        tabPage5.TabIndex = 1;
        tabPage5.Text = "(Experimental ) JD URL";
        tabPage5.UseVisualStyleBackColor = true;
        // 
        // textBox5
        // 
        textBox5.Location = new Point(6, 35);
        textBox5.Multiline = true;
        textBox5.Name = "textBox5";
        textBox5.Size = new Size(677, 129);
        textBox5.TabIndex = 2;
        textBox5.Text = "Parsed JD will go here";
        // 
        // label5
        // 
        label5.AutoSize = true;
        label5.Location = new Point(7, 13);
        label5.Name = "label5";
        label5.Size = new Size(46, 15);
        label5.TabIndex = 1;
        label5.Text = "JD URL:";
        // 
        // textBox2
        // 
        textBox2.Location = new Point(51, 10);
        textBox2.Name = "textBox2";
        textBox2.Size = new Size(634, 23);
        textBox2.TabIndex = 0;
        // 
        // progressBar1
        // 
        progressBar1.Location = new Point(18, 540);
        progressBar1.Name = "progressBar1";
        progressBar1.Size = new Size(710, 23);
        progressBar1.TabIndex = 14;
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Location = new Point(18, 9);
        label2.Name = "label2";
        label2.Size = new Size(60, 15);
        label2.TabIndex = 13;
        label2.Text = "Feedback:";
        // 
        // FeedbackArea_txt
        // 
        FeedbackArea_txt.Location = new Point(13, 24);
        FeedbackArea_txt.Name = "FeedbackArea_txt";
        FeedbackArea_txt.Size = new Size(715, 65);
        FeedbackArea_txt.TabIndex = 12;
        FeedbackArea_txt.Text = "";
        // 
        // ProcessCreateAction_btn
        // 
        ProcessCreateAction_btn.Enabled = false;
        ProcessCreateAction_btn.Location = new Point(18, 480);
        ProcessCreateAction_btn.Name = "ProcessCreateAction_btn";
        ProcessCreateAction_btn.Size = new Size(123, 24);
        ProcessCreateAction_btn.TabIndex = 11;
        ProcessCreateAction_btn.Text = "Process JD";
        ProcessCreateAction_btn.UseVisualStyleBackColor = true;
        ProcessCreateAction_btn.Click += ProcessCreateAction_btn_Click;
        // 
        // ATS_Injection_txt
        // 
        ATS_Injection_txt.Location = new Point(17, 317);
        ATS_Injection_txt.Name = "ATS_Injection_txt";
        ATS_Injection_txt.Size = new Size(710, 157);
        ATS_Injection_txt.TabIndex = 10;
        ATS_Injection_txt.Text = "";
        // 
        // OutputFileName_txt
        // 
        OutputFileName_txt.Location = new Point(266, 511);
        OutputFileName_txt.Name = "OutputFileName_txt";
        OutputFileName_txt.Size = new Size(458, 23);
        OutputFileName_txt.TabIndex = 8;
        // 
        // label4
        // 
        label4.AutoSize = true;
        label4.Location = new Point(177, 514);
        label4.Name = "label4";
        label4.Size = new Size(83, 15);
        label4.TabIndex = 7;
        label4.Text = "Output Name:";
        // 
        // OutputFolderPath_txt
        // 
        OutputFolderPath_txt.Location = new Point(266, 482);
        OutputFolderPath_txt.Name = "OutputFolderPath_txt";
        OutputFolderPath_txt.Size = new Size(458, 23);
        OutputFolderPath_txt.TabIndex = 6;
        OutputFolderPath_txt.Text = "C:\\Users\\leore\\Downloads\\Custom ATS output";
        // 
        // label3
        // 
        label3.AutoSize = true;
        label3.Location = new Point(176, 485);
        label3.Name = "label3";
        label3.Size = new Size(84, 15);
        label3.TabIndex = 5;
        label3.Text = "Output Folder:";
        // 
        // tabPage2
        // 
        tabPage2.Controls.Add(WarnOverWriteOutputFile_rdbtn);
        tabPage2.Controls.Add(SettingsResumePath_txt);
        tabPage2.Controls.Add(label1);
        tabPage2.Controls.Add(groupBox1);
        tabPage2.Location = new Point(4, 24);
        tabPage2.Name = "tabPage2";
        tabPage2.Padding = new Padding(3);
        tabPage2.Size = new Size(792, 582);
        tabPage2.TabIndex = 1;
        tabPage2.Text = "Settings";
        tabPage2.UseVisualStyleBackColor = true;
        // 
        // OverWriteOutputFile_rdbtn
        // 
        WarnOverWriteOutputFile_rdbtn.AutoSize = true;
        WarnOverWriteOutputFile_rdbtn.Location = new Point(11, 191);
        WarnOverWriteOutputFile_rdbtn.Name = "OverWriteOutputFile_rdbtn";
        WarnOverWriteOutputFile_rdbtn.Size = new Size(153, 19);
        WarnOverWriteOutputFile_rdbtn.TabIndex = 11;
        WarnOverWriteOutputFile_rdbtn.TabStop = true;
        WarnOverWriteOutputFile_rdbtn.Text = "Warn when outfile exists";
        WarnOverWriteOutputFile_rdbtn.UseVisualStyleBackColor = true;
        // 
        // SettingsResumePath_txt
        // 
        SettingsResumePath_txt.Location = new Point(130, 14);
        SettingsResumePath_txt.Name = "SettingsResumePath_txt";
        SettingsResumePath_txt.Size = new Size(586, 23);
        SettingsResumePath_txt.TabIndex = 3;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(8, 17);
        label1.Name = "label1";
        label1.Size = new Size(106, 15);
        label1.TabIndex = 2;
        label1.Text = "Your Resume Path:";
        // 
        // groupBox1
        // 
        groupBox1.Controls.Add(button5);
        groupBox1.Controls.Add(AddClaudeToken_btn);
        groupBox1.Controls.Add(Claude_rdbtn);
        groupBox1.Controls.Add(button3);
        groupBox1.Controls.Add(AddGeminiToken_btn);
        groupBox1.Controls.Add(Gemini_rdbtn);
        groupBox1.Controls.Add(button2);
        groupBox1.Controls.Add(AddChatGPTToken_btn);
        groupBox1.Controls.Add(ChatGPT_rdbtn);
        groupBox1.Location = new Point(11, 43);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new Size(444, 142);
        groupBox1.TabIndex = 0;
        groupBox1.TabStop = false;
        groupBox1.Text = "AI Models";
        // 
        // button5
        // 
        button5.Location = new Point(213, 82);
        button5.Name = "button5";
        button5.Size = new Size(104, 24);
        button5.TabIndex = 10;
        button5.Text = "Help";
        button5.UseVisualStyleBackColor = true;
        // 
        // AddClaudeToken_btn
        // 
        AddClaudeToken_btn.Location = new Point(89, 82);
        AddClaudeToken_btn.Name = "AddClaudeToken_btn";
        AddClaudeToken_btn.Size = new Size(104, 24);
        AddClaudeToken_btn.TabIndex = 9;
        AddClaudeToken_btn.Text = "Add API Token";
        AddClaudeToken_btn.UseVisualStyleBackColor = true;
        // 
        // Claude_rdbtn
        // 
        Claude_rdbtn.AutoSize = true;
        Claude_rdbtn.Location = new Point(11, 87);
        Claude_rdbtn.Name = "Claude_rdbtn";
        Claude_rdbtn.Size = new Size(62, 19);
        Claude_rdbtn.TabIndex = 8;
        Claude_rdbtn.TabStop = true;
        Claude_rdbtn.Text = "Claude";
        Claude_rdbtn.UseVisualStyleBackColor = true;
        // 
        // button3
        // 
        button3.Location = new Point(213, 52);
        button3.Name = "button3";
        button3.Size = new Size(104, 24);
        button3.TabIndex = 7;
        button3.Text = "Help";
        button3.UseVisualStyleBackColor = true;
        // 
        // AddGeminiToken_btn
        // 
        AddGeminiToken_btn.Location = new Point(89, 52);
        AddGeminiToken_btn.Name = "AddGeminiToken_btn";
        AddGeminiToken_btn.Size = new Size(104, 24);
        AddGeminiToken_btn.TabIndex = 6;
        AddGeminiToken_btn.Text = "Add API Token";
        AddGeminiToken_btn.UseVisualStyleBackColor = true;
        AddGeminiToken_btn.Click += AddGeminiToken_btn_Click;
        // 
        // Gemini_rdbtn
        // 
        Gemini_rdbtn.AutoSize = true;
        Gemini_rdbtn.Location = new Point(11, 57);
        Gemini_rdbtn.Name = "Gemini_rdbtn";
        Gemini_rdbtn.Size = new Size(63, 19);
        Gemini_rdbtn.TabIndex = 5;
        Gemini_rdbtn.TabStop = true;
        Gemini_rdbtn.Text = "Gemini";
        Gemini_rdbtn.UseVisualStyleBackColor = true;
        // 
        // button2
        // 
        button2.Location = new Point(213, 22);
        button2.Name = "button2";
        button2.Size = new Size(104, 24);
        button2.TabIndex = 4;
        button2.Text = "Help";
        button2.UseVisualStyleBackColor = true;
        // 
        // AddChatGPTToken_btn
        // 
        AddChatGPTToken_btn.Location = new Point(89, 22);
        AddChatGPTToken_btn.Name = "AddChatGPTToken_btn";
        AddChatGPTToken_btn.Size = new Size(104, 24);
        AddChatGPTToken_btn.TabIndex = 3;
        AddChatGPTToken_btn.Text = "Add API Token";
        AddChatGPTToken_btn.UseVisualStyleBackColor = true;
        AddChatGPTToken_btn.Click += AddChatGPTToken_btn_Click;
        // 
        // ChatGPT_rdbtn
        // 
        ChatGPT_rdbtn.AutoSize = true;
        ChatGPT_rdbtn.Location = new Point(11, 27);
        ChatGPT_rdbtn.Name = "ChatGPT_rdbtn";
        ChatGPT_rdbtn.Size = new Size(72, 19);
        ChatGPT_rdbtn.TabIndex = 0;
        ChatGPT_rdbtn.TabStop = true;
        ChatGPT_rdbtn.Text = "ChatGPT";
        ChatGPT_rdbtn.UseVisualStyleBackColor = true;
        // 
        // tabPage3
        // 
        tabPage3.Controls.Add(dataGridView1);
        tabPage3.Location = new Point(4, 24);
        tabPage3.Name = "tabPage3";
        tabPage3.Size = new Size(792, 582);
        tabPage3.TabIndex = 2;
        tabPage3.Text = "History";
        tabPage3.UseVisualStyleBackColor = true;
        // 
        // dataGridView1
        // 
        dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dataGridView1.Dock = DockStyle.Fill;
        dataGridView1.Location = new Point(0, 0);
        dataGridView1.Name = "dataGridView1";
        dataGridView1.Size = new Size(792, 582);
        dataGridView1.TabIndex = 0;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 610);
        Controls.Add(tabControl1);
        Name = "Form1";
        Text = "Form1";
        FormClosing += Form1_FormClosing;
        Shown += Form1_Shown;
        tabControl1.ResumeLayout(false);
        tabPage1.ResumeLayout(false);
        tabPage1.PerformLayout();
        tabControl2.ResumeLayout(false);
        tabPage4.ResumeLayout(false);
        tabPage5.ResumeLayout(false);
        tabPage5.PerformLayout();
        tabPage2.ResumeLayout(false);
        tabPage2.PerformLayout();
        groupBox1.ResumeLayout(false);
        groupBox1.PerformLayout();
        tabPage3.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private TabControl tabControl1;
    private TabPage tabPage1;
    private TabPage tabPage2;
    private TextBox OutputFileName_txt;
    private Label label4;
    private TextBox OutputFolderPath_txt;
    private Label label3;
    private GroupBox groupBox1;
    private Button button5;
    private Button AddClaudeToken_btn;
    private RadioButton Claude_rdbtn;
    private Button button3;
    private Button AddGeminiToken_btn;
    private RadioButton Gemini_rdbtn;
    private Button button2;
    private Button AddChatGPTToken_btn;
    private RadioButton ChatGPT_rdbtn;
    private RichTextBox ATS_Injection_txt;
    private Button ProcessCreateAction_btn;
    private TabPage tabPage3;
    private DataGridView dataGridView1;
    private TextBox SettingsResumePath_txt;
    private Label label1;
    private Label label2;
    private RichTextBox FeedbackArea_txt;
    private TabControl tabControl2;
    private TabPage tabPage4;
    private TabPage tabPage5;
    private ProgressBar progressBar1;
    private Label label6;
    private TextBox textBox5;
    private Label label5;
    private TextBox textBox2;
    private RichTextBox ManualJDPaste_txt;
    private Button ATS_Injection_btn;
    private RadioButton WarnOverWriteOutputFile_rdbtn;
}
