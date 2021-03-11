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
        /// Show header information
        /// </summary>
        internal static TextBox HeaderInfo { get; set; }
        /// <summary>
        /// Show metadata information
        /// </summary>
        internal static TextBox MetaDataInfo { get; set; }
        /// <summary>
        /// Show data in main table
        /// </summary>
        internal static DataGridView MainDataTable { get; set; }
        /// <summary>
        /// Show data in Oy table
        /// </summary>
        internal static DataGridView OyDataTable { get; set; }
        /// <summary>
        /// Show data in Ox table
        /// </summary>
        internal static DataGridView OxDataTable { get; set; }

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
                {
                    StreamL = stream;
                    NameOfDoc.Text = new FileInfo(Path).Name;
                }
                else
                    StreamC = stream;

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
        /// Show data in main table
        /// </summary>
        internal static void ShowMainDataTable(KeyValuePairTable<int, DateTime, float, bool>[] tableData)
        {
            // clear table
            MainDataTable.Columns.Clear();
            MainDataTable.Rows.Clear();

            // if the data is null then exit
            if (tableData == null)
                return;

            // instance size of table rows
            int Ny = tableData.Length;

            // add columns and their names
            MainDataTable.Columns.Add("Columns1", "Номер");
            MainDataTable.Columns.Add("Columns2", "Дата");
            MainDataTable.Columns.Add("Columns3", "Время");
            MainDataTable.Columns.Add("Columns4", "Т, °C");
            MainDataTable.Columns.Add(new DataGridViewCheckBoxColumn());
            MainDataTable.Columns[MainDataTable.Columns.Count - 1].HeaderText = "Нар.";

            // add formats of columns
            MainDataTable.Columns[0].DefaultCellStyle.Format = "N0";
            //MainDataTable.Columns[1].DefaultCellStyle.Format = "d";
            MainDataTable.Columns[1].DefaultCellStyle.Format = "dd.MM.yyyy";
            //MainDataTable.Columns[2].DefaultCellStyle.Format = "t";
            MainDataTable.Columns[2].DefaultCellStyle.Format = "HH:mm";
            MainDataTable.Columns[3].DefaultCellStyle.Format = "N1";

            // ability of correcting data
            MainDataTable.Columns[0].ReadOnly = true;
            MainDataTable.Columns[1].ReadOnly = true;
            MainDataTable.Columns[2].ReadOnly = true;
            MainDataTable.Columns[3].ReadOnly = false;
            MainDataTable.Columns[4].ReadOnly = false;

            // add rows to table
            MainDataTable.Rows.Add(Ny);

            // add data in table
            for (int i = 0; i < Ny; i++)
            {
                // counter for cell of row
                int j = 0;

                MainDataTable.Rows[i].Cells[j++].Value = tableData[i].Key;
                MainDataTable.Rows[i].Cells[j++].Value = tableData[i].DateTime;
                MainDataTable.Rows[i].Cells[j++].Value = tableData[i].DateTime;
                MainDataTable.Rows[i].Cells[j++].Value = tableData[i].Value;
                MainDataTable.Rows[i].Cells[j++].Value = tableData[i].OOR;
            }

            // fix cells
            //MainDataTable.AutoResizeColumnHeadersHeight();
            MainDataTable.AutoResizeColumns();
            MainDataTable.AutoResizeRows();
            MainDataTable.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);

            // remove selection of table
            MainDataTable.ClearSelection();

            // some parameters
            MainDataTable.AllowUserToAddRows = false;
            MainDataTable.AllowUserToDeleteRows = false;
            MainDataTable.AllowUserToOrderColumns = false;
        }

        /// <summary>
        /// Show data in Oy table
        /// </summary>
        internal static void ShowOyDataTable(int[] tableData)
        {
            DataGridView table = OyDataTable;

            // clear table
            table.Columns.Clear();
            table.Rows.Clear();

            // if the data is null then exit
            if (tableData == null)
                return;

            // instance size of table rows
            int Ny = tableData.Length;

            // add columns and their names
            table.Columns.Add("Columns1", "Номер");
            table.Columns.Add("Columns2", "Температура, C");

            // add formats of columns
            table.Columns[0].DefaultCellStyle.Format = "N0";
            table.Columns[1].DefaultCellStyle.Format = "N0";

            // ability of correcting data
            table.Columns[0].ReadOnly = true;
            table.Columns[1].ReadOnly = true;

            // add rows to table
            table.Rows.Add(Ny);

            // add data in table
            for (int i = 0; i < Ny; i++)
            {
                // counter for cell of row
                int j = 0;

                table.Rows[i].Cells[j++].Value = i + 1;
                table.Rows[i].Cells[j++].Value = tableData[i];
            }

            // fix cells
            //table.AutoResizeColumnHeadersHeight();
            table.AutoResizeColumns();
            table.AutoResizeRows();
            table.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);

            // remove selection of table
            table.ClearSelection();

            // some parameters
            table.AllowUserToAddRows = false;
            table.AllowUserToDeleteRows = false;
            table.AllowUserToOrderColumns = false;
        }

        /// <summary>
        /// Show data in Ox table
        /// </summary>
        internal static void ShowOxDataTable(DateTime[] tableData)
        {
            DataGridView table = OxDataTable;

            // clear table
            table.Columns.Clear();
            table.Rows.Clear();

            // if the data is null then exit
            if (tableData == null)
                return;

            // instance size of table rows
            int Ny = tableData.Length;

            // add columns and their names
            table.Columns.Add("Columns1", "Номер");
            table.Columns.Add("Columns2", "Время");
            table.Columns.Add("Columns3", "Дата");

            // add formats of columns
            table.Columns[0].DefaultCellStyle.Format = "N0";
            //table.Columns[1].DefaultCellStyle.Format = "t";
            table.Columns[1].DefaultCellStyle.Format = "HH:mm";
            //table.Columns[2].DefaultCellStyle.Format = "d";
            table.Columns[2].DefaultCellStyle.Format = "dd.MM.yyyy";

            // ability of correcting data
            table.Columns[0].ReadOnly = true;
            table.Columns[1].ReadOnly = true;
            table.Columns[2].ReadOnly = true;

            // add rows to table
            table.Rows.Add(Ny);

            // add data in table
            for (int i = 0; i < Ny; i++)
            {
                // counter for cell of row
                int j = 0;

                table.Rows[i].Cells[j++].Value = i + 1;
                table.Rows[i].Cells[j++].Value = tableData[i];
                table.Rows[i].Cells[j++].Value = tableData[i];
            }

            // fix cells
            //table.AutoResizeColumnHeadersHeight();
            table.AutoResizeColumns();
            table.AutoResizeRows();
            table.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);

            // remove selection of table
            table.ClearSelection();

            // some parameters
            table.AllowUserToAddRows = false;
            table.AllowUserToDeleteRows = false;
            table.AllowUserToOrderColumns = false;
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
