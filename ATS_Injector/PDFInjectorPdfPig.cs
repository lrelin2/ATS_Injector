using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;
using UglyToad.PdfPig.Core;
using UglyToad.PdfPig.Fonts.Standard14Fonts;
using UglyToad.PdfPig.Writer;
using PdfRectangle = UglyToad.PdfPig.Core.PdfRectangle;

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
            }); ;
        }

        private static long foundCnt = 0;
        private static long notFoundCnt = 0;
        /// <summary>
        /// Injects an array of strings into a PDF at off-screen coordinates.
        /// </summary>
        private void InjectTINYTEXT()
        {
            
            List<bool[,]> gridList = generateGridList();
            List<List<PdfPoint>> PDFareasToPrintList = new List<List<PdfPoint>>();
            List<string[]> textToPrintLists = new List<string[]>();

            for (int i=0; i< gridList.Count; i++)
            {
                List<PdfPoint> placements = FindPlacement(gridList[i], out string[] listToPrint);
                //yeah only doing one page for now.....
                PDFareasToPrintList.Add(placements);
                textToPrintLists.Add(listToPrint);
                break;
            }


            //string[] txtToPrint = textToPrintLists[0];
            //List<PdfPoint> LocToPrint = PDFareasToPrintList[0];
            AddTextWithPdfSharp(PDFareasToPrintList, textToPrintLists);
            //AddTextToPdf(PDFareasToPrintList, textToPrintLists);

            long curCng = foundCnt;
            int ab = 54;
           // Environment.Exit(0);
        }

        public void AddTextWithPdfSharp(List<List<PdfPoint>> PdfPointlocations, List<string[]> texts)
        {
            File.Copy(inputPath, outputPath, true);

            List<List<XPoint>> locations = ConvertPDFData(PdfPointlocations);

            // 1. Open the document in "Modify" mode
            using (PdfSharpCore.Pdf.PdfDocument document = PdfReader.Open(outputPath, PdfDocumentOpenMode.Modify))
            {
                // 2. Define the font (Standard Helvetica)
                // Use a small size (0.1) as per your requirement
                XFont font = new XFont("Helvetica", 0.1, XFontStyle.Regular);

                for (int i = 0; i < document.PageCount; i++)
                {
                    // Check if we have data for this page (0-indexed in PdfSharp)
                    if (i >= texts.Count || i >= locations.Count) continue;

                    PdfPage page = document.Pages[i];
                    string[] txtToPrint = texts[i];
                    List<XPoint> locToPrint = locations[i];

                    using (XGraphics gfx = XGraphics.FromPdfPage(page, XGraphicsPdfPageOptions.Append))
                    {
                        for (int j = 0; j < txtToPrint.Length; j++)
                        {
                            gfx.DrawString(txtToPrint[j], font, XBrushes.Transparent, locToPrint[j]);

                            //gfx.DrawString(txtToPrint[j], font, XBrushes.Transparent, locToPrint[j]);
                        //}


                        }
                    }
                }

                // 5. Save the document back to the same file
                document.Save(outputPath);
            }
        }

        private List<List<XPoint>> ConvertPDFData(List<List<PdfPoint>> pdfPointlocations)
        {
            List<List<XPoint>> returningData = new List<List<XPoint>>();
            using (PdfSharpCore.Pdf.PdfDocument document = PdfReader.Open(inputPath, PdfDocumentOpenMode.ReadOnly))
            {
                for(int i=0; i< pdfPointlocations.Count; i++)
                {
                    List<PdfPoint> outerData = pdfPointlocations[i];
                    PdfPage page = document.Pages[i+1];
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


        public void UpdatePdfInPlace(string outputPath, List<List<PdfPoint>> locations, List<string[]> texts)
        {
            File.Copy(inputPath, outputPath, true);
            byte[] updatedBytes;

            // 1. Read and Modify in memory
            using (UglyToad.PdfPig.PdfDocument document = UglyToad.PdfPig.PdfDocument.Open(outputPath))
            {
                PdfDocumentBuilder builder = new PdfDocumentBuilder();
                var font = builder.AddStandard14Font(Standard14Font.Helvetica);

                for (int i = 1; i <= document.NumberOfPages; i++)
                {
                    // Import original page
                    PdfPageBuilder pageBuilder = builder.AddPage(document, i);

                    // Add text if data exists for this page index
                    if (i <= texts.Count && i <= locations.Count)
                    {
                        string[] txtToPrint = texts[i - 1];
                        List<PdfPoint> locToPrint = locations[i - 1];

                        pageBuilder.SetTextAndFillColor(0, 0, 0);

                        for (int j = 0; j < txtToPrint.Length; j++)
                        {
                            // Applying the 0.1 font size for the "hidden" effect
                            pageBuilder.AddText(txtToPrint[j], 0.1, locToPrint[j], font);
                        }
                    }
                }

                // 2. Generate the new byte array while the document is still open
                updatedBytes = builder.Build();
            }

            // 3. Overwrite the original file
            // We do this AFTER the 'using' block to ensure the file handle is released
            try
            {
                File.WriteAllBytes(outputPath, updatedBytes);
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error: Could not save to {outputPath}. Is the file open in another program? {ex.Message}");
            }
        }

        //public void CreateBlankPageWithText(string outputPath, List<List<PdfPoint>> locations, List<string[]> texts)
        //{
        //    File.Copy(inputPath, outputPath, true);
        //    using (PdfDocument document = PdfDocument.Open(inputPath))
        //    {

        //        PdfDocumentBuilder builder = new PdfDocumentBuilder();
        //        var font = builder.AddStandard14Font(Standard14Font.Helvetica);

        //        for (int i = 1; i <= document.NumberOfPages; i++)
        //        {
        //            Page sourcePage = document.GetPage(i);

        //            //UglyToad.PdfPig.Content.Page page = document.GetPage(i);
        //            PdfPageBuilder pageBuilder = builder.AddPage(document, i);
        //            //pageBuilder.CopyContentFrom(sourcePage);

        //            // 2. Check if we have text to add for this specific page index
        //            // (Adjusted logic to prevent index out of range)
        //            if (i <= texts.Count && i <= locations.Count)
        //            {
        //                string[] txtToPrint = texts[i - 1];
        //                List<PdfPoint> LocToPrint = locations[i - 1];

        //                // 3. Set the graphics state for the new text
        //                pageBuilder.SetTextAndFillColor(0, 0, 0);
        //                pageBuilder.SetStrokeColor(0, 0, 0);

        //                for (int j = 0; j < txtToPrint.Length; j++)
        //                {
        //                    double fontSize = 12; // Back to your hidden size

        //                    PdfPoint point = LocToPrint[j];

        //                    // Clamp to visible area
        //                    double safeX = Math.Max(10, point.X);
        //                    double safeY = Math.Max(10, point.Y);
        //                    PdfPoint safePoint = new PdfPoint(safeX, safeY);

        //                    // 4. Add the text ON TOP of the original
        //                    pageBuilder.AddText(txtToPrint[j], fontSize, safePoint, font);
        //                }
        //            }

        //            // pageBuilder.SetTextAndFillColor(0, 0, 0);
        //            // pageBuilder.SetStrokeColor(0, 0, 0);

        //            // if (i > (locations.Count) || i > (texts.Count))
        //            // {
        //            //     continue;
        //            // }

        //            // string[] txtToPrint = texts[i-1];
        //            // List<PdfPoint> LocToPrint = locations[i-1];

        //            // for (int j = 0; j < txtToPrint.Length; j++)
        //            // {
        //            //     // Use a visible font size (12) for this test! 
        //            //     // If 12 works, then change back to 0.1.
        //            //     double fontSize = 12;

        //            //     // Ensure we aren't at 0,0 (which is sometimes invisible)
        //            //     PdfPoint point = LocToPrint[j];
        //            //     if (point.X < 5) point = new PdfPoint(10, point.Y);
        //            //     if (point.Y < 5) point = new PdfPoint(point.X, 10);

        //            //     pageBuilder.AddText(txtToPrint[j], fontSize, point, font);
        //            // }
        //            //// break;
        //        }

        //        // 5. Build and Write
        //        byte[] fileBytes = builder.Build();
        //        File.WriteAllBytes(outputPath, fileBytes);
        //    }
        //}

        //private void AddTextToPdf(List<List<PdfPoint>> locations, List<string[]> texts)
        //{

        //    using (PdfDocument document = PdfDocument.Open(inputPath))
        //    {
        //        PdfDocumentBuilder builder = new PdfDocumentBuilder();
        //        //var builder = new PdfDocumentBuilder();

        //        // Standard fonts are built-in to the PDF spec
        //        var font = builder.AddStandard14Font(Standard14Font.TimesRoman);

        //        for (int i = 1; i <= document.NumberOfPages; i++)
        //        {
        //            // 2. This creates a page in the new PDF based on the original
        //            // It copies the content stream (text, images, shapes)
        //            //PdfPageBuilder pageBuilder = builder.AddPage(document, i);
        //            UglyToad.PdfPig.Content.Page currentPAge = document.GetPage(i);
        //            PdfPageBuilder pageBuilder = builder.AddPage(currentPAge.Width, currentPAge.Height);

        //            //dang stupid base 1 that is PDF pig
        //            int locCnt = locations.Count + 1;
        //            int txtCnt = texts.Count + 1;
        //            if (i >= (locations.Count) || i >= (texts.Count))
        //            {
        //                continue;
        //            }

        //            double fontSize = 0.1;
        //            // 3. Add your specific text if this is the target page
        //            string[] txtToPrint = texts[i - 1];
        //            List<PdfPoint> LocToPrint = locations[i - 1];
        //            for (int j = 0; j < texts.Count; j++)
        //            {
        //                pageBuilder.SetStrokeColor(0, 0, 0);
        //                pageBuilder.SetTextAndFillColor(0, 0, 0);

        //                // Ensure font size is decimal (0.1m)
        //                pageBuilder.AddText(txtToPrint[j], fontSize, LocToPrint[j], font);
        //                //break;
        //            }
        //            PdfPoint tst = new PdfPoint(100, 100);
        //            pageBuilder.AddText("hey hey hey", 40, tst, font);
        //        }

        //        // 5. Build to a specific stream
        //        using (var fs = new FileStream(outputPath, FileMode.Create))
        //        {
        //            var b = builder.Build();
        //            fs.Write(b, 0, b.Length);
        //        }

        //        //// L
        //        ////
        //        //// oad a font (Standard 14 fonts are built-in)
        //        //PdfDocumentBuilder.AddedFont font = builder.AddStandard14Font(Standard14Font.Helvetica);

        //        //// 1. Loop through all pages of the original PDF
        //        //for (int i = 1; i <= document.NumberOfPages; i++)
        //        //{
        //        //    //dang stupid base 1 that is PDF pig
        //        //    if (i > (locations.Count+1) || i > (texts.Count+1))
        //        //        break;
        //        //    // Copy the original page into the builder
        //        //    Page page = document.GetPage(i);
        //        //    PdfPageBuilder pageBuilder = builder.AddPage(document, i);

        //        //    string[] txtToPrint = texts[i-1];
        //        //    List<PdfPoint> LocToPrint = locations[i-1];
        //        //    for (int j = 0; j < txtToPrint.Length; j++)
        //        //    {
        //        //        //pageBuilder.AddText(txtToPrint[j], 0.1, LocToPrint[j], font);
        //        //        pageBuilder.AddText(txtToPrint[j], 4, LocToPrint[j], font);
        //        //        break;
        //        //    }

        //        //    break;
        //        //}

        //        //// 3. Save the results to a new file
        //        //byte[] fileBytes = builder.Build();
        //        //File.WriteAllBytes(outputPath, fileBytes);
        //    }
        //}

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

                bool placed = false;

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

                            placed = true;
                            break;
                        }
                    }
                }
            }



            if(compileList.Count < BulletPoints.Length)
            {

                //TODO, what happens when you do NOT have enough space to print stuff out?
                //meaning you want to inject the entire book war and peace into the first page
                //but run out of room?
                //that is a problem that I will figure out later, but the scaffolding is set to do so.

                //if here, we took SOME, but not ALL of the bullet points out of the stirng array....
                int ab = 4;
                BulletPoints = BulletPoints[compileList.Count..];
                listToPrint = compileList.ToArray();
                Console.WriteLine("You need to debug this and figure out this edge case.!");
                // Keep everything from compileList.Count index to the end
                // check the 1 boundy make sure you are not cutting stuff of and such
                //BulletPoints = BulletPoints[compileList.Count..];
                //throw new Exception("Edge case ran out of room!");
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

                    // 3. Find placements for your strings
                    //var placements = FindPlacement(currentGrid, stringsToPrint);
                    //notFoundCnt += (width * height);
                    //// 1. Mark Text (Letters)
                    //foreach (var letter in page.Letters)
                    //{
                    //    MarkOccupied(currentGrid, letter.BoundingBox, width, height);
                    //}

                    //// 2. Mark Images
                    //foreach (var image in page.GetImages())
                    //{
                    //    MarkOccupied(currentGrid, image.BoundingBox, width, height);
                    //}

                    //// 3. Mark Vector Graphics (Paths/Boxes/Lines)
                    //foreach (var path in page.Paths)
                    //{
                    //    // Filter for visible elements
                    //    if (!path.IsFilled && !path.IsStroked) continue;

                    //    // Use the built-in bounding rectangle method
                    //    var bbox = path.GetBoundingRectangle();
                    //    if (bbox.HasValue)
                    //    {
                    //        MarkOccupied(currentGrid, bbox.Value, width, height);
                    //    }
                    //}
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

        private void MarkOccupied(bool[,] grid, PdfRectangle rect, int maxWidth, int maxHeight)
        {
            // Clamp bounds to prevent array out-of-bounds exceptions
            int xMin = Math.Max(0, (int)Math.Floor(rect.Left));
            int xMax = Math.Min(maxWidth, (int)Math.Ceiling(rect.Right));
            int yMin = Math.Max(0, (int)Math.Floor(rect.Bottom));
            int yMax = Math.Min(maxHeight, (int)Math.Ceiling(rect.Top));

            for (int x = xMin; x < xMax; x++)
            {
                for (int y = yMin; y < yMax; y++)
                {
                    grid[x, y] = true;
                    foundCnt++;
                }
            }
        }

        //private PdfRectangle? GetPathBoundingBox(PdfPath path)
        //{
        //    // PdfPath contains a list of PdfPoint called 'Points'
        //    if (path.Points == null || path.Points.Count == 0) return null;

        //    double minX = path.Points.Min(p => p.X);
        //    double minY = path.Points.Min(p => p.Y);
        //    double maxX = path.Points.Max(p => p.X);
        //    double maxY = path.Points.Max(p => p.Y);

        //    return new PdfRectangle(minX, minY, maxX, maxY);
        //}

        //private void MarkOccupied(bool[,] grid, PdfRectangle rect, int maxWidth, int maxHeight)
        //{
        //    // Convert PDF coordinates (Bottom-Left 0,0) to Grid coordinates
        //    int xMin = (int)Math.Floor(rect.Left);
        //    int xMax = (int)Math.Ceiling(rect.Right);
        //    int yMin = (int)Math.Floor(rect.Bottom);
        //    int yMax = (int)Math.Ceiling(rect.Top);

        //    for (int x = xMin; x < xMax; x++)
        //    {
        //        for (int y = yMin; y < yMax; y++)
        //        {
        //            // Ensure we stay within bounds of the array
        //            if (x >= 0 && x < maxWidth && y >= 0 && y < maxHeight)
        //            {
        //                grid[x, y] = true;
        //            }
        //        }
        //    }
        //}

    }
}
