using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.Content;
using PdfSharpCore.Pdf.Content.Objects;
using PdfSharpCore.Pdf.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ATS_Injector
{
    internal class PDFInjector
    {

        private InjectionMethod PDFAction;
        private string inputPath;
        private string outputPath;
        private string[] BulletPoints;

        /// <summary>
        /// Injection constructor
        /// </summary>
        /// <param name="PDFAction">Enum that lets user decide what PDF injection method to use.</param>
        /// <param name="inputPath">Path to the existing PDF</param>
        /// <param name="outputPath">Path where the new PDF will be saved</param>
        /// <param name="RawData">String that needs to be parsed.</param>
        public PDFInjector(InjectionMethod PDFAction, string inputPath, string outputPath, string RawData)
        {
            this.PDFAction = PDFAction;
            this.inputPath = inputPath;
            this.outputPath = outputPath;
            this.BulletPoints = AISplit(RawData);
        }

        /// <summary>
        /// Injection constructor
        /// </summary>
        /// <param name="PDFAction">Enum that lets user decide what PDF injection method to use.</param>
        /// <param name="inputPath">Path to the existing PDF</param>
        /// <param name="outputPath">Path where the new PDF will be saved</param>
        /// <param name="BulletPoints">Array of strings to embed</param>
        public PDFInjector(InjectionMethod PDFAction, string inputPath, string outputPath, string[] BulletPoints)
        {
            this.PDFAction = PDFAction;
            this.inputPath = inputPath;
            this.outputPath = outputPath;
            this.BulletPoints = BulletPoints;
        }


        public async Task<bool> StartProcess()
        {
            bool returningBool = false;
            if (File.Exists(inputPath))
            {
                bool result = await Task.Run(() =>
                {
                    bool retValue = false;
                    switch (PDFAction)
                    {
                        case InjectionMethod.OFFSCREEN:
                            PDFInjectorPdfSharp pSharp = new PDFInjectorPdfSharp(inputPath, outputPath, BulletPoints);
                            retValue = pSharp.StartProcess().GetAwaiter().GetResult();
                            break;
                        case InjectionMethod.TINYFONT:
                            PDFInjectorPdfPig pPig = new PDFInjectorPdfPig(inputPath, outputPath, BulletPoints);
                            retValue = pPig.StartProcess().GetAwaiter().GetResult();
                            break;
                    }
                    return retValue;
                });

                returningBool = result;
            }
            return returningBool;
        }

        public enum InjectionMethod
        {
            OFFSCREEN,
            TINYFONT,
        }

        private string[] AISplit(string RawData)
        {
            return RawData.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries)
            .Where(line => line.TrimStart().StartsWith("*"))
            .Select(line => line.TrimStart().TrimStart('*').Trim())
            .Where(line => !string.IsNullOrEmpty(line))
            .ToArray();
        }

    }
}
