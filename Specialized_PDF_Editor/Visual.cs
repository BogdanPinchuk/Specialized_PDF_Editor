using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

using MathNet.Numerics.Random;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
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
        /// Type of random data series
        /// </summary>
        private enum TypeOfRandom
        {
            dataScale,
            dataRandomB,
            dataSRandomS,
            dataCRandomS,
            dataMTwister,
            dataXorshift,
            dataXoshiro256SS,
            dataMcg31m1,
            dataMcg59,
            dataWH1982,
            dataWH2006,
            dataMrg32k3a,
            dataPalf,
        }

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
        /// Area for chart
        /// </summary>
        internal static PictureBox Chart;
        /// <summary>
        /// Button for chose color of temperature
        /// </summary>
        internal static Button TempColor { get; set; }
        /// <summary>
        /// Button for chose color of top limit
        /// </summary>
        internal static Button TopColor { get; set; }
        /// <summary>
        /// Button for chose color of bottom limit
        /// </summary>
        internal static Button BottomColor { get; set; }
        /// <summary>
        /// Button for chose font for text
        /// </summary>
        internal static Button FontText { get; set; }

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
        /// <param name="tableData">Input data for show in table</param>
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
            table.Columns["Value"].DefaultCellStyle.Format = "N1";

            // ability of correcting data
            table.Columns["Key"].ReadOnly = true;
            table.Columns["Date"].ReadOnly = true;
            table.Columns["Time"].ReadOnly = true;
            table.Columns["Value"].ReadOnly = false;
            table.Columns["OOR"].ReadOnly = true;

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
        /// <param name="analysis">Data of analysis pdf-file</param>
        internal static void DoingChanges(int row, int col, Analysis analysis)
        {
            var tableData = analysis.TableData;
            var table = MainDataTable;

            // check needed of columns
            if (table.Columns[col].Name != "Value")
                return;

            // new enter value
            string enterValue = table.CurrentCell.Value.ToString().Replace(".", ",");

            // convert new value
            bool canChange = float.TryParse(enterValue, out float temp);

            // save result
            if (canChange && (temp >= -273.15f))
            {
                tableData[row] =
                    new KeyValuePairTable<int, DateTime, float, bool>(tableData[row].Key,
                    tableData[row].DateTime, temp, tableData[row].OOR);

                // save result + second using canChange value for save result
                canChange = !(analysis.LimitMin <= temp && temp <= analysis.LimitMax);

                tableData[row] = new KeyValuePairTable<int, DateTime, float, bool>(tableData[row].Key,
                        tableData[row].DateTime, tableData[row].Value, canChange);
                table.Rows[row].Cells["OOR"].Value = canChange;

                // change header
                analysis.ChangeHeader();
            }
            else
                table.CurrentCell.Value = tableData[row].Value;
        }

        /// <summary>
        /// Present data on chart
        /// </summary>
        /// <param name="analysis">Data of analysis pdf-file</param>
        /// <param name="graph">graphics parameter</param>
        /// <param name="rect">size of area for draw</param>
        internal static void ShowChart(Analysis analysis, Graphics graph, Rectangle rect)
        {
            PictureBox chart = Chart;

            // names of header and axises
            string sTitle = "График температуры",
                sOyLabel = "Температура, C",
                sOxLabel = "Время";

            // names of legends
            string[] legends =
            {
                "Температура, C",
                "Верхняя граница",
                "Нижняя граница",
            };

            // prepare data
            string[] oyAxisData = analysis.DataOyAxis.AsParallel().AsOrdered()
                .Select(t => t.ToString("N0")).ToArray(),
                oxAxisData = analysis.DataOxAxis.AsParallel().AsOrdered()
                .Select(t => t.ToString("HH:mm") + "\n" + t.ToString("dd.MM.yyyy")).ToArray();

            // value of colours from chart
            Color temperature = TempColor.BackColor,
                topLimit = TopColor.BackColor,
                bottomLimit = BottomColor.BackColor,
                gridAxis = Color.FromArgb(200, 200, 200),
                borderChart = Color.FromArgb(0, 0, 0);  // and text
            //Color temperature = Color.FromArgb(0, 250, 0),
            //    topLimit = Color.FromArgb(238, 29, 37),
            //    bottomLimit = Color.FromArgb(0, 0, 250),
            //    gridAxis = Color.FromArgb(200, 200, 200),
            //    borderChart = Color.FromArgb(0, 0, 0);  // and text

            // instance font and height of text
            string fontText = FontText.Font.Name;
            //string fontText = "Tahoma";
            Font fTitle = new Font(fontText, 10.5f, FontStyle.Regular),
                fAxis = new Font(fontText, 9.75f, FontStyle.Regular),
                fLegend = new Font(fontText, 10.5f, FontStyle.Regular);
            StringFormat sFormat = new StringFormat()
            {
                Alignment = StringAlignment.Center
            };
            // TODO: do scale text

            // calculate size of fonts
            SizeF titleFS = graph.MeasureString(sTitle, fTitle),
                oyLabelFS = graph.MeasureString(sOyLabel, fAxis),
                oxLabelFS = graph.MeasureString(sOxLabel, fAxis),
                legendsFS = new SizeF(legends.Select(t => graph.MeasureString(t, fAxis).Width).Max(),
                    legends.Select(t => graph.MeasureString(t, fAxis).Height).Max()),
                gridOyFS = new SizeF(oyAxisData.Select(t => graph.MeasureString(t, fAxis).Width).Max(),
                    oyAxisData.Select(t => graph.MeasureString(t, fAxis).Height).Max()),
                gridOxFS = new SizeF(oxAxisData.Select(t => graph.MeasureString(t, fAxis).Width).Max(),
                    oxAxisData.Select(t => graph.MeasureString(t, fAxis).Height).Max());
            //legendsFS_ = graph.MeasureString(legends
            //    .Where(t => t.Length >= legends.Select(i => i.Length).Max())
            //    .ElementAt(0), fAxis);  // find the max length

            // min and max of axis
            float minY = analysis.DataOyAxis.Min(),
                maxY = analysis.DataOyAxis.Max(),
                minX = 0.0f,
                maxX = (float)(analysis.TableData[analysis.TableData.Length - 1].DateTime -
                    analysis.TableData[0].DateTime).TotalMinutes;  // convert to minutes

            // min and max of Oy axis
            float minOy, maxOy;

            {
                // position of data Oy axis and minimum possible height
                float first_Oy = analysis.PositionOyAxis[0].Rect.GetY(),
                    last_Oy = analysis.PositionOyAxis[analysis.PositionOyAxis.Length - 1].Rect.GetY(),
                    minPosH = 103f,
                    maxPosH = 485.5f;


                List<float> dif = new List<float>();
                //for (int i = 1; i < analysis.PositionOyAxis.Length; i++)
                //    dif.Add(analysis.PositionOyAxis[i].Rect.GetY() - analysis.PositionOyAxis[i - 1].Rect.GetY());
                Parallel.For(1, analysis.PositionOyAxis.Length, i =>
                    dif.Add(Math.Abs(analysis.PositionOyAxis[i].Rect.GetY() - analysis.PositionOyAxis[i - 1].Rect.GetY())));

                // step between two neghtbour values
                float stepPdf = dif.Max(),
                    stepValue = Math.Abs(analysis.DataOyAxis[1] - analysis.DataOyAxis[0]);

                minOy = analysis.DataOyAxis[analysis.DataOyAxis.Length - 1] - Math.Abs(minPosH - first_Oy) * stepValue / stepPdf;
                maxOy = analysis.DataOyAxis[0] + Math.Abs(maxPosH - last_Oy) * stepValue / stepPdf;
            }

            // draw work space "clean"
            graph.FillRectangle(new SolidBrush(Color.White), rect);

            // height and width of picture
            int width = rect.Width,
                height = rect.Height;

            // instance size of graph
            Rectangle plotArea = rect;
            plotArea.X = 20 + (int)(oyLabelFS.Height + gridOyFS.Width);
            plotArea.Y = 20 + (int)titleFS.Height;
            plotArea.Width = 15 + width - plotArea.Left - (int)gridOyFS.Width;
            plotArea.Height = height - 10 - plotArea.Top - (int)(gridOxFS.Height + oxLabelFS.Height);

            // draw area of charts and brush (can delete)
            graph.FillRectangle(new SolidBrush(Color.White), plotArea);

            // draw axis in area chart
            Pen gPen = new Pen(borderChart, 1f)
            {
                DashStyle = DashStyle.Solid,
            };

            // coeficient of scale ("zoom")
            float kfY = plotArea.Height / Math.Abs(maxOy - minOy),
                kfX = plotArea.Width / Math.Abs(maxX - minX);

            // draw helping grid, big and small
            {
                // step of grid
                float stepX = plotArea.Width / (float)(6 * 4 + 1),
                    stepY = plotArea.Height / (float)(6 * 4 + 1);

                // right - small
                for (float i = plotArea.Left; i < plotArea.Right; i += stepX)
                    graph.DrawLine(gPen, i, plotArea.Top, i, plotArea.Top + 3);
                // right - big
                for (float i = plotArea.Left; i < plotArea.Right; i += 4f * stepX)
                    graph.DrawLine(gPen, i, plotArea.Top, i, plotArea.Top + 6);
                // top - small
                for (float i = plotArea.Bottom; i > plotArea.Top; i -= stepY)
                    graph.DrawLine(gPen, plotArea.Right, i, plotArea.Right - 3, i);
                // top - big
                for (float i = plotArea.Bottom; i > plotArea.Top; i -= 4f * stepY)
                    graph.DrawLine(gPen, plotArea.Right, i, plotArea.Right - 6, i);
            }

            // step for grid
            float dX = kfX * (float)(analysis.DataOxAxis[1] - analysis.DataOxAxis[0]).TotalMinutes / 4,
                dY = 2 * kfY;
            {
                // step of help grid
                float stepY = kfY * Math.Abs(analysis.DataOyAxis[1] - analysis.DataOyAxis[0]);

                // start grid text
                float beginY = Math.Abs(minY - minOy) * kfY,
                    beginX = (float)Math.Abs((analysis.DataOxAxis[0] - analysis.TableData[0].DateTime).TotalMinutes) * kfX;

                // draw grid
                gPen = new Pen(gridAxis, 1f)
                {
                    DashStyle = DashStyle.Dash,
                    DashPattern = new float[] { 2f, 3f },
                };
                // bottom - big
                for (float i = plotArea.Left + beginX; i < plotArea.Right; i += 4f * dX)
                    graph.DrawLine(gPen, i, plotArea.Bottom, i, plotArea.Top);
                // left - big
                for (float i = plotArea.Bottom - beginY; i > plotArea.Top; i -= stepY)
                    graph.DrawLine(gPen, plotArea.Left, i, plotArea.Right, i);

                // sign value of grid
                // bottom sing
                sFormat.Alignment = StringAlignment.Far;

                // vertical values
                {
                    float j = plotArea.Bottom - beginY;
                    for (int i = 0; i < oyAxisData.Length; i++, j -= stepY)
                        graph.DrawString(oyAxisData[oyAxisData.Length - 1 - i], fAxis, new SolidBrush(borderChart),
                            new PointF(plotArea.Left - 5, j - gridOyFS.Height / 2), sFormat);
                }

                // left sing
                sFormat.Alignment = StringAlignment.Center;

                // horizontal values
                {
                    float j = plotArea.Left + beginX;
                    for (int i = 0; i < oxAxisData.Length; i++, j += 4f * dX)
                        graph.DrawString(oxAxisData[i], fAxis, new SolidBrush(borderChart),
                            new PointF(j, plotArea.Bottom + 5), sFormat);
                }

                #region draw help grid
                gPen = new Pen(borderChart, 1f)
                {
                    DashStyle = DashStyle.Solid,
                };
                // bottom (left)
                for (float i = plotArea.Left + beginX; i > plotArea.Left; i -= dX)
                    graph.DrawLine(gPen, i, plotArea.Bottom, i, plotArea.Bottom - 3);
                // bottom (right)
                for (float i = plotArea.Left + beginX; i < plotArea.Right; i += dX)
                    graph.DrawLine(gPen, i, plotArea.Bottom, i, plotArea.Bottom - 3);
                // bottom - big
                for (float i = plotArea.Left + beginX; i < plotArea.Right; i += 4f * dX)
                    graph.DrawLine(gPen, i, plotArea.Bottom, i, plotArea.Bottom - 6);

                // left (up)
                for (float i = plotArea.Bottom - beginY; i > plotArea.Top; i -= dY)
                    graph.DrawLine(gPen, plotArea.Left, i, plotArea.Left + 3, i);
                // left (down)
                for (float i = plotArea.Bottom - beginY; i < plotArea.Bottom; i += dY)
                    graph.DrawLine(gPen, plotArea.Left, i, plotArea.Left + 3, i);
                // left - big
                for (float i = plotArea.Bottom - beginY; i > plotArea.Top; i -= stepY)
                    graph.DrawLine(gPen, plotArea.Left, i, plotArea.Left + 6, i);
                #endregion

                // recalculate points relevant to scale
                PointF[] plot = new PointF[analysis.TableData.Length];

                Parallel.For(0, analysis.TableData.Length, i =>
                    plot[i] = new PointF(PlotLimits(plotArea.Left, plotArea.Right, (float)(plotArea.Left + kfX *
                        (analysis.TableData[i].DateTime - analysis.TableData[0].DateTime).TotalMinutes)),
                        PlotLimits(plotArea.Top, plotArea.Bottom,
                        (float)(plotArea.Bottom - kfY * (analysis.TableData[i].Value - minOy)))));

                // show chart
                gPen.DashStyle = DashStyle.Solid;
                gPen.Color = temperature;
                gPen.Width = 2f;

                // draw chart (1-st out of plot area range)
                //graph.DrawLines(gPen, plot);
                for (int i = 1; i < plot.Length; i++)
                    graph.DrawLine(gPen, plot[i - 1], plot[i]);

                // draw limit lines
                gPen.Color = topLimit;
                graph.DrawLine(gPen, plotArea.Left, plotArea.Bottom - kfY * Math.Abs(analysis.LimitMax - minOy),
                    plotArea.Right, plotArea.Bottom - kfY * Math.Abs(analysis.LimitMax - minOy));
                gPen.Color = bottomLimit;
                graph.DrawLine(gPen, plotArea.Left, plotArea.Bottom - kfY * Math.Abs(analysis.LimitMin - minOy),
                    plotArea.Right, plotArea.Bottom - kfY * Math.Abs(analysis.LimitMin - minOy));
            }

            // sign axises and header
            graph.DrawString(sTitle, fTitle, new SolidBrush(borderChart),
                new PointF(rect.Left + rect.Width / 2, 5), sFormat);
            graph.DrawString(sOxLabel, fAxis, new SolidBrush(borderChart),
                new PointF(rect.Left + rect.Width / 2, rect.Bottom - 20), sFormat);
            // save parameters of area
            GraphicsState gState = graph.Save();
            graph.TranslateTransform(5, rect.Top + rect.Height / 2);
            // rotate text
            graph.RotateTransform(-90);
            graph.DrawString(sOyLabel, fAxis, new SolidBrush(borderChart),
                new PointF(0, 0), sFormat);
            // restore parameters
            graph.Restore(gState);

            // draw border chart (can change colour of  background of chart)
            gPen = new Pen(borderChart, 2f)
            {
                DashStyle = DashStyle.Solid,
            };

            graph.DrawRectangle(gPen, plotArea);

            //TODO: draw legends
            {
                // area for legends
                Rectangle l_rect = new Rectangle((int)(plotArea.Right - plotArea.Width * 1.05 / 6.25),
                     (int)(plotArea.Bottom - plotArea.Height * 1.15 / 7.25),
                     (int)(plotArea.Width / 6.25), (int)(plotArea.Height / 7.25));

                // draw area
                graph.FillRectangle(new SolidBrush(Color.FromArgb((byte)(0.9 * byte.MaxValue), Color.White)), l_rect);

                // draw border
                gPen.Width = 1f;
                graph.DrawRectangle(gPen, l_rect);

                // imaginary division
                float partY = l_rect.Height / 3f,
                    partX = l_rect.Width / 3f;

                // draw lines
                gPen.Width = 2f;
                gPen.Color = temperature;
                graph.DrawLine(gPen, l_rect.Left + partX / 6, l_rect.Top + partY / 2,
                    l_rect.Left + partX - partX / 8, l_rect.Top + partY / 2);
                gPen.Color = topLimit;
                graph.DrawLine(gPen, l_rect.Left + partX / 6, l_rect.Top + 3 * partY / 2,
                    l_rect.Left + partX - partX / 8, l_rect.Top + 3 * partY / 2);
                gPen.Color = bottomLimit;
                graph.DrawLine(gPen, l_rect.Left + partX / 6, l_rect.Top + 5 * partY / 2,
                    l_rect.Left + partX - partX / 8, l_rect.Top + 5 * partY / 2);

                // draw text sing legend lines
                sFormat.Alignment = StringAlignment.Near;
                for (int i = 0; i < legends.Length; i++)
                    graph.DrawString(legends[i], fLegend, new SolidBrush(borderChart),
                            new PointF(l_rect.Left + partX,
                            l_rect.Top + (i + 0.5f) * partY - legendsFS.Height / 2), sFormat);
            }

            // dispose graphics
            gPen.Dispose();
        }

        /// <summary>
        /// Clear area of chart
        /// </summary>
        internal static void ClearChart()
            => Chart
            .CreateGraphics()
            .Clear(Chart.BackColor);

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
        /// Scaling data within acceptable limits (first variant for testing)
        /// </summary>
        /// <param name="analysis">Data of analysis pdf-file</param>
        internal unsafe static void Array_Scale(Analysis analysis)
        {
            // copy from reserve
            var tableData = analysis.TableDataBlock;
            float limitMin = analysis.LimitMin + 0.1f,
                limitMax = analysis.LimitMax - 0.1f;

            // max and min values needed for scale
            float max_all = analysis.TableDataBlock
                .AsParallel()
                .Where(t => (limitMin <= t.Value) && (t.Value <= limitMax))
                .Select(t => t.Value)
                .Max(),
                min_all = analysis.TableDataBlock
                .AsParallel()
                .Where(t => (limitMin <= t.Value) && (t.Value <= limitMax))
                .Select(t => t.Value)
                .Min();

            // create new array for out of range values
            var array_temp_min = tableData
                .AsParallel()
                .AsOrdered()
                .Where(t => t.Value <= limitMin)
                .ToArray();
            var array_temp_max = tableData
                .AsParallel()
                .AsOrdered()
                .Where(t => t.Value >= limitMax)
                .ToArray();

            // extract data values of temperature
            float[] array_temp_values_min = array_temp_min
                .AsParallel()
                .AsOrdered()
                .Select(t => t.Value)
                .ToArray(),
                array_temp_values_max = array_temp_max
                .AsParallel()
                .AsOrdered()
                .Select(t => t.Value)
                .ToArray();

            // analysis out of range up and down
            bool oor_up = array_temp_values_max.Length > 0,
                oor_down = array_temp_values_min.Length > 0;

            // scaling
            if (oor_up)
            {
                // max and min values needed for scale
                float max = array_temp_values_max.Max();

                // array for result
                float[] res_max = new float[array_temp_values_max.Length];

                // block data in RAM
                fixed (float* ar = array_temp_values_max, res = res_max)
                {
                    // instance values for pointers
                    float* _ar = ar, _res = res;

                    for (int i = 0; i < res_max.Length; i++, _ar++, _res++)
                        *_res = (*_ar - min_all) * (limitMax - min_all) / (max - min_all) + min_all;

                    // find index
                    int[] id = array_temp_max
                        .AsParallel()
                        .AsOrdered()
                        .Select(t => t.Key)
                        .ToArray();

                    // save data
                    Parallel.For(0, id.Length, i => analysis.TableData[id[i] - 1] =
                            new KeyValuePairTable<int, DateTime, float, bool>(id[i],
                            array_temp_max[i].DateTime, res_max[i], false));
                }
            }

            if (oor_down)
            {
                // max and min values needed for scale
                float min = array_temp_values_min.Min();

                // array for result
                float[] res_min = new float[array_temp_values_min.Length];

                // block data in RAM
                fixed (float* ar = array_temp_values_min, res = res_min)
                {
                    // instance values for pointers
                    float* _ar = ar, _res = res;

                    for (int i = 0; i < res_min.Length; i++, _ar++, _res++)
                        *_res = max_all - (max_all - *_ar) * (max_all - limitMin) / (max_all - min);

                    // find index
                    int[] id = array_temp_min
                        .AsParallel()
                        .AsOrdered()
                        .Select(t => t.Key)
                        .ToArray();

                    // save data
                    Parallel.For(0, id.Length, i => analysis.TableData[id[i] - 1] =
                            new KeyValuePairTable<int, DateTime, float, bool>(id[i],
                            array_temp_min[i].DateTime, res_min[i], false));
                }
            }

            // change header
            analysis.ChangeHeader();
        }

        /// <summary>
        /// Auto modify data within acceptable limits
        /// </summary>
        /// <param name="analysis">Data of analysis pdf-file</param>
        /// <param name="menuItem">Variant of chose strip menu</param>
        internal unsafe static void Array_Modify(Analysis analysis, ToolStripMenuItem menuItem)
        {
            // variant of strip menu
            bool canContinue = Enum.TryParse(menuItem.Name, out TypeOfRandom varOfSM);

            // if value is not corect, method must be exit
            if (!canContinue)
                return;

            // copy from reserve
            var tableData = analysis.TableDataBlock;
            float limitMin = analysis.LimitMin + 0.1f,
                limitMax = analysis.LimitMax - 0.1f;
            // for testing
            //float limitMin = 24f,
            //    limitMax = 36f;

            // max and min values needed for scale
            float max_all = analysis.TableDataBlock
                .AsParallel()
                .Where(t => (limitMin <= t.Value) && (t.Value <= limitMax))
                .Select(t => t.Value)
                .Max(),
                min_all = analysis.TableDataBlock
                .AsParallel()
                .Where(t => (limitMin <= t.Value) && (t.Value <= limitMax))
                .Select(t => t.Value)
                .Min();

            // create new array for out of range values
            var array_temp_min = tableData
                .AsParallel()
                .AsOrdered()
                .Where(t => t.Value <= limitMin)
                .ToArray();
            var array_temp_max = tableData
                .AsParallel()
                .AsOrdered()
                .Where(t => t.Value >= limitMax)
                .ToArray();

            // extract data values of temperature
            float[] array_temp_values_min = array_temp_min
                .AsParallel()
                .AsOrdered()
                .Select(t => t.Value)
                .ToArray(),
                array_temp_values_max = array_temp_max
                .AsParallel()
                .AsOrdered()
                .Select(t => t.Value)
                .ToArray();

            // analysis out of range up and down
            bool oor_up = array_temp_values_max.Length > 0,
                oor_down = array_temp_values_min.Length > 0;

            // scatter for random
            float scatter = 1.0f;

            // scaling
            if (oor_up)
            {
                // max and min values needed for scale
                float max = array_temp_values_max.Max();

                // array for result
                float[] res_max = new float[array_temp_values_max.Length];

                // create random coeficient for scaling
                float[] array_coef = CreateRDA(res_max.Length, varOfSM);

                // block data in RAM
                fixed (float* ar = array_temp_values_max, coef = array_coef, res = res_max)
                {
                    // instance values for pointers
                    float* _ar = ar, _c = coef, _res = res;

                    for (int i = 0; i < res_max.Length; i++, _ar++, _c++, _res++)
                        *_res = scatter * (*_c - 1.0f) + min_all + (*_ar - min_all) * (limitMax - min_all) / (max - min_all);

                    // find index
                    int[] id = array_temp_max
                        .AsParallel()
                        .AsOrdered()
                        .Select(t => t.Key)
                        .ToArray();

                    // save data
                    Parallel.For(0, id.Length, i => analysis.TableData[id[i] - 1] =
                            new KeyValuePairTable<int, DateTime, float, bool>(id[i],
                            array_temp_max[i].DateTime, res_max[i], false));
                }
            }

            if (oor_down)
            {
                // max and min values needed for scale
                float min = array_temp_values_min.Min();

                // array for result
                float[] res_min = new float[array_temp_values_min.Length];

                // create random coeficient for scaling
                float[] array_coef = CreateRDA(res_min.Length, varOfSM);

                // block data in RAM
                fixed (float* ar = array_temp_values_min, coef = array_coef, res = res_min)
                {
                    // instance values for pointers
                    float* _ar = ar, _c = coef, _res = res;

                    for (int i = 0; i < res_min.Length; i++, _ar++, _c++, _res++)
                        *_res = scatter * (1.0f - *_c) + max_all - (max_all - *_ar) * (max_all - limitMin) / (max_all - min);

                    // find index
                    int[] id = array_temp_min
                        .AsParallel()
                        .AsOrdered()
                        .Select(t => t.Key)
                        .ToArray();

                    // save data
                    Parallel.For(0, id.Length, i => analysis.TableData[id[i] - 1] =
                            new KeyValuePairTable<int, DateTime, float, bool>(id[i],
                            array_temp_min[i].DateTime, res_min[i], false));
                }
            }

            // change header
            analysis.ChangeHeader();

            // create random data array
            // lenght - length of array
            // type - type of series
            float[] CreateRDA(int lenght, TypeOfRandom type)
            {
                float[] res = new float[lenght];
                double[] array = new double[lenght];
                var rnd = new Random();

                switch (type)
                {
                    case TypeOfRandom.dataScale:
                        Parallel.For(0, lenght, i => array[i] = 1.0);
                        goto default;
                    case TypeOfRandom.dataRandomB:
                         Parallel.For(0, lenght, i => array[i] = rnd.NextDouble());
                        goto default;
                    case TypeOfRandom.dataSRandomS:
                        array = SystemRandomSource.Doubles(lenght, rnd.Next(lenght));
                        goto default;
                    case TypeOfRandom.dataCRandomS:
                        array = CryptoRandomSource.Doubles(lenght);
                        goto default;
                    case TypeOfRandom.dataMTwister:
                        array = MersenneTwister.Doubles(lenght, rnd.Next(lenght));
                        goto default;
                    case TypeOfRandom.dataXorshift:
                        array = Xorshift.Doubles(lenght, rnd.Next(lenght));
                        goto default;
                    case TypeOfRandom.dataXoshiro256SS:
                        array = Xoshiro256StarStar.Doubles(lenght, rnd.Next(lenght));
                        goto default;
                    case TypeOfRandom.dataMcg31m1:
                        array = Mcg31m1.Doubles(lenght, rnd.Next(lenght));
                        goto default;
                    case TypeOfRandom.dataMcg59:
                        array = Mcg59.Doubles(lenght, rnd.Next(lenght));
                        goto default;
                    case TypeOfRandom.dataWH1982:
                        array = WH1982.Doubles(lenght, rnd.Next(lenght));
                        goto default;
                    case TypeOfRandom.dataWH2006:
                        array = WH2006.Doubles(lenght, rnd.Next(lenght));
                        goto default;
                    case TypeOfRandom.dataMrg32k3a:
                        array = Mrg32k3a.Doubles(lenght, rnd.Next(lenght));
                        goto default;
                    case TypeOfRandom.dataPalf:
                        array = Palf.Doubles(lenght, rnd.Next(lenght));
                        goto default;
                    default:
                        Parallel.For(0, lenght, i => res[i] = (float)array[i]);
                        break;
                }

                return res;
            }

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
