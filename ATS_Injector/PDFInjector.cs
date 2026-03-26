using System;
using System.IO;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.IO;

namespace ATS_Injector
{
    internal class PDFInjector
    {

        //public PDFInjector(string inputFile, string outFile, string injectionText)
        //{

        //}


        /// <summary>
        /// Injects an array of strings into a PDF at off-screen coordinates.
        /// </summary>
        /// <param name="inputPath">Path to the existing PDF</param>
        /// <param name="outputPath">Path where the new PDF will be saved</param>
        /// <param name="RawData">String stream that needs to be split</param>
        public static void InjectHiddenText(string inputPath, string outputPath, string RawData)
        { 
            //List<string> lines = new List<string>();
            string[] data = RawData
            // 1. Split into lines, removing empty entries automatically
            .Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries)
            // 2. Select only lines that start with * (trimmed)
            .Where(line => line.TrimStart().StartsWith("*"))
            // 3. Remove the * symbol and trim whitespace from the remaining text
            .Select(line => line.TrimStart().TrimStart('*').Trim())
            // 4. Ensure we don't return empty strings if a line was just "*"
            .Where(line => !string.IsNullOrEmpty(line))
            .ToArray();
            InjectHiddenText(inputPath, outputPath, data);
        }
        /// <summary>
        /// Injects an array of strings into a PDF at off-screen coordinates.
        /// </summary>
        /// <param name="inputPath">Path to the existing PDF</param>
        /// <param name="outputPath">Path where the new PDF will be saved</param>
        /// <param name="BulletPoints">Array of strings to embed</param>
        public static void InjectHiddenText(string inputPath, string outputPath, string[] BulletPoints)
        {
            if (!File.Exists(inputPath))
                throw new FileNotFoundException("Input PDF not found.");

            // 1. Open the existing document in Modify mode
            using (PdfDocument document = PdfReader.Open(inputPath, PdfDocumentOpenMode.Modify))
            {
                // 2. We only need to attach this to the first page (or any single page)
                PdfPage page = document.Pages[0];
                double width = page.Width.Value;
                double height = page.Height.Value;
                int offestVal = 100;

                // 3. Create graphics object. 'Append' ensures we don't overwrite existing content.
                using (XGraphics gfx = XGraphics.FromPdfPage(page, XGraphicsPdfPageOptions.Append))
                {
                    // Define a standard font (required for PDFSharp to write the stream)
                    XFont font = new XFont("Arial", 8);
                    XBrush brush = XBrushes.Transparent; // Added layer of "invisibility"

                    // 4. Iterate through the array and print each string "off-screen"
                    double offScreenX = width+ offestVal;
                    double offScreenY = height + offestVal;

                    foreach (string data in BulletPoints)
                    {
                        if (string.IsNullOrWhiteSpace(data)) continue;

                        gfx.DrawString(data, font, brush, new XPoint(offScreenX, offScreenY));

                        // Increment Y slightly for each entry so they are distinct in the internal stream
                        offScreenY += 10;
                    }
                }

                // 5. Save the updated PDF to the new location
                document.Save(outputPath);
            }
        }
    }
}
