using System;
using System.IO;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.IO;

namespace ATS_Injector
{
    internal class PDFInjector
    {

        public PDFInjector(string inputFile, string outFile, string injectionText)
        {

        }

        /// <summary>
        /// Injects an array of strings into a PDF at off-screen coordinates.
        /// </summary>
        /// <param name="inputPath">Path to the existing PDF</param>
        /// <param name="outputPath">Path where the new PDF will be saved</param>
        /// <param name="hiddenMetadata">Array of strings to embed</param>
        public static void InjectHiddenText(string inputPath, string outputPath, string[] hiddenMetadata)
        {
            if (!File.Exists(inputPath))
                throw new FileNotFoundException("Input PDF not found.");

            // 1. Open the existing document in Modify mode
            using (PdfDocument document = PdfReader.Open(inputPath, PdfDocumentOpenMode.Modify))
            {
                // 2. We only need to attach this to the first page (or any single page)
                PdfPage page = document.Pages[0];

                // 3. Create graphics object. 'Append' ensures we don't overwrite existing content.
                using (XGraphics gfx = XGraphics.FromPdfPage(page, XGraphicsPdfPageOptions.Append))
                {
                    // Define a standard font (required for PDFSharp to write the stream)
                    XFont font = new XFont("Arial", 8);
                    XBrush brush = XBrushes.Transparent; // Added layer of "invisibility"

                    // 4. Iterate through the array and print each string "off-screen"
                    // Standard A4 is ~595x842. Points like 5000x5000 are miles off the canvas.
                    double offScreenX = 5000;
                    double offScreenY = 5000;

                    foreach (string data in hiddenMetadata)
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
