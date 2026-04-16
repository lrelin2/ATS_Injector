using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UglyToad.PdfPig.Core;
using PdfRectangle = UglyToad.PdfPig.Core.PdfRectangle;
using XPoint = PdfSharpCore.Drawing.XPoint;
using XGraphics = PdfSharpCore.Drawing.XGraphics;
using XFont = PdfSharpCore.Drawing.XFont;

namespace ATS_Injector
{
    internal class PDFInjectorPdfPig
    {
        private string inputPath;
        private string outputPath;
        private string[] BulletPoints;
        private static string errorMsg = string.Empty;

        private const double FontSize = 0.1;
        private const double BufferPoints = 2.0;

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
            errorMsg = string.Empty;
        }

        public async Task<bool> StartProcess()
        {
            return await Task.Run(() =>
            {
                InjectTINYTEXT();
                return true;
            });
        }

        /// <summary>
        /// Injects an array of strings into a PDF at off-screen coordinates.
        /// </summary>
        private void InjectTINYTEXT()
        {
            List<List<PdfPoint>> PDFareasToPrintList = new List<List<PdfPoint>>();
            List<string[]> textToPrintLists = new List<string[]>();

            //Generate a list of white spaces available per page
            List<bool[,]> gridList = generateGridList();

            //Next step is figure out where you can stick the text. If you run out of space to inject the text, go to next page.
            for (int i=0; i< gridList.Count; i++)
            {
                List<PdfPoint> placements = FindPlacement(gridList[i], out string[] listToPrint);
                PDFareasToPrintList.Add(placements);
                textToPrintLists.Add(listToPrint);
            }

            AddTextWithPdfSharp(PDFareasToPrintList, textToPrintLists);
        }

        public void AddTextWithPdfSharp(List<List<PdfPoint>> PdfPointlocations, List<string[]> texts)
        {
            File.Copy(inputPath, outputPath, true);

            List<List<XPoint>> locations = ConvertPDFData(PdfPointlocations);

            using (PdfSharpCore.Pdf.PdfDocument document = PdfSharpCore.Pdf.IO.PdfReader.Open(outputPath, PdfSharpCore.Pdf.IO.PdfDocumentOpenMode.Modify))
            {
                XFont font = new XFont("Helvetica", 0.1, PdfSharpCore.Drawing.XFontStyle.Regular);

                for (int i = 0; i < document.PageCount; i++)
                {
                    // Check if we have data for this page (0-indexed in PdfSharp)
                    if (i >= texts.Count || i >= locations.Count) continue;

                    PdfSharpCore.Pdf.PdfPage page = document.Pages[i];
                    string[] txtToPrint = texts[i];
                    List<XPoint> locToPrint = locations[i];

                    using (XGraphics gfx = XGraphics.FromPdfPage(page, PdfSharpCore.Drawing.XGraphicsPdfPageOptions.Append))
                    {
                        for (int j = 0; j < txtToPrint.Length; j++)
                        {
                            gfx.DrawString(txtToPrint[j], font, PdfSharpCore.Drawing.XBrushes.Transparent, locToPrint[j]);
                        }
                    }
                }
                document.Save(outputPath);
            }
        }

        private List<List<XPoint>> ConvertPDFData(List<List<PdfPoint>> pdfPointlocations)
        {
            List<List<XPoint>> returningData = new List<List<XPoint>>();
            using (PdfSharpCore.Pdf.PdfDocument document = PdfSharpCore.Pdf.IO.PdfReader.Open(inputPath, PdfSharpCore.Pdf.IO.PdfDocumentOpenMode.ReadOnly))
            {
                for(int i=0; i<pdfPointlocations.Count; i++)
                {
                    List<PdfPoint> outerData = pdfPointlocations[i];
                    PdfSharpCore.Pdf.PdfPage page = document.Pages[i];
                    double pageHeight = page.Height.Point;
                    List<XPoint> TempOuterData = new List<XPoint>();
                    foreach (PdfPoint data in outerData)
                    {
                        TempOuterData.Add(ToXPoint(data, pageHeight));
                    }

                    returningData.Add(TempOuterData);
                }

            }
            return returningData;
        }

        /// <summary>
        /// Converts a PdfPig point (Bottom-Left 0,0) to a PDFsharp point (Top-Left 0,0).
        /// </summary>
        /// <param name="pigPoint">The point from PdfPig.</param>
        /// <param name="pageHeight">The height of the page in points.</param>
        private XPoint ToXPoint(PdfPoint pigPoint, double pageHeight)
        {
            // X remains the same
            // Y is inverted: PageHeight - PigY
            return new XPoint(pigPoint.X, pageHeight - pigPoint.Y);
        }


        private List<PdfPoint> FindPlacement(bool[,] grid, out string[] listToPrint)
        {
            listToPrint = new string[] { };
            //you need to loop throughthe current grid
            //find empty spaces, and figure out what needs to be filled in, from the BulletPoints array
            //As you are creating the new text pdfPoint areas, remove the string from BulletPoints
            //and place those strings into listToPrint
            //The goal is that in the List of PdfPoint
            //there shall be a matching sequence of Pdfpoints with corresponding string that matches the size that is to be printed.
            List<PdfPoint> returningData = new List<PdfPoint>();
            List<string> compileList = new List<string>();
            int gridWidth = grid.GetLength(0);
            int gridHeight = grid.GetLength(1);

            // Required height includes the 0.1 font plus the 2-unit buffer on top and bottom
            int requiredHeight = (int)Math.Ceiling(FontSize + (BufferPoints * 2));

            foreach (string text in BulletPoints)
            {
                // Scan from top to bottom
                for (int y = gridHeight - requiredHeight; y > 0; y--)
                {
                    // Since the entire row is either empty or full, we only need to check x=0
                    if (!grid[0, y])
                    {
                        // Check if there's enough vertical clearance for the requiredHeight
                        if (IsVerticalSpanEmpty(grid, y, requiredHeight))
                        {
                            // Place at x=5 (small indent) or wherever you prefer
                            returningData.Add(new PdfPoint(5, y + BufferPoints));
                            compileList.Add(text);

                            // Mark the rows we just used so the next string doesn't overlap
                            MarkRangeOccupied(grid, y, requiredHeight, gridWidth);
                            break;
                        }
                    }
                }
            }

            if(compileList.Count < BulletPoints.Length)
            {
                //if here, we took SOME, but not ALL of the bullet points out of the stirng array....
                BulletPoints = BulletPoints[compileList.Count..];
                listToPrint = compileList.ToArray();
            }
            else
            {
                //if here, you probably have consumed all of the words
                listToPrint = compileList.ToArray();
                BulletPoints = new string[] { };
            }
            return returningData;
        }

        private bool IsVerticalSpanEmpty(bool[,] grid, int startY, int height)
        {
            for (int y = startY; y < startY + height; y++)
            {
                if (grid[0, y]) return false;
            }
            return true;
        }

        private void MarkRangeOccupied(bool[,] grid, int startY, int height, int width)
        {
            for (int y = startY; y < startY + height; y++)
            {
                for (int x = 0; x < width; x++) grid[x, y] = true;
            }
        }


        private List<bool[,]> generateGridList()
        {
            List<bool[,]> gridList = new List<bool[,]>();
            int totalPages = -1;
            using (UglyToad.PdfPig.PdfDocument document = UglyToad.PdfPig.PdfDocument.Open(inputPath))
            {
                totalPages = document.GetPages().Count();
                foreach (UglyToad.PdfPig.Content.Page page in document.GetPages())
                {
                    int width = (int)Math.Ceiling(page.Width);
                    int height = (int)Math.Ceiling(page.Height);
                    bool[,] currentGrid = new bool[width, height];

                    var elements = new List<PdfRectangle>();
                    elements.AddRange(page.Letters.Select(l => l.BoundingBox));
                    elements.AddRange(page.GetImages().Select(i => i.BoundingBox));

                    foreach (var path in page.Paths)
                    {
                        if (!path.IsFilled && !path.IsStroked) continue;
                        var bbox = path.GetBoundingRectangle();
                        if (bbox.HasValue) elements.Add(bbox.Value);
                    }

                    // 2. Mark the ENTIRE ROW as occupied if an element exists there
                    foreach (var rect in elements)
                    {
                        MarkEntireRowOccupied(currentGrid, rect, width, height);
                    }
                    gridList.Add(currentGrid);
                }//end of for loop per page
            }

            if(totalPages == -1)
            {
                errorMsg += "Host PDF document did not have any pages?";
                gridList = null;
            }
            else if (totalPages != gridList.Count)
            {
                //something terrible has happened...
                errorMsg += "Page count error happened, setting grid count to zero";
                gridList = null;
            }

            return gridList;
        }

        private void MarkEntireRowOccupied(bool[,] grid, PdfRectangle rect, int maxWidth, int maxHeight)
        {
            // Find the vertical span of the object
            int yMin = Math.Max(0, (int)Math.Floor(rect.Bottom));
            int yMax = Math.Min(maxHeight, (int)Math.Ceiling(rect.Top));

            // Mark the entire width (0 to maxWidth) for these Y coordinates
            for (int y = yMin; y < yMax; y++)
            {
                for (int x = 0; x < maxWidth; x++)
                {
                    grid[x, y] = true;
                }
            }
        }
    }
}
