using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;

using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("Specialized_PDF_Editor.Tests")]
namespace Specialized_PDF_Editor
{
    /// <summary>
    /// Class for analysis of basic pdf file and extract data
    /// </summary>
    internal sealed class Analysis : IDisposable
    {
        /// <summary>
        /// Stream for loaded file
        /// </summary>
        private MemoryStream stream = new MemoryStream();
        /// <summary>
        /// Information about every page
        /// </summary>
        internal PageInfo[] Pages { get; private set; }

        /// <summary>
        /// Page numbers of loaded documents
        /// </summary>
        internal int PageCount { get; private set; }

        /// <summary>
        /// Create instance for analysis
        /// </summary>
        /// <param name="stream">stream of loaded file</param>
        internal Analysis(MemoryStream stream)
        {
            this.stream = new MemoryStream(stream.ToArray(), 0, (int)stream.Length);
        }

        /// <summary>
        /// Extract data from PDF file
        /// </summary>
        /// <param name="stream">stream of loaded file</param>
        internal void ExtractData(MemoryStream stream)
        {
            stream.CopyTo(this.stream);
            ExtractData();
        }

        internal void ExtractData()
        {
            using (var reader = new PdfReader(stream))
            using (var pdf = new PdfDocument(reader))
            using (var doc = new Document(pdf))
            {
                reader.SetCloseStream(false);

                PageCount = pdf.GetNumberOfPages();

                #region Another variant of load all page (simple but longer)
#if false
                {
                    List<PdfPage> pages = new List<PdfPage>();
                    for (int i = 0; i < PageCount; i++)
                        pages.Add(pdf.GetPage(i + 1));
                }
#endif
                #endregion

                // get every page for analyse
                PdfPage[] pages = Enumerable
                            .Range(0, PageCount)
                            .Select(t => pdf.GetPage(t + 1))
                            .ToArray();

                // create array for save information about every page
                Pages = new PageInfo[pages.Length];

                for (int i = 0; i < pages.Length; i++)
                {
                    Pages[i].Id = i + 1;
                    Pages[i].Rotation = pages[i].GetRotation();
                    Pages[i].Size = pages[i].GetPageSize();

                    var dictionary = pdf.GetPage(i + 1).GetPdfObject().GetAsRectangle(PdfName.UserUnit);
                }


            }
        }

        public void Dispose()
        {
            stream?.Dispose();
        }
    }

    internal struct PageInfo
    {
        /// <summary>
        /// Number of page
        /// </summary>
        internal int Id { get; set; }

        /// <summary>
        /// Page orientation
        /// </summary>
        internal int Rotation { get; set; }

        /// <summary>
        /// Size of page
        /// </summary>
        internal Rectangle Size { get; set; }

        public override string ToString()
            => new StringBuilder($"Number of page: {Id};")
            .Append($"\nPage orientation: {Rotation} degree;")
            .Append($"\nPage size:")
            .Append($"\n\t - X: {Size.GetX()}")
            .Append($"\n\t - Y: {Size.GetY()}")
            .Append($"\n\t - Width: {Size.GetWidth()}")
            .Append($"\n\t - Height: {Size.GetHeight()}")
            .Append($"\n\t - Top: {Size.GetTop()}")
            .Append($"\n\t - Bottom: {Size.GetBottom()}")
            .Append($"\n\t - Left: {Size.GetLeft()}")
            .Append($"\n\t - Right: {Size.GetRight()}")
            .ToString();
    }
}
