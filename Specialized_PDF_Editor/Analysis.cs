using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Data;
using iText.Kernel.Pdf.Canvas.Parser.Filter;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Layout;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        private readonly MemoryStream stream = new MemoryStream();
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
        /// Data from tables
        /// </summary>
        internal KeyValuePairTable<int, DateTime, float, bool>[] TableData { get; set; }
        /// <summary>
        /// Data from Oy axis
        /// </summary>
        internal int[] DataOyAxis { get; set; }
        /// <summary>
        /// Coordinates of position data Oy axis
        /// </summary>
        internal TextChunk[] PositionOyAxis { get; set; }
        /// <summary>
        /// Data from Ox axis
        /// </summary>
        internal DateTime[] DataOxAxis { get; set; }
        /// <summary>
        /// Minimum of alloweble of range
        /// </summary>
        internal float LimitMin { get; set; }
        /// <summary>
        /// Maximum of alloweble of range
        /// </summary>
        internal float LimitMax { get; set; }

        /// <summary>
        /// Array of data from headers
        /// </summary>
        internal StringBuilder HeadInfo { get; private set; }
        /// <summary>
        /// Array of names of colums
        /// </summary>
        internal StringBuilder ColumnInfo { get; private set; }
        /// <summary>
        /// Array of data from tables
        /// </summary>
        private StringBuilder[] DataInfo { get; set; }
        /// <summary>
        /// Array of values of Oy axis
        /// </summary>
        private StringBuilder OyAxisInfo { get; set; }
        /// <summary>
        /// Array of values of Ox axis
        /// </summary>
        private StringBuilder OxAxisInfo { get; set; }

        /// <summary>
        /// Block acess to data for multitreading
        /// </summary>
        private readonly object block = new object();

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
            ExtractMetaData();
        }

        internal void ExtractMetaData()
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

                // data of every page (two diferent variants)
                HeadInfo = ParsingHeader(pages[0]);

                // find the limits
                {
                    Regex regex = new Regex(@"Задан диапазон температур между:(\s+[+-]?\d+)°C и(\s+[+-]?\d+)°C",
                        RegexOptions.IgnoreCase | RegexOptions.Compiled);
                    Match match = regex.Match(HeadInfo.ToString());
                    string findS = match.Value;

                    regex = new Regex(@"([+-]?\d+)", RegexOptions.Compiled);
                    MatchCollection matches = regex.Matches(findS);

                    List<float> results = new List<float>();
                    foreach (Match m in matches)
                    {
                        float.TryParse(m.Value, out float res);
                        results.Add(res);
                    }

                    // save data
                    LimitMin = results.Min();
                    LimitMax = results.Max();
                }

                //TODO: delete last row for last page, when we will be create new pdf-file

                // data of names every columns
                ColumnInfo = ParsingColumns(pages[0]);

                // data from tables
                DataInfo = new StringBuilder[PageCount - 1];

                // analyse tables all page without last using multithreading
                Parallel.For(0, DataInfo.Length,
                    i => DataInfo[i] = ParsingTables(pages[i]));

                // convert string data to real data
                TableData = ExtractTableData(DataInfo);

                // data of Oy axis
                OyAxisInfo = ParsingOyAxis(pages[PageCount - 1]);
                // convert string data to real data
                DataOyAxis = ExtractOyAxis(OyAxisInfo);

                // data of Ox axis
                OxAxisInfo = ParsingOxAxis(pages[PageCount - 1]);
                // convert string data to real data
                DataOxAxis = ExtractOxAxis(OxAxisInfo);

                pdf.Close();
            }
        }

        /// <summary>
        /// Check is file corected
        /// </summary>
        /// <returns></returns>
        internal bool FileValidation()
        {
            stream.Position = 0;

            // value that show is file corected
            bool result = true;

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

                // data of every page (two diferent variants)
                for (int i = 0; i < pages.Length; i++)
                {
                    HeadInfo = ParsingHeader(pages[i]);

                    Regex regex = new Regex(@"Серийный номер термотестера:",
                        RegexOptions.IgnoreCase | RegexOptions.Compiled);

                    Match match = regex.Match(HeadInfo.ToString());

                    result &= !string.IsNullOrEmpty(match.Value);
                }
            }

            return result;
        }

        /// <summary>
        /// Parsing header of pdf-file
        /// </summary>
        /// <param name="page">Data of page</param>
        /// <returns>header text from page</returns>
        internal StringBuilder ParsingHeader(PdfPage page)
        {
            // temp variable
            Rectangle readBox;
            TextRegionEventFilter readText;
            FilteredEventListener listener;
            LocationTextExtractionStrategy extractor;
            PdfCanvasProcessor parser;
            string[] lines;
            StringBuilder result = new StringBuilder();

            // area limit for read
            readBox = new Rectangle(Margin.Left,
                page.GetPageSize().GetHeight() - Margin.Top - 50,
                page.GetPageSize().GetWidth() - Margin.Right,
                Margin.Top + 50);
            readText = new TextRegionEventFilter(readBox);
            listener = new FilteredEventListener();

            // create a text extraction renderer
            extractor = listener
                .AttachEventListener(new LocationTextExtractionStrategy(),
                readText);

            lock (block)
            {
                (parser = new PdfCanvasProcessor(listener))
                    .ProcessPageContent(page);
                parser.Reset();
            }

            // read every line (row)
            lines = extractor
                .GetResultantText()
                .Split('\n');

            foreach (string line in lines)
                if (!string.IsNullOrEmpty(line.Trim()))
                    result.AppendLine(line);

            return result;
        }

        /// <summary>
        /// Parsing columns name
        /// </summary>
        /// <param name="page">Data of page</param>
        /// <returns>names of columns from page</returns>
        internal StringBuilder ParsingColumns(PdfPage page)
        {
            // temp variable
            Rectangle readBox;
            TextRegionEventFilter readText;
            FilteredEventListener listener;
            LocationTextExtractionStrategy extractor;
            PdfCanvasProcessor parser;
            string[] lines;
            StringBuilder result = new StringBuilder();

            // area limit for read
            readBox = new Rectangle(Margin.Left,
                page.GetPageSize().GetHeight() - Margin.Top - 70,
                (page.GetPageSize().GetWidth() - Margin.Right) / 4, 10);
            readText = new TextRegionEventFilter(readBox);
            listener = new FilteredEventListener();

            // create a text extraction renderer
            extractor = listener
                .AttachEventListener(new LocationTextExtractionStrategy(),
                readText);

            lock (block)
            {
                (parser = new PdfCanvasProcessor(listener))
                    .ProcessPageContent(page);
                parser.Reset();
            }

            // read every line (row)
            lines = extractor.GetResultantText()
                .Split('\n');

            foreach (string line in lines)
                result.AppendLine(line);

            return result;
        }

        /// <summary>
        /// Parsing data from tables
        /// </summary>
        /// <param name="page">Data of page</param>
        /// <returns>data of table every cell</returns>
        internal StringBuilder ParsingTables(PdfPage page)
        {
            // temp variable
            Rectangle readBox;
            TextRegionEventFilter readText;
            FilteredEventListener listener;
            LocationTextExtractionStrategy extractor;
            PdfCanvasProcessor parser;
            string[] lines;
            StringBuilder result = new StringBuilder();

            for (int j = 0; j < 4; j++)
            {
                // area limit for read
                readBox = new Rectangle(Margin.Left + j * (page.GetPageSize().GetWidth() - Margin.Right) / 4,
                    Margin.Bottom - 20,
                    (page.GetPageSize().GetWidth() - Margin.Right) / 4,
                    page.GetPageSize().GetHeight() - Margin.Bottom - 80);

                readText = new TextRegionEventFilter(readBox);
                listener = new FilteredEventListener();

                // create a text extraction renderer
                extractor = listener
                    .AttachEventListener(new LocationTextExtractionStrategy(),
                    readText);

                lock (block)
                {
                    (parser = new PdfCanvasProcessor(listener))
                        .ProcessPageContent(page);
                    parser.Reset();
                }

                // read every line (row)
                lines = extractor
                    .GetResultantText()
                    .Split('\n');

                foreach (string line in lines)
                    if (!string.IsNullOrEmpty(line.Trim()))
                        result.AppendLine(line);
            }
            return result;
        }

        /// <summary>
        /// Parsing data from Oy axis
        /// </summary>
        /// <param name="page">Data of page</param>
        /// <returns>data of Oy axis</returns>
        internal StringBuilder ParsingOyAxis(PdfPage page)
        {
            // temp variable
            Rectangle readBox;
            TextRegionEventFilter readText;
            FilteredEventListener listener;
            LocationTextExtractionStrategy extractor;
            PdfCanvasProcessor parser;
            string[] lines;
            StringBuilder result = new StringBuilder();

            // area limit for read
            readBox = new Rectangle(Margin.Left, Margin.Bottom + 60, 20,
                page.GetPageSize().GetHeight() - Margin.Bottom - 160);

            readText = new TextRegionEventFilter(readBox);
            listener = new FilteredEventListener();

            // create a text extraction renderer
            extractor = listener
                .AttachEventListener(new LocationTextExtractionStrategy(),
                readText);

            lock (block)
            {
                (parser = new PdfCanvasProcessor(listener))
                    .ProcessPageContent(page);
                parser.Reset();
            }

            // read every line (row)
            lines = extractor
                .GetResultantText()
                .Split('\n');

            foreach (string line in lines)
                if (!string.IsNullOrEmpty(line.Trim()))
                    result.AppendLine(line);

            TextExtractionStrategy strategy =
                listener.AttachEventListener(new TextExtractionStrategy(), readText);
            lock (block)
            {
                (parser = new PdfCanvasProcessor(listener))
                    .ProcessPageContent(page);
                parser.Reset();
            }

            PositionOyAxis = strategy.TextResult.ToArray();

            return result;
        }

        /// <summary>
        /// Parsing data from Ox axis
        /// </summary>
        /// <param name="page">Data of page</param>
        /// <returns>data of Oy axis</returns>
        internal StringBuilder ParsingOxAxis(PdfPage page)
        {
            // temp variable
            Rectangle readBox;
            TextRegionEventFilter readText;
            FilteredEventListener listener;
            LocationTextExtractionStrategy extractor;
            PdfCanvasProcessor parser;
            string[] lines;
            StringBuilder result = new StringBuilder();

            // area limit for read
            readBox = new Rectangle(Margin.Left + 20, Margin.Bottom + 40,
                page.GetPageSize().GetWidth() - Margin.Right, 40);

            readText = new TextRegionEventFilter(readBox);
            listener = new FilteredEventListener();

            // create a text extraction renderer
            extractor = listener
                .AttachEventListener(new LocationTextExtractionStrategy(),
                readText);

            lock (block)
            {
                (parser = new PdfCanvasProcessor(listener))
                    .ProcessPageContent(page);
                parser.Reset();
            }

            // read every line (row)
            lines = extractor
                .GetResultantText()
                .Split('\n');

            foreach (string line in lines)
                if (!string.IsNullOrEmpty(line.Trim()))
                    result.AppendLine(line);

            return result;
        }

        /// <summary>
        /// Extract data from text rows
        /// </summary>
        /// <param name="dataInfo">string data</param>
        /// <returns>array of data row</returns>
        internal KeyValuePairTable<int, DateTime, float, bool>[] ExtractTableData(StringBuilder[] dataInfo)
        {
            StringBuilder all = new StringBuilder();

            // union data
            foreach (StringBuilder page in dataInfo)
                all.Append(page);

            // convert to array of string
            string[] rows = all
                .ToString()
                .Trim('\n')
                .Replace("\r", "")
                .Split('\n');

            // create array of data
            var data = new KeyValuePairTable<int, DateTime, float, bool>[rows.Length];

            // convert strint into data using multithreading
            Parallel.For(0, rows.Length, i =>
            {
                var temp = rows[i].Trim().Split(' ');
                int.TryParse(temp[0], out int key);
                DateTime.TryParse(temp[1] + " " + temp[2], out DateTime datetime);
                float.TryParse(temp[3].Replace('.', ','), out float value);
                data[i] = new KeyValuePairTable<int, DateTime, float, bool>(key, datetime, value, temp.Length == 5);
            });

            return data;
        }

        /// <summary>
        /// Extract data from chart Oy axis
        /// </summary>
        /// <param name="dataInfo">string data</param>
        /// <returns>array of data Oy</returns>
        internal int[] ExtractOyAxis(StringBuilder dataInfo)
        {
            // convert to array of string
            string[] rows = dataInfo
                .ToString()
                .Trim('\n')
                .Replace("\r", "")
                .Split('\n');

            // create array of data
            int[] data = new int[rows.Length];

            // convert strint into data using multithreading
            Parallel.For(0, rows.Length, i => int.TryParse(rows[i], out data[i]));

            return data;
        }

        /// <summary>
        /// Extract data from chart Ox axis
        /// </summary>
        /// <param name="dataInfo">string data</param>
        /// <returns>array of data Ox</returns>
        internal DateTime[] ExtractOxAxis(StringBuilder dataInfo)
        {
            // convert to array of string
            string[] rows = dataInfo
                .ToString()
                .Trim('\n')
                .Replace("\r", "")
                .Split('\n');

            string[] times = rows[0].Trim().Split(' '),
                dates = rows[1].Trim().Split(' ');

            // create array of data
            DateTime[] data = new DateTime[times.Length];

            // convert strint into data using multithreading
            Parallel.For(0, times.Length, i =>
                DateTime.TryParse(times[i] + " " + dates[i], out data[i]));

            return data;
        }

        public void Dispose()
        {
            stream?.Dispose();
            HeadInfo = null;
            ColumnInfo = null;
            DataInfo = null;
        }
    }

    /// <summary>
    /// Class for analysis position of text
    /// </summary>
    internal class TextExtractionStrategy : LocationTextExtractionStrategy
    {
        /// <summary>
        /// List of posinion letters
        /// </summary>
        public List<TextChunk> TextResult { get; set; }
            = new List<TextChunk>();

        public override void EventOccurred(IEventData data, EventType type)
        {
            if (!type.Equals(EventType.RENDER_TEXT))
                return;

            TextRenderInfo renderInfo = (TextRenderInfo)data;

            string curFont = renderInfo.GetFont().GetFontProgram().ToString();
            float curFontSize = renderInfo.GetFontSize();

            IList<TextRenderInfo> text = renderInfo.GetCharacterRenderInfos();

            foreach (TextRenderInfo t in text)
            {
                string letter = t.GetText();
                Vector l_start = t.GetBaseline().GetStartPoint(),
                    l_end = t.GetAscentLine().GetEndPoint();

                Rectangle l_rect = new Rectangle(l_start.Get(0), l_start.Get(1),
                    l_end.Get(0) - l_start.Get(0), l_end.Get(1) - l_start.Get(1));

                if (letter != " " && !letter.Contains(' '))
                {
                    TextResult.Add(new TextChunk()
                    {
                        Text = letter,
                        Rect = l_rect,
                        FontFamily = curFont,
                        FontSize = curFontSize,
                    });
                }
            }

        }
    }

    /// <summary>
    /// Base info about position letter
    /// </summary>
    internal struct TextChunk
    {
        public string Text { get; set; }
        public Rectangle Rect { get; set; }
        public string FontFamily { get; set; }
        public float FontSize { get; set; }
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
            .Append($"\r\n    - Title: {Title}")
            .Append($"\r\n    - Author: {Author}")
            .Append($"\r\n    - Subject: {Subject}")
            .Append($"\r\n    - Keywords: {Keywords}")
            .Append($"\r\n    - Creator: {Creator}")
            .Append($"\r\n    - Producer: {Producer}")
            .Append($"\r\n    - Version: {Version}")
            .Append($"\r\n    - Creatioan date: " +
                $"{(string.IsNullOrEmpty(CreationDate) ? string.Empty : PdfDate.Decode(CreationDate).ToString())}")
            .Append($"\r\n    - Modification date: " +
                $"{(string.IsNullOrEmpty(ModificationDate) ? string.Empty : PdfDate.Decode(ModificationDate).ToString())}")
            .ToString();

    }

    /// <summary>
    /// Collection for table data
    /// </summary>
    /// <typeparam name="TKey">Number</typeparam>
    /// <typeparam name="TDateTime">Date and time</typeparam>
    /// <typeparam name="TValue">Temperature in Celsius</typeparam>
    /// <typeparam name="TOOR">Out of range</typeparam>
    internal struct KeyValuePairTable<TKey, TDateTime, TValue, TOOR>
    {
        /// <summary>
        /// Number
        /// </summary>
        internal TKey Key { get; }
        /// <summary>
        /// Date and time
        /// </summary>
        internal TDateTime DateTime { get; }
        /// <summary>
        /// Temperature in Celsius
        /// </summary>
        internal TValue Value { get; }
        /// <summary>
        /// Out of range
        /// </summary>
        internal TOOR OOR { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="key">Number</param>
        /// <param name="datetime">Date and time</param>
        /// <param name="value">Temperature in Celsius</param>
        /// <param name="oor">Out of range</param>
        internal KeyValuePairTable(TKey key, TDateTime datetime, TValue value, TOOR oor)
        {
            Key = key;
            DateTime = datetime;
            Value = value;
            OOR = oor;
        }

        public override string ToString()
            => new StringBuilder($"Number: {Key};")
            .Append($"\nDate and time: {DateTime};")
            .Append($"\nTemperature: {Value};")
            .Append($"\nOut of range: {OOR};")
            .ToString();

    }

}
