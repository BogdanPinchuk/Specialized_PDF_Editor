using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Specialized_PDF_Editor
{
    /// <summary>
    /// Class for control GUI
    /// </summary>
    internal static class Visual
    {
        /// <summary>
        /// Viewer for loaded pdf file
        /// </summary>
        internal static PdfiumViewer.PdfViewer PdfViewerL { get; set; }
        /// <summary>
        /// Viewer for created pdf file
        /// </summary>
        internal static PdfiumViewer.PdfViewer PdfViewerC { get; set; }
        /// <summary>
        /// Status label for names of documents
        /// </summary>
        internal static ToolStripStatusLabel NameOfDoc { get; set; }
        /// <summary>
        /// Status label for important messeges
        /// </summary>
        internal static ToolStripStatusLabel Status { get; set; }
        /// <summary>
        /// Temp testing textboxing
        /// </summary>
        internal static TextBox TestInfo { get; set; }

        /// <summary>
        /// File in RAM
        /// </summary>
        internal static MemoryMappedFile mmFile;
        /// <summary>
        /// Stream for file in RAM
        /// </summary>
        internal static MemoryMappedViewStream streamMMF;

        /// <summary>
        /// Stream for loaded file
        /// </summary>
        internal static MemoryStream StreamL { get; set; }
        /// <summary>
        /// Stream for created file
        /// </summary>
        internal static MemoryStream StreamC { get; set; }
        /// <summary>
        /// Path for base-file
        /// </summary>
        internal static string BPath { get; set; }
        /// <summary>
        /// Path for temp-file
        /// </summary>
        internal static string TPath { get; set; }


        /// <summary>
        /// Load pdf-file to RAM
        /// </summary>
        /// <param name="Path">Path for file</param>
        /// <param name="stream">stream for document</param>
        /// <param name="pdfViewer">Component with view the document</param>
        internal static void LoadPdfToMemory(string Path, PdfiumViewer.PdfViewer pdfViewer)
        {
            try
            {
                if (new FileInfo(Path).Extension != ".pdf")
                    throw new InvalidOperationException("File not in PDF format or corrupted");

                byte[] bytes = File.ReadAllBytes(Path);
                MemoryStream stream = new MemoryStream(bytes);
                ShowDocument(stream, pdfViewer);
                pdfViewer.ShowToolbar = true;
                pdfViewer.ShowBookmarks = true;

                if (pdfViewer.Equals(PdfViewerL))
                    StreamL = stream;
                else
                    StreamC = stream;

                if (pdfViewer.Equals(PdfViewerL))
                    NameOfDoc.Text = new FileInfo(Path).Name;
            }
            catch (Exception ex)
            {
                Status.Text = ex.Message;
            }
        }

        /// <summary>
        /// Load pdf-file to RAM
        /// </summary>
        /// <param name="Path">Path for file</param>
        /// <param name="stream">stream for document</param>
        /// <param name="pdfViewer">Component with view the document</param>
        internal static void LoadPdfToMemory(Stream stream, PdfiumViewer.PdfViewer pdfViewer)
        {
            try
            {
                ShowDocument(stream, pdfViewer);
                pdfViewer.ShowToolbar = true;
                pdfViewer.ShowBookmarks = true;
            }
            catch (Exception ex)
            {
                Status.Text = ex.Message;
            }
        }
        
        /// <summary>
        /// Show document
        /// </summary>
        /// <param name="stream">Stream of data</param>
        /// <param name="pdfViewer">Component with view the document</param>
        private static void ShowDocument(Stream stream, PdfiumViewer.PdfViewer pdfViewer)
        {
            try
            {
                PdfiumViewer.PdfDocument pdf = PdfiumViewer.PdfDocument.Load(stream);
                pdfViewer.Document = pdf;
                pdfViewer.Show();
                Status.Text = "File showed.";

                if (pdfViewer.Equals(PdfViewerL))
                {
                    if (PdfViewerC.Document == null ||
                        PdfViewerC.Document.PageCount < 1)
                        return;

                    do
                    {
                        PdfViewerC.Document.DeletePage(0);
                    } while (PdfViewerC.Document.PageCount > 0);

                    PdfViewerC.Refresh();
                }
            }
            catch (Exception ex)
            {
                Status.Text = ex.Message;
            }
        }

        /// <summary>
        /// Create local temp file for save result
        /// </summary>
        internal static void CreateLocalFile()
        {
            if (!string.IsNullOrEmpty(TPath))
                File.Delete(TPath);

            TPath = Directory.GetCurrentDirectory() + "\\temp.pdf";

            if (File.Exists(TPath))
            {
                Status.Text = "Creating temp-file";

                var time = DateTime.Now.AddSeconds(-1);

                do
                {
                    TPath = Directory.GetCurrentDirectory() + "\\temp " +
                        time.AddSeconds(1).ToString().Replace(":", ".") + ".pdf";
                } while (File.Exists(TPath));

                Status.Text = string.Empty;
            }

            try
            {
                using (var stream = new FileStream(TPath, FileMode.Create, FileAccess.ReadWrite))
                using (var writer = new PdfWriter(stream))
                using (var pdf = new PdfDocument(writer))
                using (var doc = new Document(pdf))
                {
                    doc.Add(new Paragraph("I'm Bogdan, I`m created this test-file."))
                        .Add(new Paragraph("Life is beautiful"));

                    writer.Flush();
                    stream.Flush();
                }
            }
            catch (Exception ex)
            {
                Status.Text = ex.Message;
            }

            LoadPdfToMemory(TPath, PdfViewerC);

            Status.Text = "Analysis is completed";
        }

        /// <summary>
        /// Create temp file in RAM for save result
        /// </summary>
        internal static void CreateRAMFile()
        {
            string name = "temp " + DateTime.Now.ToString().Replace(":", ".") + ".pdf";

            try
            {
                mmFile = MemoryMappedFile.CreateNew(name, 4096, MemoryMappedFileAccess.ReadWrite);
                streamMMF = mmFile.CreateViewStream(0, 0, MemoryMappedFileAccess.ReadWrite);

                using (var writer = new PdfWriter(streamMMF))
                using (var pdf = new PdfDocument(writer))
                using (var doc = new Document(pdf))
                {
                    writer.SetCloseStream(false);

                    doc.Add(new Paragraph("I'm Bogdan, I`m created this test-file."))
                        .Add(new Paragraph("Life is beautiful"));

                    doc.Flush();
                    doc.Close();    // write data in RAM after close this
                }

            }
            catch (Exception ex)
            {
                Status.Text = ex.Message;
            }

            LoadPdfToMemory(streamMMF, PdfViewerC);

            Status.Text = "Analysis is completed";
        }

        /// <summary>
        /// Create data of future file in RAM for save result
        /// </summary>
        internal static void CreateRAMData()
        {
            try
            {
                StreamC = new MemoryStream();

                using (var writer = new PdfWriter(StreamC))
                using (var pdf = new PdfDocument(writer))
                using (var doc = new Document(pdf))
                {
                    writer.SetCloseStream(false);

                    doc.Add(new Paragraph("I'm Bogdan, I`m created this test-file."))
                        .Add(new Paragraph("Life is beautiful"));

                    doc.Flush();
                    doc.Close();    // write data in RAM after close this
                }

            }
            catch (Exception ex)
            {
                Status.Text = ex.Message;
            }

            LoadPdfToMemory(StreamC, PdfViewerC);

            Status.Text = "Analysis is completed";
        }


        /// <summary>
        /// Unload/clean data from memory but not close
        /// </summary>
        internal static void Unload()
        {
            StreamL?.Close();
            StreamC?.Close();
            PdfViewerL?.Dispose();
            PdfViewerC?.Dispose();

            if (!string.IsNullOrEmpty(TPath))
                File.Delete(TPath);
        }

        internal static void Dispose()
        {
            Unload();

            mmFile?.Dispose();
            streamMMF?.Dispose();
        }

    }
}
