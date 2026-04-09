using PdfSharpCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UglyToad.PdfPig.Content;
using UglyToad.PdfPig.Core;
using UglyToad.PdfPig.Writer;

namespace ATS_Injector
{
    internal class PDFInjectorPdfPig
    {
        private string inputPath;
        private string outputPath;
        private string[] BulletPoints;

        /// <summary>
        /// Injection constructor
        /// </summary>
        /// <param name="PDFAction">Enum that lets user decide what PDF injection method to use.</param>
        /// <param name="inputPath">Path to the existing PDF</param>
        /// <param name="outputPath">Path where the new PDF will be saved</param>
        /// <param name="BulletPoints">Array of strings to embed</param>
        public PDFInjectorPdfPig(string inputPath, string outputPath, string[] BulletPoints)
        {
            this.inputPath = inputPath;
            this.outputPath = outputPath;
            this.BulletPoints = BulletPoints;
        }

        public async Task<bool> StartProcess()
        {
            return await Task.Run(() =>
            {
                InjectOFFSCREEN();
                return true;
            }); ;
        }


        /// <summary>
        /// Injects an array of strings into a PDF at off-screen coordinates.
        /// </summary>
        private void InjectOFFSCREEN()
        {

            PdfDocumentBuilder builder = new PdfDocumentBuilder();
            PdfPageBuilder page = builder.AddPage(UglyToad.PdfPig.Content.PageSize.A4);
            // 1. Open the existing document in Modify mode
            //using (PdfDocument document = PdfReader.Open(inputPath, PdfDocumentOpenMode.Modify))
            //{
            //    // 2. We only need to attach this to the first page (or any single page)
            //    PdfPage page = document.Pages[0];
            //    double width = page.Width.Value;
            //    double height = page.Height.Value;
            //    int offestVal = 100;

            //    using (XGraphics gfx = XGraphics.FromPdfPage(page, XGraphicsPdfPageOptions.Append))
            //    {
            //    }
            //}
        }
    }
}
