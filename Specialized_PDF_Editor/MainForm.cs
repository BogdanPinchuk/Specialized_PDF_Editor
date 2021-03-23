using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Text;
using System.Windows.Forms;

namespace Specialized_PDF_Editor
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Path for base-file
        /// </summary>
        internal string BPath
        {
            get { return Visual.BPath; }
            set { Visual.BPath = value; }
        }
        /// <summary>
        /// Path for temp-file
        /// </summary>
        internal string TPath
        {
            get { return Visual.TPath; }
            set { Visual.TPath = value; }
        }

        internal Analysis analysis;

        internal MainForm(string[] pathes)
        {
            InitializeComponent();
            ConnectFormComponent();

            if (pathes.Length > 0 && !string.IsNullOrEmpty(pathes[0]))
                Visual.LoadPdfToMemory(pathes[0], pdfViewerL);
        }

        /// <summary>
        /// Connecting this form`s element with spetial class
        /// </summary>
        private void ConnectFormComponent()
        {
            Visual.NameOfDoc = nameOfDoc;
            Visual.PdfViewerL = pdfViewerL;
            Visual.PdfViewerC = pdfViewerC;
            Visual.Status = status;

            Visual.HeaderInfo = headerInfo;
            Visual.MetaDataInfo = metaData;

            Visual.MainDataTable = tableMainData;
            Visual.OyDataTable = tableOyData;
            Visual.OxDataTable = tableOxData;

            Visual.Chart = mainChart;

            Visual.TempColor = bTempColor;
            Visual.TopColor = bTopColor;
            Visual.BottomColor = bBottomColor;
            Visual.FontText = bFont;
        }

        /// <summary>
        /// Unload data from memory
        /// </summary>
        private void UnLoad()
        {
            Visual.Dispose();
            Dispose();

            Environment.Exit(0);
        }

        private void OpenMenu_Click(object sender, EventArgs e)
        {
            if (openFD.ShowDialog() == DialogResult.OK)
                Visual.LoadPdfToMemory(openFD.FileName, pdfViewerL);
        }

        private void ExitMenu_Click(object sender, EventArgs e)
            => UnLoad();

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
            => UnLoad();

        private void AnalyseMenu_Click(object sender, EventArgs e)
        {
            Visual.CreateRAMData();
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            string path = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
            Visual.LoadPdfToMemory(path, pdfViewerL);
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

        private void ToolAnalysisFile_Click(object sender, EventArgs e)
        {
            // check file availability
            if (Visual.StreamL == null)
            {
                status.Text = "The file is invalid or non-available";
                return;
            }

            Visual.HeaderInfo.Clear();
            analysis = new Analysis(Visual.StreamL);
            analysis.ExtractMetaData();

            // check corected pdf-file
            if (!analysis.FileValidation())
            {
                status.Text = "The file is invalid or non-available";
                Visual.ClearChart();
                analysis = null;
                return;
            }

            analysis.ParsingFile();

            Visual.MetaDataInfo.Text = analysis.Metadata.ToString();
            Visual.HeaderInfo.Text = analysis.HeadInfo.ToString();

            Visual.ShowMainDataTable(analysis.TableData);
            Visual.ShowOyDataTable(analysis.DataOyAxis);
            Visual.ShowOxDataTable(analysis.DataOxAxis);

            // update area of chart
            Refresh();
        }

        /// <summary>
        /// Occurs when cell ending changing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TableMainData_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Visual.DoingChanges(e.RowIndex, e.ColumnIndex, analysis);
            Visual.HeaderInfo.Text = analysis.HeadInfo.ToString();

            // update area of chart
            Refresh();
        }

        private void MainChart_Paint(object sender, PaintEventArgs e)
        {
            if (analysis != null)
                Visual.ShowChart(analysis, e.Graphics, e.ClipRectangle);
        }

        private void BTempColor_Click(object sender, EventArgs e)
        {
            if (colorChouse.ShowDialog() == DialogResult.OK)
                bTempColor.BackColor = colorChouse.Color;
        }

        private void BTopColor_Click(object sender, EventArgs e)
        {
            if (colorChouse.ShowDialog() == DialogResult.OK)
                bTopColor.BackColor = colorChouse.Color;
        }

        private void BBottomColor_Click(object sender, EventArgs e)
        {
            if (colorChouse.ShowDialog() == DialogResult.OK)
                bBottomColor.BackColor = colorChouse.Color;
        }

        private void BFont_Click(object sender, EventArgs e)
        {
            if (fontChouse.ShowDialog() == DialogResult.OK)
            {
                bFont.Text = fontChouse.Font.Name;
                bFont.Font = new Font(
                    new Font(fontChouse.Font, FontStyle.Regular).FontFamily, 14f);
            }
        }

        private void DataScale_Click(object sender, EventArgs e)
        {
            if (analysis != null)
                return;
        }
    }
}
