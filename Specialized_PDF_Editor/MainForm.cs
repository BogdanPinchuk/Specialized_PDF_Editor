using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Windows.Forms;

namespace Specialized_PDF_Editor
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// File in RAM
        /// </summary>
        private MemoryMappedFile mmFile;
        /// <summary>
        /// Stream for file in RAM
        /// </summary>
        private MemoryMappedViewStream streamMMF;

        /// <summary>
        /// Stream for loaded file
        /// </summary>
        private MemoryStream streamL;
        /// <summary>
        /// Stream for created file
        /// </summary>
        internal MemoryStream streamC;
        /// <summary>
        /// Path for base-file
        /// </summary>
        private string path;
        /// <summary>
        /// Path for temp-file
        /// </summary>
        private string tpath;

        internal MainForm(string[] pathes)
        {
            InitializeComponent();

            if (pathes.Length > 0 && !string.IsNullOrEmpty(pathes[0]))
            {
                path = pathes[0];
                LoadPdfToMemory(path, ref streamL, pdfViewerL);
            }
        }

        /// <summary>
        /// Load pdf-file to RAM
        /// </summary>
        /// <param name="path">path for file</param>
        /// <param name="stream">stream for document</param>
        /// <param name="pdfViewer">Component with view the document</param>
        internal void LoadPdfToMemory(string path, ref MemoryStream stream, PdfiumViewer.PdfViewer pdfViewer)
        {
            try
            {
                if (new FileInfo(path).Extension != ".pdf")
                    throw new InvalidOperationException("File not in PDF format or corrupted");

                byte[] bytes = File.ReadAllBytes(path);
                stream = new MemoryStream(bytes);
                ShowDocument(stream, pdfViewer);
                pdfViewer.ShowToolbar = true;
                pdfViewer.ShowBookmarks = true;

                if (pdfViewer.Equals(pdfViewerL))
                    nameOfDoc.Text = new FileInfo(path).Name;
            }
            catch (Exception ex)
            {
                status.Text = ex.Message;
            }
        }

        /// <summary>
        /// Load pdf-file to RAM
        /// </summary>
        /// <param name="path">path for file</param>
        /// <param name="stream">stream for document</param>
        /// <param name="pdfViewer">Component with view the document</param>
        internal void LoadPdfToMemory(string path, MemoryMappedViewStream stream, PdfiumViewer.PdfViewer pdfViewer)
        {
            try
            {
                ShowDocument(stream, pdfViewer);
                pdfViewer.ShowToolbar = true;
                pdfViewer.ShowBookmarks = true;
            }
            catch (Exception ex)
            {
                status.Text = ex.Message;
            }
        }

        /// <summary>
        /// Show document
        /// </summary>
        /// <param name="stream">Stream of data</param>
        /// <param name="pdfViewer">Component with view the document</param>
        private void ShowDocument(Stream stream, PdfiumViewer.PdfViewer pdfViewer)
        {
            try
            {
                PdfiumViewer.PdfDocument pdf = PdfiumViewer.PdfDocument.Load(stream);
                pdfViewer.Document = pdf;
                pdfViewer.Show();
                status.Text = "File showed.";

                if (pdfViewer.Equals(pdfViewerL))
                {
                    if (pdfViewerC.Document == null ||
                        pdfViewerC.Document.PageCount < 1)
                        return;

                    do
                    {
                        pdfViewerC.Document.DeletePage(0);
                    } while (pdfViewerC.Document.PageCount > 0);

                    pdfViewerC.Refresh();
                }
            }
            catch (Exception ex)
            {
                status.Text = ex.Message;
            }
        }

        /// <summary>
        /// Unload data from memory
        /// </summary>
        private void UnLoad()
        {
            streamL?.Close();
            streamC?.Close();
            pdfViewerL?.Dispose();
            pdfViewerC?.Dispose();

            if (!string.IsNullOrEmpty(tpath))
                File.Delete(tpath);

            Dispose();

            Environment.Exit(0);
        }

        private void OpenMenu_Click(object sender, EventArgs e)
        {
            if (openFD.ShowDialog() == DialogResult.OK)
                LoadPdfToMemory(openFD.FileName, ref streamL, pdfViewerL);
        }

        private void ExitMenu_Click(object sender, EventArgs e)
            => UnLoad();

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
            => UnLoad();

        private void AnalyseMenu_Click(object sender, EventArgs e)
        {
            //CreateLocalFile();
            CreateRAMFile();
        }

        /// <summary>
        /// Create local temp file for save result
        /// </summary>
        private void CreateLocalFile()
        {
            if (!string.IsNullOrEmpty(tpath))
                File.Delete(tpath);

            tpath = Directory.GetCurrentDirectory() + "\\temp.pdf";

            if (File.Exists(tpath))
            {
                status.Text = "Creating temp-file";

                var time = DateTime.Now.AddSeconds(-1);

                do
                {
                    tpath = Directory.GetCurrentDirectory() + "\\temp " +
                        time.AddSeconds(1).ToString().Replace(":", ".") + ".pdf";
                } while (File.Exists(tpath));

                status.Text = string.Empty;
            }

            try
            {
                using (var stream = new FileStream(tpath, FileMode.Create, FileAccess.ReadWrite))
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
                status.Text = ex.Message;
            }

            LoadPdfToMemory(tpath, ref streamC, pdfViewerC);

            status.Text = "Analysis is completed";
        }

        /// <summary>
        /// Create RAM temp file for save result
        /// </summary>
        private void CreateRAMFile()
        {
            //if (!string.IsNullOrEmpty(tpath))
            //    File.Delete(tpath);

            //tpath = Directory.GetCurrentDirectory() + "\\temp.pdf";

            /*
            if (File.Exists(tpath))
            {
                status.Text = "Creating temp-file";

                var time = DateTime.Now.AddSeconds(-1);

                do
                {
                    tpath = Directory.GetCurrentDirectory() + "\\temp " +
                        time.AddSeconds(1).ToString().Replace(":", ".") + ".pdf";
                } while (File.Exists(tpath));

                status.Text = string.Empty;
            }
            */

            string name = string.Empty;

            if (string.IsNullOrEmpty(tpath))
            {
                tpath = Directory.GetCurrentDirectory() + "\\temp " +
                        DateTime.Now.ToString().Replace(":", ".") + ".pdf";
            }
            name = "temp.pdf";

            try
            {
                mmFile = MemoryMappedFile.CreateNew(name, 4096, MemoryMappedFileAccess.ReadWrite);
                streamMMF = mmFile.CreateViewStream(0, 0, MemoryMappedFileAccess.ReadWrite);

                //using (mmFile = MemoryMappedFile.CreateNew(name, 4096, MemoryMappedFileAccess.ReadWrite))
                //using (var stream = new FileStream(tpath, FileMode.Create, FileAccess.ReadWrite))
                //using (streamMMF = mmFile.CreateViewStream(0, 0, MemoryMappedFileAccess.ReadWrite))
                using (var writer = new PdfWriter(streamMMF))
                using (var pdf = new PdfDocument(writer))
                using (var doc = new Document(pdf))
                {
                    writer.SetCloseStream(false);

                    doc.Add(new Paragraph("I'm Bogdan, I`m created this test-file."))
                        .Add(new Paragraph("Life is beautiful"));

                    // create file in RAM
                    //mmFile = MemoryMappedFile.CreateNew(name, stream.Length, MemoryMappedFileAccess.ReadWrite);
                    // create stream from file
                    //streamMMF = mmFile.CreateViewStream(0, stream.Length, MemoryMappedFileAccess.ReadWrite);
                    // copy data to stream in RAM
                    //stream.CopyTo(streamMMF);
                    //streamMMF.Position = 0;
                    //writer.Flush();
                    //streamMMF.Flush();
                }

            }
            catch (Exception ex)
            {
                status.Text = ex.Message;
            }

            LoadPdfToMemory(name, streamMMF, pdfViewerC);

            status.Text = "Analysis is completed";
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            string path = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];

            LoadPdfToMemory(path, ref streamL, pdfViewerL);
        }

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.AllowedEffect;
        }

        private void HelpMenu_Click(object sender, EventArgs e)
        {
            MessageBox.Show("in development...", "Additional information",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Load PDF file in stream
        /// </summary>
        internal void LoadPdfForRead()
        {
            try
            {
                if (new FileInfo(path).Extension != ".pdf")
                    throw new InvalidOperationException("File not in PDF format or corrupted");

                byte[] bytes = File.ReadAllBytes(path);
                streamC = new MemoryStream(bytes);
            }
            catch (Exception ex)
            {
                status.Text = ex.Message;
            }
        }

    }
}
