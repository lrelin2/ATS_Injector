using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace ATS_Injector
{
    internal class PersistanceSettings
    {
        //private readonly string _filePath;
        private static string SettingsFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ATS_Tokens");
        private static string outFile = Path.Combine(SettingsFolder, "UserSettings.json");

        public PersistanceSettings(out string ErrorMsg)
        {
            ErrorMsg = string.Empty;
            try
            {
                if (File.Exists(outFile) == false)
                {
                    string updatedJson = JsonSerializer.Serialize(UserSettings.CreateDefault(), new JsonSerializerOptions { WriteIndented = true });
                    File.WriteAllText(outFile, updatedJson);
                    Console.WriteLine("File updated successfully.");
                }
            }
            catch (Exception ex) { ErrorMsg = ex.Message; }

        }

        public UserSettings GetSettings()
        {
            if (!File.Exists(outFile))
            {
                Console.WriteLine("Settings file not found. Returning default settings.");
                return UserSettings.CreateDefault();
            }

            try
            {
                string jsonString = File.ReadAllText(outFile);
                // Deserialize the JSON back into a C# object
                return JsonSerializer.Deserialize<UserSettings>(jsonString);
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error parsing settings file: {ex.Message}");
                return null;
            }
        }

        public void UpdateData(UserSettings newSettings)
        {
            string updatedJson = JsonSerializer.Serialize(newSettings, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(outFile, updatedJson);
        }
    }

    public class UserSettings
    {
        public string ResumePath { get; set; }
        public string OutputFolderPath { get; set; }
        public string OutputFileName { get; set; }
        public bool WarnOverWriteOutputFile { get; set; }
        public API_AI_ID PreviousToken { get; set; }

        public static UserSettings CreateDefault()
        {
            string userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string downloadsPath = Path.Combine(userProfile, "Downloads", "MyResumePDF.pdf");
            string outputFolderPath = Path.Combine(userProfile, "Downloads");
            bool warnOverWriteOutputFile = true;
            //string outputFileName = "ATS_injectedResume.pdf";

            return new UserSettings { ResumePath = downloadsPath, PreviousToken = API_AI_ID.NO_TOKEN, OutputFolderPath = outputFolderPath , WarnOverWriteOutputFile =warnOverWriteOutputFile};
        }
    }
}
