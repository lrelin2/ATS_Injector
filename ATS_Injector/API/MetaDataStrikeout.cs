
using ATSInjector;
using org.pdfclown.documents;
using org.pdfclown.documents.interchange.metadata;
using System;

namespace ATS_Injector.API
{
    internal class MetaDataStrikeout
    {
        private string filePath;
        private UserSettings metaData;
        //private string tempExe;
        public MetaDataStrikeout(string filePath, UserSettings metaData)
        {
            this.filePath = filePath;
            this.metaData = metaData;
        }

        public void run()
        {
            try
            {
                using (org.pdfclown.files.File file = new org.pdfclown.files.File(filePath))
                {
                    Document document = file.Document;

                    //-----------------------------------------------------
                    // Clear Document Information
                    //-----------------------------------------------------

                    Information info = document.Information;

                    if (info != null)
                    {
                        if (string.IsNullOrEmpty(metaData.meta_Title) == false)
                            info.Title = metaData.meta_Title;

                        if (string.IsNullOrEmpty(metaData.meta_Subject) == false)
                            info.Subject = metaData.meta_Subject;

                        if (string.IsNullOrEmpty(metaData.meta_Author) == false)
                            info.Author = metaData.meta_Author;

                        if (string.IsNullOrEmpty(metaData.meta_Creator) == false)
                            info.Creator = metaData.meta_Creator;

                        if (string.IsNullOrEmpty(metaData.meta_Producer) == false)
                            info.Producer = metaData.meta_Producer;

                        if (string.IsNullOrEmpty(metaData.meta_Keywords) == false)
                            info.Keywords = metaData.meta_Keywords;

                        //add a little razzale dazzle common folk stuff here
                        if (string.IsNullOrEmpty(info.Producer))
                            info.Producer = "Adobe PDF printer";    //most typical source....

                    }

                    //-----------------------------------------------------
                    // Remove ViewerPreferences
                    //-----------------------------------------------------

                    org.pdfclown.objects.PdfDictionary catalog = document.Document.BaseDataObject;//.Catalog.BaseDataObject;

                    if (catalog != null)
                    {
                        catalog.Remove(org.pdfclown.objects.PdfName.ViewerPreferences);
                    }

                    //-----------------------------------------------------
                    // Save back over the original PDF
                    //-----------------------------------------------------

                    file.Save();

                    Console.WriteLine("PDF metadata successfully cleared.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error clearing PDF metadata.");
                Console.WriteLine("Message:");
                Console.WriteLine(ex.Message);

                Console.WriteLine();

                Console.WriteLine("Exception Type:");
                Console.WriteLine(ex.GetType().FullName);

                Console.WriteLine();

                Console.WriteLine("Stack Trace:");
                Console.WriteLine(ex.StackTrace);

                if (ex.InnerException != null)
                {
                    Console.WriteLine();

                    Console.WriteLine("Inner Exception:");
                    Console.WriteLine(ex.InnerException.Message);
                    Console.WriteLine(ex.InnerException.StackTrace);
                }
            }
        }

        //private bool StripMetaDataAndJunk()
        //{
        //    //this function shall do the following
        //    //1 - Remove viewer prefrence data. This is the you start at page 2 or something
        //    //This will also remove if the thing was stuck wanting to print, I don't know how I got that mess up.
        //    //2 - It will update the meta data, if user entered new meta data to enter.
        //    //If empty, do not make any changes
        //    //Modify PDF meta data, and file meta data as well if need be.

        //    //PDF meta data step 1!
        //    PdfDocument document = null;
        //    try
        //    {
        //        // Open the document
        //        document = PdfReader.Open(filePath, PdfDocumentOpenMode.Modify);

        //        // Access the Root dictionary of the PDF via Internals
        //        PdfDictionary root = document.Internals.Catalog;

        //        // Check if ViewerPreferences exists and remove it
        //        if (root.Elements.ContainsKey("/ViewerPreferences"))
        //        {
        //            root.Elements.Remove("/ViewerPreferences");
        //        }

        //        if (String.IsNullOrEmpty(metaData.meta_Title) == false)
        //            document.Info.Title = metaData.meta_Title;
        //        if (String.IsNullOrEmpty(metaData.meta_Author) == false)
        //            document.Info.Author = metaData.meta_Author;
        //        if (String.IsNullOrEmpty(metaData.meta_Subject) == false)
        //            document.Info.Subject = metaData.meta_Subject;
        //        if (String.IsNullOrEmpty(metaData.meta_Creator) == false)
        //            document.Info.Creator = metaData.meta_Creator;

        //        //Read only????
        //        //document.Info.Producer = "";

        //        // Save the cleaned document
        //        document.Save(filePath);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error processing file: {ex.Message}");
        //        Console.WriteLine("--- Stack Trace ---");
        //        Console.WriteLine(ex.StackTrace);
        //    }
        //    finally
        //    {
        //        // Always clean up resources
        //        document?.Dispose();
        //    }

        //    //Step 2, update the file meta data as well, cause why not?
        //    //NVM to much extra work, and I don't want more libraries...

        //    return false;
        //}


        //public void run()
        //{
        //    //this is going to the main proces that will run the external exiftool tool
        //    //steps:
        //    //1 - rummage through the resource directory, find the .exe file
        //    //2 - extract into the temp directory
        //    //3 - run the tool as desired
        //    //4 - delete the temp directory, as well as the tool
        //    //5 - profit

        //    //run the normal PDF meta data thing
        //    StripMetaDataAndJunk();

        //    //now strip the part that we can't anymore...

        //    Assembly assembly = Assembly.GetExecutingAssembly();

        //    //step 1
        //    // List all embedded resources
        //    Console.WriteLine("Embedded Resources:");
        //    string[] resources = assembly.GetManifestResourceNames();
        //    string keepText = "";

        //    for (int i = 0; i < resources.Length; i++)
        //    {
        //        Console.WriteLine("  " + resources[i]);
        //        keepText += $"|{i} : {resources[i]}  |";
        //    }
        //    //
        //    // Replace this with the resource name you find above
        //    string resourceName = "ATS_Injector.Resources.exiftool.exe";

        //    //step 2
        //    //should be false, but thing break you know....
        //    if (File.Exists(tempExe) == false)
        //    {
        //        using (Stream resource = assembly.GetManifestResourceStream(resourceName))
        //        {
        //            if (resource == null)
        //            {
        //                throw new Exception("Embedded resource not found: " + resourceName);
        //            }

        //            using (FileStream output = File.Create(tempExe))
        //            {
        //                resource.CopyTo(output);
        //            }
        //        }
        //    }

        //    string newProducer = string.Empty;
        //    if (string.IsNullOrEmpty(metaData.meta_Producer) == false)
        //        newProducer = metaData.meta_Producer;

        //    string argsToSend = $"-overwrite_original -Producer=\"{newProducer}\" \"{metaData.OutputFileName}\"";
        //    string outputStr;
        //    string errorsStr;
        //    //step 3
        //    // Execute ExifTool
        //    try
        //    {
        //        ProcessStartInfo psi = new ProcessStartInfo
        //        {
        //            FileName = tempExe,
        //            Arguments = argsToSend,
        //            UseShellExecute = false,
        //            CreateNoWindow = true,
        //            RedirectStandardOutput = true,
        //            RedirectStandardError = true
        //        };

        //        using (Process process = Process.Start(psi))
        //        {
        //            if (process == null)
        //            {
        //                Console.WriteLine("Failed to start process.");
        //                return;
        //            }

        //            outputStr = process.StandardOutput.ReadToEnd();
        //            errorsStr = process.StandardError.ReadToEnd();

        //            process.WaitForExit();

        //            Console.WriteLine("Exit Code: " + process.ExitCode);

        //            if (!string.IsNullOrWhiteSpace(outputStr))
        //            {
        //                Console.WriteLine("Output:");
        //                Console.WriteLine(outputStr);
        //            }

        //            if (!string.IsNullOrWhiteSpace(errorsStr))
        //            {
        //                Console.WriteLine("Errors:");
        //                Console.WriteLine(errorsStr);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Exception occurred while running ExifTool.");
        //        Console.WriteLine("Message: " + ex.Message);
        //        Console.WriteLine("Type: " + ex.GetType().FullName);
        //        Console.WriteLine("Stack Trace:");
        //        Console.WriteLine(ex.StackTrace);

        //        if (ex.InnerException != null)
        //        {
        //            Console.WriteLine("Inner Exception:");
        //            Console.WriteLine(ex.InnerException.Message);
        //        }
        //    }
        //    int absdafds = 5;
        //    //step 4
        //    try
        //    {
        //        Thread.Sleep(100);
        //        File.Delete(tempExe);
        //    }
        //    catch (Exception)
        //    {

        //    }
        //    int ab = 4;
        //    //step 5, profit!
        //}
    }
}
