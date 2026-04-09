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
    internal class PDFInjectorPdfSharp
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
        public PDFInjectorPdfSharp(string inputPath, string outputPath, string[] BulletPoints)
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
            // 1. Open the existing document in Modify mode
            using (PdfDocument document = PdfReader.Open(inputPath, PdfDocumentOpenMode.Modify))
            {
                // 2. We only need to attach this to the first page (or any single page)
                PdfPage page = document.Pages[0];
                double width = page.Width.Value;
                double height = page.Height.Value;
                int offestVal = 100;

                using (XGraphics gfx = XGraphics.FromPdfPage(page, XGraphicsPdfPageOptions.Append))
                {
                    XFont font = new XFont("Arial", 8);
                    XBrush brush = XBrushes.Transparent;

                    double offScreenX = width + offestVal;
                    double offScreenY = height + offestVal;

                    foreach (string data in BulletPoints)
                    {
                        if (string.IsNullOrWhiteSpace(data)) continue;

                        gfx.DrawString(data, font, brush, new XPoint(offScreenX, offScreenY));
                        offScreenY += 10;
                    }
                }
                document.Save(outputPath);
            }
        }



        #region TINYFONT

        // A point is 1/72 of an inch. A gridSize of 5 is quite precise.
        private const int GridSize = 5;

        private void InjectTINYFONT()
        {
            //Steps
            //loop over every page and create a grid of available empty spaces per page
            //loop over bullet list, and inject the text using greddy algorith

            List<bool[,]> gridList = generateGridList();
            int ab = 4;
        }

        private List<bool[,]> generateGridList()
        {
            int totalPages = -1;
            List<bool[,]> retruningData = new List<bool[,]>();
            int targetFontSize = 1;
            
            try
            {
                // Open the document in InformationOnly mode for speed
                using (PdfDocument document = PdfReader.Open(inputPath, PdfDocumentOpenMode.InformationOnly))
                {
                    totalPages = document.PageCount;
                }
            }
            catch (Exception ex)
            {
                totalPages = -1;
            }

            if (totalPages > 0)
            {
                using (var document = PdfReader.Open(inputPath, PdfDocumentOpenMode.ReadOnly))
                {

                    for(int i=0; i< totalPages; i++)
                    {
                        PdfPage currentPage = document.Pages[i];

                        int rows = (int)(currentPage.Height.Point / GridSize) + 1;
                        int cols = (int)(currentPage.Width.Point / GridSize) + 1;

                        bool[,] currentGrid = new bool[cols, rows]; // true = occupied, false = empty

                        CObject content = ContentReader.ReadContent(currentPage);
                        double currentFontSize = 0;

                        foreach (CObject obj in ContentReader.ReadContent(currentPage))
                        {
                            if (obj is CSequence sequence)
                            {
                                // Recursive check if the sequence contains more operators
                                ProcessSequence(sequence, targetFontSize, currentGrid);
                            }
                            else if (obj is COperator opPos && (opPos.Name == "Tm" || opPos.Name == "Td"))
                            {
                                // Simplified: Extract X and Y from the operator operands
                                // Note: Real PDF parsing requires tracking the CTM (Matrix)
                                double x = GetOperandValue(opPos.Operands[opPos.Operands.Count - 2]);
                                double y = GetOperandValue(opPos.Operands[opPos.Operands.Count - 1]);

                                // If this matches our font size, block the grid
                                if (Math.Abs(currentFontSize - targetFontSize) < 0.5)
                                {
                                    MarkGridOccupied(currentGrid, x, y, currentFontSize);
                                }
                            }
                        }
                        retruningData.Add(currentGrid);
                    }//End of for int i loop
                }//End of using var documents
            }//end of if pages > 0

            return retruningData;
        }

        public void ProcessSequence(CSequence sequence, double targetFontSize, bool[,] grid)
        {
            double currentFontSize = 0;

            for (int i = 0; i < sequence.Count; i++)
            {
                var element = sequence[i];

                if (element is CSequence nested)
                {
                    ProcessSequence(nested, targetFontSize, grid);
                }
                else if (element is COperator op)
                {
                    // Tf: Set Font and Size
                    if (op.Name == "Tf" && op.Operands.Count >= 2)
                    {
                        if (op.Operands[1] is CReal fSize) currentFontSize = fSize.Value;
                        else if (op.Operands[1] is CInteger iSize) currentFontSize = iSize.Value;
                    }

                    // Tm or Td: Text Positioning
                    if ((op.Name == "Tm" || op.Name == "Td" || op.Name == "TD") && op.Operands.Count >= 2)
                    {
                        if (Math.Abs(currentFontSize - targetFontSize) < 0.1)
                        {
                            double x = GetOperandValue(op.Operands[op.Operands.Count - 2]);
                            double y = GetOperandValue(op.Operands[op.Operands.Count - 1]);
                            MarkGridOccupied(grid, x, y, currentFontSize);
                        }
                    }
                }
            }
        }

        private void MarkGridOccupied(bool[,] grid, double x, double y, double fontSize)
        {
            // Estimate a bounding box: Width is roughly (charCount * fontSize * 0.6)
            // For this logic, we mark the area around the coordinate
            int startX = (int)(x / GridSize);
            int startY = (int)(y / GridSize);

            // We block a small area proportional to font size
            int sizeInCells = (int)(fontSize / GridSize);

            for (int i = startX; i < startX + (sizeInCells * 5); i++) // Assuming ~5 chars width
            {
                for (int j = startY; j < startY + sizeInCells; j++)
                {
                    if (i >= 0 && i < grid.GetLength(0) && j >= 0 && j < grid.GetLength(1))
                        grid[i, j] = true;
                }
            }
        }

        private double GetOperandValue(CObject obj)
        {
            if (obj is CReal r) return r.Value;
            if (obj is CInteger i) return i.Value;
            return 0;
        }

        public (int x, int y)? FindFirstEmptySpot(bool[,] grid, int widthPoints, int heightPoints)
        {
            int wCells = widthPoints / GridSize;
            int hCells = heightPoints / GridSize;

            for (int y = 0; y < grid.GetLength(1) - hCells; y++)
            {
                for (int x = 0; x < grid.GetLength(0) - wCells; x++)
                {
                    if (IsRegionEmpty(grid, x, y, wCells, hCells))
                        return (x * GridSize, y * GridSize);
                }
            }
            return null;
        }

        private bool IsRegionEmpty(bool[,] grid, int x, int y, int w, int h)
        {
            for (int i = x; i < x + w; i++)
                for (int j = y; j < y + h; j++)
                    if (grid[i, j]) return false;
            return true;
        }


        #endregion TINYFONT
        public enum InjectionMethod
        {
            OFFSCREEN,
            TINYFONT,
        }

        private string[] AISplit(string RawData)
        {
            return RawData
            .Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries)
            .Where(line => line.TrimStart().StartsWith("*"))
            .Select(line => line.TrimStart().TrimStart('*').Trim())
            .Where(line => !string.IsNullOrEmpty(line))
            .ToArray();
        }

    }
}
