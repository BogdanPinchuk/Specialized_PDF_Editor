using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

using System;
using System.IO;
using System.Windows.Forms;

namespace Specialized_PDF_Editor
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Stream for loaded file
        /// </summary>
        private MemoryStream streamL;
        /// <summary>
        /// Stream for created file
        /// </summary>
        private MemoryStream streamC;
        /// <summary>
        /// Path for temp-file
        /// </summary>
        private string path;

        public MainForm(string[] pathes)
        {
            InitializeComponent();

            if (pathes.Length > 0)
            {
                status.Text = pathes[0];

                if (!string.IsNullOrEmpty(pathes[0]))
                    LoadPdfToMemory(pathes[0], ref streamL, pdfViewerL);
            }
        }

        /// <summary>
        /// Load pdf-file to RAM
        /// </summary>
        /// <param name="path">path for file</param>
        /// <param name="stream">stream for document</param>
        /// <param name="pdfViewer">Component with view the document</param>
        private void LoadPdfToMemory(string path, ref MemoryStream stream, PdfiumViewer.PdfViewer pdfViewer)
        {
            try
            {
                byte[] bytes = File.ReadAllBytes(path);
                stream = new MemoryStream(bytes);
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
                    if (pdfViewerC.Document == null)
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

            if (!string.IsNullOrEmpty(path))
                File.Delete(path);

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
            path = Directory.GetCurrentDirectory() + "\\temp.pdf";

            using (var stream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
            using (var writer = new PdfWriter(stream))
            using (var pdf = new PdfDocument(writer))
            using (var doc = new Document(pdf))
            {
                doc.Add(new Paragraph("I'm Bogdan, I`m ccreated this test-file."))
                    .Add(new Paragraph("Life is beautiful"));
            }

            LoadPdfToMemory(path, ref streamC, pdfViewerC);

            status.Text = "This work is completed.";
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

    }
}
