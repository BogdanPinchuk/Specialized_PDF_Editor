using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

using System;
using System.Collections.Generic;
using System.Drawing;
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
        internal static PictureBox Chart;

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
            DataGridView table = MainDataTable;

            // clear table
            table.Columns.Clear();
            table.Rows.Clear();

            // if the data is null then exit
            if (tableData == null)
                return;

            // instance size of table rows
            int Ny = tableData.Length;

            // add columns and their names
            table.Columns.Add("Key", "Номер");
            table.Columns.Add("Date", "Дата");
            table.Columns.Add("Time", "Время");
            table.Columns.Add("Value", "Т, °C");
            table.Columns.Add(new DataGridViewCheckBoxColumn());
            table.Columns[table.Columns.Count - 1].Name = "OOR";
            table.Columns["OOR"].HeaderText = "Нар.";

            // add formats of columns
            table.Columns["Key"].DefaultCellStyle.Format = "N0";
            //table.Columns["Date"].DefaultCellStyle.Format = "d";
            table.Columns["Date"].DefaultCellStyle.Format = "dd.MM.yyyy";
            //table.Columns["Time"].DefaultCellStyle.Format = "t";
            table.Columns["Time"].DefaultCellStyle.Format = "HH:mm";
            table.Columns["OOR"].DefaultCellStyle.Format = "N1";

            // ability of correcting data
            table.Columns["Key"].ReadOnly = true;
            table.Columns["Date"].ReadOnly = true;
            table.Columns["Time"].ReadOnly = true;
            table.Columns["Value"].ReadOnly = false;
            table.Columns["OOR"].ReadOnly = false;

            // add rows to table
            table.Rows.Add(Ny);

            // add data in table
            for (int i = 0; i < Ny; i++)
            {
                table.Rows[i].Cells["Key"].Value = tableData[i].Key;
                table.Rows[i].Cells["Date"].Value = tableData[i].DateTime;
                table.Rows[i].Cells["Time"].Value = tableData[i].DateTime;
                table.Rows[i].Cells["Value"].Value = tableData[i].Value;
                table.Rows[i].Cells["OOR"].Value = tableData[i].OOR;
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
            table.AllowUserToOrderColumns = true;
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
            table.Columns.Add("Key", "Номер");
            table.Columns.Add("Value", "Температура, C");

            // add formats of columns
            table.Columns["Key"].DefaultCellStyle.Format = "N0";
            table.Columns["Value"].DefaultCellStyle.Format = "N0";

            // ability of correcting data
            table.Columns["Key"].ReadOnly = true;
            table.Columns["Value"].ReadOnly = true;

            // add rows to table
            table.Rows.Add(Ny);

            // add data in table
            for (int i = 0; i < Ny; i++)
            {
                table.Rows[i].Cells["Key"].Value = i + 1;
                table.Rows[i].Cells["Value"].Value = tableData[i];
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
            table.AllowUserToOrderColumns = true;
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
            table.Columns.Add("Key", "Номер");
            table.Columns.Add("Time", "Время");
            table.Columns.Add("Date", "Дата");

            // add formats of columns
            table.Columns["Key"].DefaultCellStyle.Format = "N0";
            //table.Columns["Time"].DefaultCellStyle.Format = "t";
            table.Columns["Time"].DefaultCellStyle.Format = "HH:mm";
            //table.Columns["Date"].DefaultCellStyle.Format = "d";
            table.Columns["Date"].DefaultCellStyle.Format = "dd.MM.yyyy";

            // ability of correcting data
            table.Columns["Key"].ReadOnly = true;
            table.Columns["Time"].ReadOnly = true;
            table.Columns["Date"].ReadOnly = true;

            // add rows to table
            table.Rows.Add(Ny);

            // add data in table
            for (int i = 0; i < Ny; i++)
            {
                table.Rows[i].Cells["Key"].Value = i + 1;
                table.Rows[i].Cells["Time"].Value = tableData[i];
                table.Rows[i].Cells["Date"].Value = tableData[i];
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
            table.AllowUserToOrderColumns = true;
        }

        /// <summary>
        /// Events when user changing values in tables
        /// </summary>
        /// <param name="row">Select row</param>
        /// <param name="col">Select column</param>
        /// <param name="tableData">Table which changing</param>
        internal static void DoingChanges(int row, int col, KeyValuePairTable<int, DateTime, float, bool>[] tableData)
        {
            var table = MainDataTable;

            // check needed of columns
            if (table.Columns[col].Name != "Value" &&
                table.Columns[col].Name != "OOR")
                return;

            if (table.Columns[col].Name == "Value")
            {
                // new enter value
                string enterValue = table.CurrentCell.Value.ToString().Replace(".", ",");

                // convert new value
                float temp;
                bool canChange = float.TryParse(enterValue, out temp);

                // save result
                if (canChange && (temp >= -273.15f))
                    tableData[row] =
                        new KeyValuePairTable<int, DateTime, float, bool>(tableData[row].Key,
                        tableData[row].DateTime, temp, tableData[row].OOR);
                else
                    table.CurrentCell.Value = tableData[row].Value;
            }
            else
            {
                // save result
                tableData[row] = new KeyValuePairTable<int, DateTime, float, bool>(tableData[row].Key,
                        tableData[row].DateTime, tableData[row].Value, (bool)table.CurrentCell.Value);
            }
        }

        /// <summary>
        /// Present data on chart
        /// </summary>
        /// <param name="analysis">Data of analysis pdf-file</param>
        /// <param name="graph">graphics parameter</param>
        internal static void ShowChart(Analysis analysis, Graphics graph)
        {
            PictureBox chart = Chart;
            //KeyValuePairTable<int, DateTime, float, bool>[] tableData = analysis.TableData;

            // prepare data
            string[] oyAxisData = analysis.DataOyAxis.AsParallel().AsOrdered()
                .Select(t => t.ToString("N0")).ToArray(),
                oxAxisData = analysis.DataOxAxis.AsParallel().AsOrdered()
                .Select(t => t.ToString("HH:mm") + "\n" + t.ToString("dd.MM.yyyy")).ToArray();
            float minY = analysis.DataOyAxis.Min(),
                maxY = analysis.DataOyAxis.Max(),
                minX = 0,
                maxX = (float)(analysis.TableData[analysis.TableData.Length - 1].DateTime - 
                    analysis.TableData[0].DateTime).TotalMinutes;  // convert to minutes
            //TODO: Added using the customer range


            // instance limits of chart
            float realWidth = 277.81f,
                realHeight = 158.75f;

            // height and width of picture
            int width = (int)(analysis.Pages[analysis.PageCount - 1].Size.WidthUU * realWidth /
                    analysis.Pages[analysis.PageCount - 1].Size.Width),
                height = (int)(analysis.Pages[analysis.PageCount - 1].Size.HeightUU * realHeight /
                    analysis.Pages[analysis.PageCount - 1].Size.Height);

            // instance size of graph
            Rectangle plotArea = new Rectangle(0, 0, width, height);

            // names of header and axises
            string sTitle = "График температуры",
                sOyLabel = "Температура, C",
                sOxLabel = "Время";

            // names of legends
            string[] legends =
            {
                "Температура, C",
                "Верхняя граница",
                "Ниж няя граница",
            };

            // value of colours from chart
            Color temperature = Color.FromArgb(0, 250, 0),
                topLimit = Color.FromArgb(238, 29, 37),
                bottomLimit = Color.FromArgb(0, 0, 250),
                gridAxis = Color.FromArgb(200, 200, 200),
                borderChart = Color.FromArgb(0, 0, 0);

            // instance font and height of text
            string fontText = "Tahoma";
            Font fTitle = new Font(fontText, 10.5f, FontStyle.Regular),
                fAxis = new Font(fontText, 9.75f, FontStyle.Regular),
                fLegend = new Font(fontText, 10.5f, FontStyle.Regular);
            StringFormat sFormat = new StringFormat()
            {
                Alignment = StringAlignment.Center
            };

            // calculate size of fonts
            SizeF titleFS = graph.MeasureString(sTitle, fTitle),
                oyLabelFS = graph.MeasureString(sOyLabel, fAxis),
                oxLabelFS = graph.MeasureString(sOxLabel, fAxis),
                legendsFS = new SizeF(legends.Select(t => graph.MeasureString(t, fAxis).Width).Max(),
                    legends.Select(t => graph.MeasureString(t, fAxis).Height).Max()),
                //legendsFS_ = graph.MeasureString(legends
                //    .Where(t => t.Length >= legends.Select(i => i.Length).Max())
                //    .ElementAt(0), fAxis);  // find the max length
                gridOyFS = new SizeF(oyAxisData.Select(t => graph.MeasureString(t, fAxis).Width).Max(),
                    oyAxisData.Select(t => graph.MeasureString(t, fAxis).Height).Max()),
                gridOxFs = new SizeF(oxAxisData.Select(t => graph.MeasureString(t, fAxis).Width).Max(),
                    oxAxisData.Select(t => graph.MeasureString(t, fAxis).Height).Max());

            // coeficient of scale ("zoom")
            double kfY = plotArea.Height / (maxY - minY),
                kfX = plotArea.Width / (maxX - minX);

            // recalculate points relevant to scale
            PointF[] plot = new PointF[analysis.TableData.Length];

            Parallel.For(0, analysis.TableData.Length, i =>
                plot[i] = new PointF(PlotLimits(plotArea.Left, plotArea.Right, (float)(plotArea.Left + kfX *
                    (analysis.TableData[analysis.TableData.Length - 1].DateTime - 
                    analysis.TableData[i].DateTime).TotalMinutes)),
                    PlotLimits(plotArea.Top, plotArea.Bottom, (float)(plotArea.Bottom - kfY * 
                    (maxY - analysis.TableData[i].Value)))));


        }

        /// <summary>
        /// Limiting of plot
        /// </summary>
        /// <param name="min">min value</param>
        /// <param name="max">max value</param>
        /// <param name="value">value</param>
        /// <returns></returns>
        private static float PlotLimits(float min, float max, float value)
            => (value < min) ? min : ((value > max) ? max : value);


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
