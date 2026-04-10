using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;
using UglyToad.PdfPig.Core;
using UglyToad.PdfPig.Graphics;
using UglyToad.PdfPig.Graphics.Operations.General;
using static UglyToad.PdfPig.Core.PdfSubpath;
using PdfRectangle = UglyToad.PdfPig.Core.PdfRectangle;

namespace ATS_Injector
{
    internal class PDFInjectorPdfPig
    {
        private string inputPath;
        private string outputPath;
        private string[] BulletPoints;
        private static string errorMsg = string.Empty;

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
            //List < PdfPoint > 

            long curCng = foundCnt;
            int ab = 54;
        }

        private List<bool[,]> generateGridList()
        {
            List<bool[,]> gridList = new List<bool[,]>();
            int totalPages = -1;
            using (PdfDocument document = PdfDocument.Open(inputPath))
            {
                totalPages = document.GetPages().Count();
                foreach (Page page in document.GetPages())
                {
                    int width = (int)Math.Ceiling(page.Width);
                    int height = (int)Math.Ceiling(page.Height);
                    bool[,] currentGrid = new bool[width, height];
                    notFoundCnt += (width * height);
                    // 1. Mark Text (Letters)
                    foreach (var letter in page.Letters)
                    {
                        MarkOccupied(currentGrid, letter.BoundingBox, width, height);
                    }

                    // 2. Mark Images
                    foreach (var image in page.GetImages())
                    {
                        MarkOccupied(currentGrid, image.BoundingBox, width, height);
                    }

                    // 3. Mark Vector Graphics (Paths/Boxes/Lines)
                    foreach (var path in page.Paths)
                    {
                        // Filter for visible elements
                        if (!path.IsFilled && !path.IsStroked) continue;

                        // Use the built-in bounding rectangle method
                        var bbox = path.GetBoundingRectangle();
                        if (bbox.HasValue)
                        {
                            MarkOccupied(currentGrid, bbox.Value, width, height);
                        }
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
