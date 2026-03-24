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
        CreatePDF_btn = new Button();
        FeedbackArea_txt = new RichTextBox();
        progressBar1 = new ProgressBar();
        textBox4 = new TextBox();
        label4 = new Label();
        textBox3 = new TextBox();
        label3 = new Label();
        ProcessJD_btn = new Button();
        JobUrl_txt = new TextBox();
        label2 = new Label();
        textBox1 = new TextBox();
        label1 = new Label();
        tabPage2 = new TabPage();
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
        tabControl1.Size = new Size(800, 450);
        tabControl1.TabIndex = 0;
        // 
        // tabPage1
        // 
        tabPage1.Controls.Add(CreatePDF_btn);
        tabPage1.Controls.Add(FeedbackArea_txt);
        tabPage1.Controls.Add(progressBar1);
        tabPage1.Controls.Add(textBox4);
        tabPage1.Controls.Add(label4);
        tabPage1.Controls.Add(textBox3);
        tabPage1.Controls.Add(label3);
        tabPage1.Controls.Add(ProcessJD_btn);
        tabPage1.Controls.Add(JobUrl_txt);
        tabPage1.Controls.Add(label2);
        tabPage1.Controls.Add(textBox1);
        tabPage1.Controls.Add(label1);
        tabPage1.Location = new Point(4, 24);
        tabPage1.Name = "tabPage1";
        tabPage1.Padding = new Padding(3);
        tabPage1.Size = new Size(792, 422);
        tabPage1.TabIndex = 0;
        tabPage1.Text = "ATS Injector Main";
        tabPage1.UseVisualStyleBackColor = true;
        // 
        // CreatePDF_btn
        // 
        CreatePDF_btn.Enabled = false;
        CreatePDF_btn.Location = new Point(3, 123);
        CreatePDF_btn.Name = "CreatePDF_btn";
        CreatePDF_btn.Size = new Size(123, 24);
        CreatePDF_btn.TabIndex = 11;
        CreatePDF_btn.Text = "Create Custom PDF";
        CreatePDF_btn.UseVisualStyleBackColor = true;
        CreatePDF_btn.Click += CreatePDF_btn_Click;
        // 
        // FeedbackArea_txt
        // 
        FeedbackArea_txt.Location = new Point(7, 234);
        FeedbackArea_txt.Name = "FeedbackArea_txt";
        FeedbackArea_txt.Size = new Size(710, 157);
        FeedbackArea_txt.TabIndex = 10;
        FeedbackArea_txt.Text = "";
        // 
        // progressBar1
        // 
        progressBar1.Location = new Point(132, 93);
        progressBar1.Name = "progressBar1";
        progressBar1.Size = new Size(585, 54);
        progressBar1.TabIndex = 9;
        // 
        // textBox4
        // 
        textBox4.Location = new Point(130, 197);
        textBox4.Name = "textBox4";
        textBox4.Size = new Size(586, 23);
        textBox4.TabIndex = 8;
        // 
        // label4
        // 
        label4.AutoSize = true;
        label4.Location = new Point(8, 200);
        label4.Name = "label4";
        label4.Size = new Size(63, 15);
        label4.TabIndex = 7;
        label4.Text = "File Name:";
        // 
        // textBox3
        // 
        textBox3.Location = new Point(130, 168);
        textBox3.Name = "textBox3";
        textBox3.Size = new Size(586, 23);
        textBox3.TabIndex = 6;
        textBox3.Text = "C:\\Users\\leore\\Downloads\\Custom ATS output";
        // 
        // label3
        // 
        label3.AutoSize = true;
        label3.Location = new Point(8, 171);
        label3.Name = "label3";
        label3.Size = new Size(84, 15);
        label3.TabIndex = 5;
        label3.Text = "Output Folder:";
        // 
        // ProcessJD_btn
        // 
        ProcessJD_btn.Location = new Point(3, 93);
        ProcessJD_btn.Name = "ProcessJD_btn";
        ProcessJD_btn.Size = new Size(123, 24);
        ProcessJD_btn.TabIndex = 4;
        ProcessJD_btn.Text = "Process JD";
        ProcessJD_btn.UseVisualStyleBackColor = true;
        ProcessJD_btn.Click += ProcessJD_btn_Click;
        // 
        // JobUrl_txt
        // 
        JobUrl_txt.Location = new Point(130, 52);
        JobUrl_txt.Name = "JobUrl_txt";
        JobUrl_txt.Size = new Size(586, 23);
        JobUrl_txt.TabIndex = 3;
        JobUrl_txt.Text = "https://www.linkedin.com/jobs/collections/recommended/?currentJobId=4359058081";
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Location = new Point(8, 55);
        label2.Name = "label2";
        label2.Size = new Size(52, 15);
        label2.TabIndex = 2;
        label2.Text = "Job URL:";
        // 
        // textBox1
        // 
        textBox1.Location = new Point(130, 11);
        textBox1.Name = "textBox1";
        textBox1.Size = new Size(586, 23);
        textBox1.TabIndex = 1;
        textBox1.Text = "C:\\Users\\leore\\Downloads\\Leo Relin Resume 2026.pdf";
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(8, 14);
        label1.Name = "label1";
        label1.Size = new Size(106, 15);
        label1.TabIndex = 0;
        label1.Text = "Your Resume Path:";
        // 
        // tabPage2
        // 
        tabPage2.Controls.Add(groupBox1);
        tabPage2.Location = new Point(4, 24);
        tabPage2.Name = "tabPage2";
        tabPage2.Padding = new Padding(3);
        tabPage2.Size = new Size(792, 422);
        tabPage2.TabIndex = 1;
        tabPage2.Text = "Settings";
        tabPage2.UseVisualStyleBackColor = true;
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
        groupBox1.Location = new Point(18, 23);
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
        tabPage3.Size = new Size(792, 422);
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
        dataGridView1.Size = new Size(792, 422);
        dataGridView1.TabIndex = 0;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Controls.Add(tabControl1);
        Name = "Form1";
        Text = "Form1";
        tabControl1.ResumeLayout(false);
        tabPage1.ResumeLayout(false);
        tabPage1.PerformLayout();
        tabPage2.ResumeLayout(false);
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
    private Label label1;
    private TextBox textBox4;
    private Label label4;
    private TextBox textBox3;
    private Label label3;
    private Button ProcessJD_btn;
    private TextBox JobUrl_txt;
    private Label label2;
    private TextBox textBox1;
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
    private ProgressBar progressBar1;
    private RichTextBox FeedbackArea_txt;
    private Button CreatePDF_btn;
    private TabPage tabPage3;
    private DataGridView dataGridView1;
}
