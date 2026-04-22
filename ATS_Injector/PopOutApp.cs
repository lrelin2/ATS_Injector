using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ATS_Injector
{
    public class PopOutApp
    {
        /// <summary>
        /// Shows an input dialog with a prompt, and optional window & prompt icons.
        /// </summary>
        /// <param name="title">Window title.</param>
        /// <param name="prompt">Prompt text shown above the input.</param>
        /// <param name="AIModel">Which AI Model is being configured.</param>
        /// <param name="initialText">Optional initial value inside the TextBox where [API model] token goes in.</param>
        /// <param name="windowIcon">Optional Icon for the window (title bar).</param>
        /// <param name="promptIcon">Optional image shown to the left of the prompt.</param>
        /// <returns>Entered string if Save is clicked; null if Cancel is clicked.</returns>
        public static string PopOutBox(string title,
                                  string prompt,
                                  API_AI_ID AIModel,
                                  string initialText = "",
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
                                  Icon? windowIcon = null,
                                  Image? promptIcon = null
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
                                  )
        {
            // --- Form ---
            var form = new Form
            {
                Text = $"({AIModel}){ title }",
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MinimizeBox = false,
                MaximizeBox = false,
                BackColor = Color.White,
                Padding = new Padding(15),
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };

            if (windowIcon != null)
                form.Icon = windowIcon;
            else
                form.Icon = SystemIcons.Shield; // generic input-ish icon


            var lbl = new Label
            {
                Text = prompt,
                Font = new Font("Segoe UI", 10),
                AutoSize = true,
                MaximumSize = new Size(480, 0) // wrap long prompts
            };

            var txt = new TextBox
            {
                Font = new Font("Segoe UI", 10),
                Width = 320,
                Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top,
                Text = initialText
            };

            // Buttons
            var btnOk = new Button
            {
                Text = "Save",
                Font = new Font("Segoe UI", 9),
                DialogResult = DialogResult.OK,
                FlatStyle = FlatStyle.System,
                AutoSize = true
            };

            var btnCancel = new Button
            {
                Text = "Cancel",
                Font = new Font("Segoe UI", 9),
                DialogResult = DialogResult.Cancel,
                FlatStyle = FlatStyle.System,
                AutoSize = true
            };


            // --- Layout ---
            // Top row: [Icon][Prompt]
            var header = new TableLayoutPanel
            {
                ColumnCount = 2,
                RowCount = 1,
                Dock = DockStyle.Top,
                AutoSize = true,
                Padding = new Padding(0, 0, 0, 8)
            };
            header.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            header.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            header.Controls.Add(lbl, 1, 0);

            // Middle: TextBox
            var middle = new Panel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                Padding = new Padding(0, 0, 0, 8)
            };
            txt.Dock = DockStyle.Top;
            middle.Controls.Add(txt);

            // Bottom: Buttons aligned right
            var buttons = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.RightToLeft,
                Dock = DockStyle.Top,
                AutoSize = true
            };

            buttons.Controls.Add(btnCancel);
            buttons.Controls.Add(btnOk);

            // Container stack
            var stack = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                RowCount = 3,
                ColumnCount = 1
            };
            stack.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            stack.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            stack.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            stack.Controls.Add(header, 0, 0);
            stack.Controls.Add(middle, 0, 1);
            stack.Controls.Add(buttons, 0, 2);

            form.Controls.Add(stack);

            form.AcceptButton = btnOk;
            form.CancelButton = btnCancel;

            // Show and return
            if(form.ShowDialog() == DialogResult.OK)
            {
                WriteAPIToken(AIModel, txt.Text, out string errorMsg);
            }
            //if you need to return the API token, you can do it here as well I guess?
            return string.Empty;
        }

        public static string PopOutBox_automated(API_AI_ID AIModel)
        {
            string title = AIModel.ToString();
            string prompt = $"Enter token from {title} AI service";
            if (API_Token_Written(AIModel) == false)
            {
                title = $"Save your {title} AI API Token";
            }
            else{
                title = $"Update your {title} API Token";
                prompt = $"Enter Updated token from {title} AI service";
            }
            return PopOutBox(title: title, prompt:prompt, AIModel:AIModel);
        }

        private static string ATS_Folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ATS_Tokens");
        public static string GetATSFolder() { return ATS_Folder; }  
        /// <summary>
        /// Checks if the token has been written to the file system, under C:\Users\<Username>\AppData\Roaming\ATS_Tokens\AI_model.txt
        /// </summary>
        /// <returns>True if token file exists AND has contents, false otherwise</returns>
        public static bool API_Token_Written(API_AI_ID AIModel)
        {
            bool returningBool = false;
            try
            {
                if (Directory.Exists(ATS_Folder))
                {
                    var info = new FileInfo(Path.Combine(ATS_Folder, $"{AIModel.ToString()}.txt"));
                    return info.Exists && info.Length > 0;
                }
            }
            catch
            {
                //not good, should never happen
                //when user attempts to write the file, they will know with error message..
                returningBool = false;
            }
            return returningBool;
        }

        /// <summary>
        /// Get the contents of the token flat file. This contains your personal [API] token.
        /// </summary>
        /// <param name="rawToken">returns the string literal to be used for [API] AI access</param>
        /// <param name="errorMsg">Returns any IO error message, with stack trace.</param>
        /// <returns>If</returns>
        public static bool GetAPI_Token(API_AI_ID AIModel, out string rawToken, out string errorMsg)
        {
            bool returningBool = false;
            errorMsg = "Empty API token error msg";
            rawToken = string.Empty;
            if (API_Token_Written(AIModel))
            {
                try
                {
                    rawToken = File.ReadAllText(Path.Combine(ATS_Folder, $"{AIModel.ToString()}.txt"));
                    if (string.IsNullOrEmpty(rawToken) == false)
                    {
                        if (rawToken.Length >= 30)
                        {
                            returningBool = true;
                        }
                        else
                        {
                            errorMsg = $"Token in the file, [{Path.Combine(ATS_Folder, $"{AIModel.ToString()}.txt")}], is not of valid length? Looking for something about 60 characters long, yours was [{rawToken.Length}]";

                        }
                    }
                    else
                    {
                        errorMsg = $"Token provide in the file, [{Path.Combine(ATS_Folder, $"{AIModel.ToString()}.txt")}], is empty! ";
                    }

                }
                catch (Exception ex)
                {
                    errorMsg = $"There was an error getting your API token data from the file:[{Path.Combine(ATS_Folder, $"{AIModel.ToString()}.txt")}]\r\nError Stack Msg:[{ex.StackTrace}]";
                    rawToken = string.Empty;
                    returningBool = false;
                }
            }
            return returningBool;
        }

        public static bool WriteAPIToken(API_AI_ID AIModel, string rawToken, out string errorMsg)
        {
            bool returningBool = true;
            errorMsg = string.Empty;
            bool writeContents = true;
            try
            {
                if (Directory.Exists(ATS_Folder) == false)
                {
                    Directory.CreateDirectory(ATS_Folder);
                    //presume system is slow, maybe anti virus doesn't like me
                    Thread.Sleep(100);
                }
            }
            catch (Exception ex)
            {
                errorMsg = $"Could not read or write to the directory :[{ATS_Folder}]\r\nError Stack Msg:[{ex.StackTrace}]";
                return false;
            }

            string absFilePath = Path.Combine(ATS_Folder, $"{AIModel.ToString()}.txt");
            if (File.Exists(absFilePath))
            {
                //file exists?
                //check contents see if they match. If they match user dumb
                string localData = string.Empty;
                try
                {
                    localData = File.ReadAllText(absFilePath);
                }
                catch (Exception ex)
                {
                    errorMsg = $"Error reading in data from the file:[{absFilePath}]\r\nError Stack Msg:[{ex.StackTrace}]";
                    return false;
                }

                if (string.IsNullOrEmpty(localData))
                {
                    //empty file? Should never happen but okay I guess....
                    //later down stream token shall be written 
                }
                else if (string.Equals(localData, rawToken, StringComparison.Ordinal))
                {
                    writeContents = false;
                }
            }

            if (writeContents)
            {
                try
                {
                    File.WriteAllText(absFilePath, rawToken);
                }
                catch (Exception ex)
                {
                    errorMsg = $"Could not write API token to :[{absFilePath}]\r\nError Stack Msg:[{ex.StackTrace}]";
                    return false;
                }
            }
            return returningBool;
        }

    }
    public enum API_AI_ID
    {
        ChatGPT,
        Gemini,
        Claude,
        NO_TOKEN
    }
}
