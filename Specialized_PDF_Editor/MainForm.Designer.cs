using System.Windows.Forms;

namespace Specialized_PDF_Editor
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menu = new System.Windows.Forms.MenuStrip();
            this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.openMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.saveMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsAnalysis = new System.Windows.Forms.ToolStripMenuItem();
            this.analysisFile = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.analyseMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.statusM = new System.Windows.Forms.StatusStrip();
            this.status = new System.Windows.Forms.ToolStripStatusLabel();
            this.nameOfDoc = new System.Windows.Forms.ToolStripStatusLabel();
            this.openFD = new System.Windows.Forms.OpenFileDialog();
            this.tabC = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.pdfViewerL = new PdfiumViewer.PdfViewer();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.pdfViewerC = new PdfiumViewer.PdfViewer();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.splitC1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupHeader = new System.Windows.Forms.GroupBox();
            this.headerInfo = new System.Windows.Forms.TextBox();
            this.groupMetaInfo = new System.Windows.Forms.GroupBox();
            this.metaData = new System.Windows.Forms.TextBox();
            this.groupTable = new System.Windows.Forms.GroupBox();
            this.splitC2 = new System.Windows.Forms.SplitContainer();
            this.tabGraph = new System.Windows.Forms.TabControl();
            this.tabChart = new System.Windows.Forms.TabPage();
            this.mainChart = new System.Windows.Forms.PictureBox();
            this.tabParam = new System.Windows.Forms.TabPage();
            this.tabTable = new System.Windows.Forms.TabControl();
            this.tabMainData = new System.Windows.Forms.TabPage();
            this.tableMainData = new System.Windows.Forms.DataGridView();
            this.Key = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OOR = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.tabOy = new System.Windows.Forms.TabPage();
            this.tabOx = new System.Windows.Forms.TabPage();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.tableOyData = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableOxData = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menu.SuspendLayout();
            this.statusM.SuspendLayout();
            this.tabC.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitC1)).BeginInit();
            this.splitC1.Panel1.SuspendLayout();
            this.splitC1.Panel2.SuspendLayout();
            this.splitC1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupHeader.SuspendLayout();
            this.groupMetaInfo.SuspendLayout();
            this.groupTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitC2)).BeginInit();
            this.splitC2.Panel1.SuspendLayout();
            this.splitC2.Panel2.SuspendLayout();
            this.splitC2.SuspendLayout();
            this.tabGraph.SuspendLayout();
            this.tabChart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainChart)).BeginInit();
            this.tabTable.SuspendLayout();
            this.tabMainData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tableMainData)).BeginInit();
            this.tabOy.SuspendLayout();
            this.tabOx.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tableOyData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableOxData)).BeginInit();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu,
            this.toolsAnalysis,
            this.helpMenu,
            this.analyseMenu});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(800, 24);
            this.menu.TabIndex = 0;
            this.menu.Text = "menu";
            // 
            // fileMenu
            // 
            this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openMenu,
            this.saveMenu,
            this.toolStripSeparator1,
            this.exitMenu});
            this.fileMenu.Image = global::Specialized_PDF_Editor.Properties.Resources.LocalFileSites_16x;
            this.fileMenu.Name = "fileMenu";
            this.fileMenu.Size = new System.Drawing.Size(53, 20);
            this.fileMenu.Text = "File";
            // 
            // openMenu
            // 
            this.openMenu.Image = global::Specialized_PDF_Editor.Properties.Resources.OpenFile_16x;
            this.openMenu.Name = "openMenu";
            this.openMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openMenu.Size = new System.Drawing.Size(146, 22);
            this.openMenu.Text = "Open";
            this.openMenu.Click += new System.EventHandler(this.OpenMenu_Click);
            // 
            // saveMenu
            // 
            this.saveMenu.Enabled = false;
            this.saveMenu.Image = global::Specialized_PDF_Editor.Properties.Resources.Save_16x;
            this.saveMenu.Name = "saveMenu";
            this.saveMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveMenu.Size = new System.Drawing.Size(146, 22);
            this.saveMenu.Text = "Save";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(143, 6);
            // 
            // exitMenu
            // 
            this.exitMenu.Image = global::Specialized_PDF_Editor.Properties.Resources.Close_red_16x;
            this.exitMenu.Name = "exitMenu";
            this.exitMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitMenu.Size = new System.Drawing.Size(146, 22);
            this.exitMenu.Text = "Exit";
            this.exitMenu.Click += new System.EventHandler(this.ExitMenu_Click);
            // 
            // toolsAnalysis
            // 
            this.toolsAnalysis.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.analysisFile});
            this.toolsAnalysis.Image = global::Specialized_PDF_Editor.Properties.Resources.SolutionFolderSwitch_16x;
            this.toolsAnalysis.Name = "toolsAnalysis";
            this.toolsAnalysis.Size = new System.Drawing.Size(63, 20);
            this.toolsAnalysis.Text = "Tools";
            // 
            // analysisFile
            // 
            this.analysisFile.Image = global::Specialized_PDF_Editor.Properties.Resources.ValidationSummary_16x;
            this.analysisFile.Name = "analysisFile";
            this.analysisFile.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.analysisFile.Size = new System.Drawing.Size(176, 22);
            this.analysisFile.Text = "Analysis pdf file";
            this.analysisFile.Click += new System.EventHandler(this.ToolAnalysisFile_Click);
            // 
            // helpMenu
            // 
            this.helpMenu.Image = global::Specialized_PDF_Editor.Properties.Resources.FSHelpApplication_16x;
            this.helpMenu.Name = "helpMenu";
            this.helpMenu.Size = new System.Drawing.Size(60, 20);
            this.helpMenu.Text = "Help";
            this.helpMenu.Click += new System.EventHandler(this.HelpMenu_Click);
            // 
            // analyseMenu
            // 
            this.analyseMenu.Image = global::Specialized_PDF_Editor.Properties.Resources.AnalyzeTrace_16x;
            this.analyseMenu.Name = "analyseMenu";
            this.analyseMenu.Size = new System.Drawing.Size(76, 20);
            this.analyseMenu.Text = "Analyse";
            this.analyseMenu.Click += new System.EventHandler(this.AnalyseMenu_Click);
            // 
            // statusM
            // 
            this.statusM.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.status,
            this.nameOfDoc});
            this.statusM.Location = new System.Drawing.Point(0, 428);
            this.statusM.Name = "statusM";
            this.statusM.Size = new System.Drawing.Size(800, 22);
            this.statusM.TabIndex = 1;
            this.statusM.Text = "status";
            // 
            // status
            // 
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(28, 17);
            this.status.Text = "Info";
            // 
            // nameOfDoc
            // 
            this.nameOfDoc.Name = "nameOfDoc";
            this.nameOfDoc.Size = new System.Drawing.Size(757, 17);
            this.nameOfDoc.Spring = true;
            this.nameOfDoc.Text = "Name of document";
            this.nameOfDoc.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // openFD
            // 
            this.openFD.FileName = "file";
            this.openFD.Filter = "PDF files (*.pdf) | *.pdf";
            // 
            // tabC
            // 
            this.tabC.Controls.Add(this.tabPage1);
            this.tabC.Controls.Add(this.tabPage2);
            this.tabC.Controls.Add(this.tabPage3);
            this.tabC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabC.Location = new System.Drawing.Point(0, 24);
            this.tabC.Name = "tabC";
            this.tabC.SelectedIndex = 0;
            this.tabC.Size = new System.Drawing.Size(800, 404);
            this.tabC.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.pdfViewerL);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(792, 378);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Loaded Document";
            this.tabPage1.ToolTipText = "Shows the basic document";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // pdfViewerL
            // 
            this.pdfViewerL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pdfViewerL.Location = new System.Drawing.Point(3, 3);
            this.pdfViewerL.Name = "pdfViewerL";
            this.pdfViewerL.ShowToolbar = false;
            this.pdfViewerL.Size = new System.Drawing.Size(786, 372);
            this.pdfViewerL.TabIndex = 0;
            this.pdfViewerL.ZoomMode = PdfiumViewer.PdfViewerZoomMode.FitBest;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.pdfViewerC);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(792, 378);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Changed Document";
            this.tabPage2.ToolTipText = "Shows the document after changes";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // pdfViewerC
            // 
            this.pdfViewerC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pdfViewerC.Location = new System.Drawing.Point(3, 3);
            this.pdfViewerC.Name = "pdfViewerC";
            this.pdfViewerC.ShowToolbar = false;
            this.pdfViewerC.Size = new System.Drawing.Size(786, 372);
            this.pdfViewerC.TabIndex = 0;
            this.pdfViewerC.ZoomMode = PdfiumViewer.PdfViewerZoomMode.FitBest;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.splitC1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(792, 378);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Correction";
            this.tabPage3.ToolTipText = "Page for make changes";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // splitC1
            // 
            this.splitC1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitC1.Location = new System.Drawing.Point(3, 3);
            this.splitC1.Name = "splitC1";
            this.splitC1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitC1.Panel1
            // 
            this.splitC1.Panel1.Controls.Add(this.splitContainer1);
            // 
            // splitC1.Panel2
            // 
            this.splitC1.Panel2.Controls.Add(this.groupTable);
            this.splitC1.Size = new System.Drawing.Size(786, 372);
            this.splitC1.SplitterDistance = 158;
            this.splitC1.SplitterWidth = 10;
            this.splitC1.TabIndex = 2;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupHeader);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupMetaInfo);
            this.splitContainer1.Size = new System.Drawing.Size(786, 158);
            this.splitContainer1.SplitterDistance = 517;
            this.splitContainer1.SplitterWidth = 10;
            this.splitContainer1.TabIndex = 2;
            // 
            // groupHeader
            // 
            this.groupHeader.Controls.Add(this.headerInfo);
            this.groupHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupHeader.Location = new System.Drawing.Point(0, 0);
            this.groupHeader.Name = "groupHeader";
            this.groupHeader.Size = new System.Drawing.Size(517, 158);
            this.groupHeader.TabIndex = 1;
            this.groupHeader.TabStop = false;
            this.groupHeader.Text = "Header of information";
            // 
            // headerInfo
            // 
            this.headerInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.headerInfo.Location = new System.Drawing.Point(3, 16);
            this.headerInfo.Multiline = true;
            this.headerInfo.Name = "headerInfo";
            this.headerInfo.ReadOnly = true;
            this.headerInfo.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.headerInfo.Size = new System.Drawing.Size(511, 139);
            this.headerInfo.TabIndex = 0;
            // 
            // groupMetaInfo
            // 
            this.groupMetaInfo.Controls.Add(this.metaData);
            this.groupMetaInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupMetaInfo.Location = new System.Drawing.Point(0, 0);
            this.groupMetaInfo.Name = "groupMetaInfo";
            this.groupMetaInfo.Size = new System.Drawing.Size(259, 158);
            this.groupMetaInfo.TabIndex = 2;
            this.groupMetaInfo.TabStop = false;
            this.groupMetaInfo.Text = "Metadata";
            // 
            // metaData
            // 
            this.metaData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metaData.Location = new System.Drawing.Point(3, 16);
            this.metaData.Multiline = true;
            this.metaData.Name = "metaData";
            this.metaData.ReadOnly = true;
            this.metaData.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.metaData.Size = new System.Drawing.Size(253, 139);
            this.metaData.TabIndex = 0;
            // 
            // groupTable
            // 
            this.groupTable.Controls.Add(this.splitC2);
            this.groupTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupTable.Location = new System.Drawing.Point(0, 0);
            this.groupTable.Name = "groupTable";
            this.groupTable.Size = new System.Drawing.Size(786, 204);
            this.groupTable.TabIndex = 0;
            this.groupTable.TabStop = false;
            this.groupTable.Text = "Data of tables";
            // 
            // splitC2
            // 
            this.splitC2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitC2.Location = new System.Drawing.Point(3, 16);
            this.splitC2.Name = "splitC2";
            // 
            // splitC2.Panel1
            // 
            this.splitC2.Panel1.Controls.Add(this.tabGraph);
            // 
            // splitC2.Panel2
            // 
            this.splitC2.Panel2.Controls.Add(this.tabTable);
            this.splitC2.Size = new System.Drawing.Size(780, 185);
            this.splitC2.SplitterDistance = 335;
            this.splitC2.SplitterWidth = 10;
            this.splitC2.TabIndex = 0;
            // 
            // tabGraph
            // 
            this.tabGraph.Controls.Add(this.tabChart);
            this.tabGraph.Controls.Add(this.tabParam);
            this.tabGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabGraph.Location = new System.Drawing.Point(0, 0);
            this.tabGraph.Name = "tabGraph";
            this.tabGraph.SelectedIndex = 0;
            this.tabGraph.Size = new System.Drawing.Size(335, 185);
            this.tabGraph.TabIndex = 0;
            // 
            // tabChart
            // 
            this.tabChart.Controls.Add(this.mainChart);
            this.tabChart.Location = new System.Drawing.Point(4, 22);
            this.tabChart.Name = "tabChart";
            this.tabChart.Padding = new System.Windows.Forms.Padding(3);
            this.tabChart.Size = new System.Drawing.Size(327, 159);
            this.tabChart.TabIndex = 0;
            this.tabChart.Text = "Chart";
            this.tabChart.UseVisualStyleBackColor = true;
            // 
            // mainChart
            // 
            this.mainChart.BackColor = System.Drawing.Color.Silver;
            this.mainChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainChart.Location = new System.Drawing.Point(3, 3);
            this.mainChart.Name = "mainChart";
            this.mainChart.Size = new System.Drawing.Size(321, 153);
            this.mainChart.TabIndex = 0;
            this.mainChart.TabStop = false;
            // 
            // tabParam
            // 
            this.tabParam.Location = new System.Drawing.Point(4, 22);
            this.tabParam.Name = "tabParam";
            this.tabParam.Padding = new System.Windows.Forms.Padding(3);
            this.tabParam.Size = new System.Drawing.Size(327, 159);
            this.tabParam.TabIndex = 1;
            this.tabParam.Text = "Parameters";
            this.tabParam.UseVisualStyleBackColor = true;
            // 
            // tabTable
            // 
            this.tabTable.Controls.Add(this.tabMainData);
            this.tabTable.Controls.Add(this.tabOy);
            this.tabTable.Controls.Add(this.tabOx);
            this.tabTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabTable.Location = new System.Drawing.Point(0, 0);
            this.tabTable.Name = "tabTable";
            this.tabTable.SelectedIndex = 0;
            this.tabTable.Size = new System.Drawing.Size(435, 185);
            this.tabTable.TabIndex = 0;
            // 
            // tabMainData
            // 
            this.tabMainData.Controls.Add(this.tableMainData);
            this.tabMainData.Location = new System.Drawing.Point(4, 22);
            this.tabMainData.Name = "tabMainData";
            this.tabMainData.Padding = new System.Windows.Forms.Padding(3);
            this.tabMainData.Size = new System.Drawing.Size(427, 159);
            this.tabMainData.TabIndex = 0;
            this.tabMainData.Text = "Main data";
            this.tabMainData.UseVisualStyleBackColor = true;
            // 
            // tableMainData
            // 
            this.tableMainData.AllowUserToAddRows = false;
            this.tableMainData.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.tableMainData.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.tableMainData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.tableMainData.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.tableMainData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.tableMainData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableMainData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Key,
            this.Date,
            this.Time,
            this.Value,
            this.OOR});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.tableMainData.DefaultCellStyle = dataGridViewCellStyle7;
            this.tableMainData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableMainData.Location = new System.Drawing.Point(3, 3);
            this.tableMainData.Name = "tableMainData";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.tableMainData.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.tableMainData.RowsDefaultCellStyle = dataGridViewCellStyle9;
            this.tableMainData.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.tableMainData.Size = new System.Drawing.Size(421, 153);
            this.tableMainData.TabIndex = 0;
            // 
            // Key
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Format = "N0";
            dataGridViewCellStyle3.NullValue = null;
            this.Key.DefaultCellStyle = dataGridViewCellStyle3;
            this.Key.HeaderText = "Номер";
            this.Key.Name = "Key";
            // 
            // Date
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Format = "d";
            dataGridViewCellStyle4.NullValue = null;
            this.Date.DefaultCellStyle = dataGridViewCellStyle4;
            this.Date.HeaderText = "Дата";
            this.Date.Name = "Date";
            // 
            // Time
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.Format = "t";
            dataGridViewCellStyle5.NullValue = null;
            this.Time.DefaultCellStyle = dataGridViewCellStyle5;
            this.Time.HeaderText = "Время";
            this.Time.Name = "Time";
            // 
            // Value
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.Format = "N1";
            dataGridViewCellStyle6.NullValue = null;
            this.Value.DefaultCellStyle = dataGridViewCellStyle6;
            this.Value.HeaderText = "Т, °C";
            this.Value.Name = "Value";
            // 
            // OOR
            // 
            this.OOR.HeaderText = "Нар.";
            this.OOR.Name = "OOR";
            this.OOR.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // tabOy
            // 
            this.tabOy.Controls.Add(this.tableOyData);
            this.tabOy.Location = new System.Drawing.Point(4, 22);
            this.tabOy.Name = "tabOy";
            this.tabOy.Padding = new System.Windows.Forms.Padding(3);
            this.tabOy.Size = new System.Drawing.Size(427, 159);
            this.tabOy.TabIndex = 1;
            this.tabOy.Text = "Axis Oy";
            this.tabOy.UseVisualStyleBackColor = true;
            // 
            // tabOx
            // 
            this.tabOx.Controls.Add(this.tableOxData);
            this.tabOx.Location = new System.Drawing.Point(4, 22);
            this.tabOx.Name = "tabOx";
            this.tabOx.Size = new System.Drawing.Size(427, 159);
            this.tabOx.TabIndex = 2;
            this.tabOx.Text = "Axis Ox";
            this.tabOx.UseVisualStyleBackColor = true;
            // 
            // tableOyData
            // 
            this.tableOyData.AllowUserToAddRows = false;
            this.tableOyData.AllowUserToDeleteRows = false;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.tableOyData.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle10;
            this.tableOyData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.tableOyData.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.tableOyData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.tableOyData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableOyData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn4});
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.tableOyData.DefaultCellStyle = dataGridViewCellStyle14;
            this.tableOyData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableOyData.Location = new System.Drawing.Point(3, 3);
            this.tableOyData.Name = "tableOyData";
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.tableOyData.RowHeadersDefaultCellStyle = dataGridViewCellStyle15;
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.tableOyData.RowsDefaultCellStyle = dataGridViewCellStyle16;
            this.tableOyData.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.tableOyData.Size = new System.Drawing.Size(421, 153);
            this.tableOyData.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.Format = "N0";
            dataGridViewCellStyle12.NullValue = null;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle12;
            this.dataGridViewTextBoxColumn1.HeaderText = "Номер";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle13.Format = "N0";
            dataGridViewCellStyle13.NullValue = null;
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle13;
            this.dataGridViewTextBoxColumn4.HeaderText = "Температура, C";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // tableOxData
            // 
            this.tableOxData.AllowUserToAddRows = false;
            this.tableOxData.AllowUserToDeleteRows = false;
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.tableOxData.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle17;
            this.tableOxData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.tableOxData.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle18.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle18.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle18.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.tableOxData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle18;
            this.tableOxData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableOxData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn3});
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle22.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle22.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle22.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle22.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle22.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.tableOxData.DefaultCellStyle = dataGridViewCellStyle22;
            this.tableOxData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableOxData.Location = new System.Drawing.Point(0, 0);
            this.tableOxData.Name = "tableOxData";
            dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle23.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle23.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle23.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle23.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle23.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.tableOxData.RowHeadersDefaultCellStyle = dataGridViewCellStyle23;
            dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.tableOxData.RowsDefaultCellStyle = dataGridViewCellStyle24;
            this.tableOxData.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.tableOxData.Size = new System.Drawing.Size(427, 159);
            this.tableOxData.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle19.Format = "N0";
            dataGridViewCellStyle19.NullValue = null;
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle19;
            this.dataGridViewTextBoxColumn2.HeaderText = "Номер";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle20.Format = "t";
            dataGridViewCellStyle20.NullValue = null;
            this.dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle20;
            this.dataGridViewTextBoxColumn5.HeaderText = "Время";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle21.Format = "d";
            dataGridViewCellStyle21.NullValue = null;
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle21;
            this.dataGridViewTextBoxColumn3.HeaderText = "Дата";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabC);
            this.Controls.Add(this.statusM);
            this.Controls.Add(this.menu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menu;
            this.Name = "MainForm";
            this.Text = "Specialized pdf editor (SPE)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.statusM.ResumeLayout(false);
            this.statusM.PerformLayout();
            this.tabC.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.splitC1.Panel1.ResumeLayout(false);
            this.splitC1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitC1)).EndInit();
            this.splitC1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupHeader.ResumeLayout(false);
            this.groupHeader.PerformLayout();
            this.groupMetaInfo.ResumeLayout(false);
            this.groupMetaInfo.PerformLayout();
            this.groupTable.ResumeLayout(false);
            this.splitC2.Panel1.ResumeLayout(false);
            this.splitC2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitC2)).EndInit();
            this.splitC2.ResumeLayout(false);
            this.tabGraph.ResumeLayout(false);
            this.tabChart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainChart)).EndInit();
            this.tabTable.ResumeLayout(false);
            this.tabMainData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tableMainData)).EndInit();
            this.tabOy.ResumeLayout(false);
            this.tabOx.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tableOyData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableOxData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MenuStrip menu;
        private StatusStrip statusM;
        private OpenFileDialog openFD;
        private ToolStripMenuItem fileMenu;
        private ToolStripMenuItem openMenu;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem exitMenu;
        private ToolStripMenuItem saveMenu;
        private ToolStripMenuItem analyseMenu;
        private ToolStripMenuItem helpMenu;
        private ToolStripStatusLabel status;
        private TabControl tabC;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private PdfiumViewer.PdfViewer pdfViewerL;
        private PdfiumViewer.PdfViewer pdfViewerC;
        private TabPage tabPage3;
        private ToolTip toolTip;
        private ToolStripStatusLabel nameOfDoc;
        private TextBox headerInfo;
        private GroupBox groupHeader;
        private SplitContainer splitC1;
        private GroupBox groupTable;
        private SplitContainer splitC2;
        private SplitContainer splitContainer1;
        private GroupBox groupMetaInfo;
        private TextBox metaData;
        private TabControl tabGraph;
        private TabPage tabChart;
        private TabPage tabParam;
        private TabControl tabTable;
        private TabPage tabMainData;
        private TabPage tabOy;
        private TabPage tabOx;
        private PictureBox mainChart;
        private DataGridView tableMainData;
        private DataGridViewTextBoxColumn Key;
        private DataGridViewTextBoxColumn Date;
        private DataGridViewTextBoxColumn Time;
        private DataGridViewTextBoxColumn Value;
        private DataGridViewCheckBoxColumn OOR;
        private ToolStripMenuItem toolsAnalysis;
        private ToolStripMenuItem analysisFile;
        private DataGridView tableOyData;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridView tableOxData;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
    }
}

