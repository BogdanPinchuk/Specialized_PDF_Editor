using iText.Kernel.Pdf;
using iText.Layout;

using System;
using System.IO;
using System.Runtime.CompilerServices;

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
            } 
        }

        public void Dispose()
        {
            stream?.Dispose();
        }
    }
}
