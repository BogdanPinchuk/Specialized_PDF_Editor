using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Action;
using iText.Kernel.Pdf.Annot;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Filter;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Layout;

using System;
using System.Collections.Generic;
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
        /// Margin of document
        /// </summary>
        internal MarginInfo Margin { get; set; }
        /// <summary>
        /// Metadata of document
        /// </summary>
        internal MetaData Metadata { get; set; }

        /// <summary>
        /// Array of data from headers
        /// </summary>
        internal StringBuilder[] HeadInfo { get; private set; }

        /// <summary>
        /// Create instance for analysis
        /// </summary>
        /// <param name="stream">stream of loaded file</param>
        internal Analysis(MemoryStream stream)
        {
            this.stream = new MemoryStream(stream.ToArray(), 0, (int)stream.Length);
        }

        /// <summary>
        /// Create instance for analysis
        /// </summary>
        public Analysis() { }

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
            stream.Position = 0;

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

                    Pages[i].Size = new SizeInfo
                    {
                        X = pages[i].GetPageSize().GetX(),
                        Y = pages[i].GetPageSize().GetY(),
                        Height = pages[i].GetPageSize().GetHeight(),
                        Width = pages[i].GetPageSize().GetWidth(),
                        HeightUU = iTextSharp.text.Utilities
                            .PointsToMillimeters(pages[i].GetPageSize().GetHeight()),
                        WidthUU = iTextSharp.text.Utilities
                            .PointsToMillimeters(pages[i].GetPageSize().GetWidth()),
                        Top = pages[i].GetPageSize().GetTop(),
                        Bottom = pages[i].GetPageSize().GetBottom(),
                        Left = pages[i].GetPageSize().GetLeft(),
                        Right = pages[i].GetPageSize().GetRight(),
                    };
                }

                Margin = new MarginInfo
                {
                    Top = doc.GetTopMargin(),
                    Bottom = doc.GetBottomMargin(),
                    Left = doc.GetLeftMargin(),
                    Right = doc.GetRightMargin()
                };

                var data = pdf.GetDocumentInfo();
                Metadata = new MetaData
                {
                    Title = data.GetTitle(),
                    Author = data.GetAuthor(),
                    Subject = data.GetSubject(),
                    Keywords = data.GetKeywords(),
                    Creator = data.GetCreator(),
                    Producer = data.GetProducer(),
                    Version = pdf.GetPdfVersion(),
                    CreationDate = data.GetMoreInfo(PdfName.CreationDate.GetValue()),
                    ModificationDate = data.GetMoreInfo(PdfName.ModDate.GetValue()),
                };
            }
        }

        /// <summary>
        /// Parsing data of pdf-file from tables
        /// </summary>
        internal void ParsingFile()
        {
            stream.Position = 0;

            using (var reader = new PdfReader(stream))
            using (var pdf = new PdfDocument(reader))
            {
                reader.SetCloseStream(false);

                PageCount = pdf.GetNumberOfPages();

                // get every page for analyse
                PdfPage[] pages = Enumerable
                            .Range(0, PageCount)
                            .Select(t => pdf.GetPage(t + 1))
                            .ToArray();

                // data of every page
                HeadInfo = new StringBuilder[PageCount];

                // temp variable
                Rectangle readBox;
                TextRegionEventFilter readText;
                FilteredEventListener listener;
                LocationTextExtractionStrategy extractor;

                for (int i = 0; i < 1 + 0* PageCount;  i++)
                {
                    HeadInfo[i] = new StringBuilder();

                    // read head-information

                    // area limit for read
                    readBox = new Rectangle(Margin.Left,
                        Pages[i].Size.Height - Margin.Top - 60,
                        Pages[i].Size.Width - Margin.Right, 60);
                    //readBox = new Rectangle(36, 400, 100, 36);
                    readText = new TextRegionEventFilter(readBox);
                    listener = new FilteredEventListener();

                    // create a text extraction renderer
                    //extractor = new LocationTextExtractionStrategy();
                    extractor = listener
                        .AttachEventListener(new LocationTextExtractionStrategy(),
                        readText);

                    new PdfCanvasProcessor(listener)
                        .ProcessPageContent(pages[i]);

                    HeadInfo[i].Append(extractor.GetResultantText())
                        .Append("\n");
                }

                pdf.Close();
            }
        }

        public void Dispose()
        {
            stream?.Dispose();
        }
    }

    /// <summary>
    /// General page info
    /// </summary>
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
        internal SizeInfo Size { get; set; }

        public override string ToString()
            => new StringBuilder($"Number of page: {Id};")
            .Append($"\nPage orientation: {Rotation} degree;")
            .Append($"\n{Size}")
            .ToString();

    }

    /// <summary>
    /// General size info
    /// </summary>
    internal struct SizeInfo
    {
        /// <summary>
        /// X pixels
        /// </summary>
        internal float X { get; set; }
        /// <summary>
        /// Y pixels
        /// </summary>
        internal float Y { get; set; }
        /// <summary>
        /// Height in user units
        /// </summary>
        internal float Height { get; set; }
        /// <summary>
        /// Width in pixels
        /// </summary>
        internal float Width { get; set; }
        /// <summary>
        /// Height in pixels
        /// </summary>
        internal float HeightUU { get; set; }
        /// <summary>
        /// Width in user units
        /// </summary>
        internal float WidthUU { get; set; }
        /// <summary>
        /// Top in pixels
        /// </summary>
        internal float Top { get; set; }
        /// <summary>
        /// Bottom in pixels
        /// </summary>
        internal float Bottom { get; set; }
        /// <summary>
        /// Left in pixels
        /// </summary>
        internal float Left { get; set; }
        /// <summary>
        /// Right in pixels
        /// </summary>
        internal float Right { get; set; }

        public override string ToString()
            => new StringBuilder($"Page size:")
            .Append($"\n\t - X: {X}")
            .Append($"\n\t - Y: {Y}")
            .Append($"\n\t - Width: {Width}")
            .Append($"\n\t - Height: {Height}")
            .Append($"\n\t - Width: {WidthUU} mm")
            .Append($"\n\t - Height: {HeightUU} mm")
            .Append($"\n\t - Top: {Top}")
            .Append($"\n\t - Bottom: {Bottom}")
            .Append($"\n\t - Left: {Left}")
            .Append($"\n\t - Right: {Right}")
            .ToString();

    }

    /// <summary>
    /// General margin info
    /// </summary>
    internal struct MarginInfo
    {
        internal float Top { get; set; }
        /// <summary>
        /// Bottom in pixels
        /// </summary>
        internal float Bottom { get; set; }
        /// <summary>
        /// Left in pixels
        /// </summary>
        internal float Left { get; set; }
        /// <summary>
        /// Right in pixels
        /// </summary>
        internal float Right { get; set; }

        public override string ToString()
            => new StringBuilder($"Margin value:")
            .Append($"\n\t - Top: {Top}")
            .Append($"\n\t - Bottom: {Bottom}")
            .Append($"\n\t - Left: {Left}")
            .Append($"\n\t - Right: {Right}")
            .ToString();
    }

    /// <summary>
    /// General info about pad-file
    /// </summary>
    internal struct MetaData
    {
        /// <summary>
        /// Title
        /// </summary>
        internal string Title { get; set; }
        /// <summary>
        /// Author
        /// </summary>
        internal string Author { get; set; }
        /// <summary>
        /// Theme
        /// </summary>
        internal string Subject { get; set; }
        /// <summary>
        /// Key words
        /// </summary>
        internal string Keywords { get; set; }
        /// <summary>
        /// Time created of fiel
        /// </summary>
        internal string Creator { get; set; }
        /// <summary>
        /// Creator
        /// </summary>
        internal string Producer { get; set; }
        /// <summary>
        /// Version of pdf
        /// </summary>
        internal PdfVersion Version { get; set; }
        /// <summary>
        /// Creation date of file
        /// </summary>
        internal string CreationDate { get; set; }
        /// <summary>
        /// Modification date of file
        /// </summary>
        internal string ModificationDate { get; set; }
        ///// <summary>
        ///// Fonts in file
        ///// </summary>
        //internal List<Font> Fonts { get; set; }

        public override string ToString()
            => new StringBuilder($"PDF description:")
            .Append($"\n\t - Title: {Title}")
            .Append($"\n\t - Author: {Author}")
            .Append($"\n\t - Subject: {Subject}")
            .Append($"\n\t - Keywords: {Keywords}")
            .Append($"\n\t - Creator: {Creator}")
            .Append($"\n\t - Producer: {Producer}")
            .Append($"\n\t - Version: {Version}")
            .Append($"\n\t - Creatioan date: " +
                $"{(string.IsNullOrEmpty(CreationDate) ? string.Empty : PdfDate.Decode(CreationDate).ToString())}")
            .Append($"\n\t - Modification date: " +
                $"{(string.IsNullOrEmpty(ModificationDate) ? string.Empty : PdfDate.Decode(ModificationDate).ToString())}")
            .ToString();

    }

}
