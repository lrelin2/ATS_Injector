using System;
using System.Windows.Forms;

namespace ATSInjector
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Remeber to get the single .exe run this command in the terminal
            //msbuild /t:Publish /p:Configuration=Release
            //Then go to [[current dir]]\bin\Release\net10.0-windows\win-x64\publish
            //and you will have the .exe file!
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
