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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menu = new System.Windows.Forms.MenuStrip();
            this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.openMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.saveMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.analyseMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenu = new System.Windows.Forms.ToolStripMenuItem();
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
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.menu.SuspendLayout();
            this.statusM.SuspendLayout();
            this.tabC.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu,
            this.analyseMenu,
            this.helpMenu});
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
            // analyseMenu
            // 
            this.analyseMenu.Image = global::Specialized_PDF_Editor.Properties.Resources.AnalyzeTrace_16x;
            this.analyseMenu.Name = "analyseMenu";
            this.analyseMenu.Size = new System.Drawing.Size(76, 20);
            this.analyseMenu.Text = "Analyse";
            this.analyseMenu.Click += new System.EventHandler(this.AnalyseMenu_Click);
            // 
            // helpMenu
            // 
            this.helpMenu.Image = global::Specialized_PDF_Editor.Properties.Resources.FSHelpApplication_16x;
            this.helpMenu.Name = "helpMenu";
            this.helpMenu.Size = new System.Drawing.Size(60, 20);
            this.helpMenu.Text = "Help";
            this.helpMenu.Click += new System.EventHandler(this.HelpMenu_Click);
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
            this.pdfViewerL.ShowToolbar = true;
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
            this.pdfViewerC.ShowToolbar = true;
            this.pdfViewerC.Size = new System.Drawing.Size(786, 372);
            this.pdfViewerC.TabIndex = 0;
            this.pdfViewerC.ZoomMode = PdfiumViewer.PdfViewerZoomMode.FitBest;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(792, 378);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Correction";
            this.tabPage3.ToolTipText = "Page for make changes";
            this.tabPage3.UseVisualStyleBackColor = true;
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
    }
}

