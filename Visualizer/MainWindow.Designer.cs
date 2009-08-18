namespace Visualizer
{
	partial class MainWindow
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
				DisposeSource();

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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
			this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.streamsListView = new System.Windows.Forms.ListView();
			this.nameColumnHeader = new System.Windows.Forms.ColumnHeader();
			this.streamsContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.changeColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.viewport = new Graphics.Viewport();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.captureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.clearDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.plotterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.freezeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.graphExtensionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.showStreamListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.minimalModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.hideAllGraphsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.gCCollectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.colorDialog = new System.Windows.Forms.ColorDialog();
			this.openCaptureFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.saveCaptureFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.exportCaptureFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
			this.toolStripContainer1.ContentPanel.SuspendLayout();
			this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
			this.toolStripContainer1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.streamsContextMenuStrip.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStripContainer1
			// 
			// 
			// toolStripContainer1.BottomToolStripPanel
			// 
			this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.statusStrip1);
			// 
			// toolStripContainer1.ContentPanel
			// 
			this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainer1);
			this.toolStripContainer1.ContentPanel.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
			this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(737, 512);
			this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
			this.toolStripContainer1.Name = "toolStripContainer1";
			this.toolStripContainer1.Size = new System.Drawing.Size(737, 558);
			this.toolStripContainer1.TabIndex = 0;
			this.toolStripContainer1.Text = "toolStripContainer1";
			// 
			// toolStripContainer1.TopToolStripPanel
			// 
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStrip1);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
			this.statusStrip1.Location = new System.Drawing.Point(0, 0);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
			this.statusStrip1.Size = new System.Drawing.Size(737, 22);
			this.statusStrip1.TabIndex = 0;
			// 
			// statusLabel
			// 
			this.statusLabel.Name = "statusLabel";
			this.statusLabel.Size = new System.Drawing.Size(39, 17);
			this.statusLabel.Text = "Ready";
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.streamsListView);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.viewport);
			this.splitContainer1.Size = new System.Drawing.Size(737, 512);
			this.splitContainer1.SplitterDistance = 245;
			this.splitContainer1.TabIndex = 0;
			// 
			// streamsListView
			// 
			this.streamsListView.CheckBoxes = true;
			this.streamsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameColumnHeader});
			this.streamsListView.ContextMenuStrip = this.streamsContextMenuStrip;
			this.streamsListView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.streamsListView.FullRowSelect = true;
			this.streamsListView.Location = new System.Drawing.Point(0, 0);
			this.streamsListView.Name = "streamsListView";
			this.streamsListView.Size = new System.Drawing.Size(245, 512);
			this.streamsListView.TabIndex = 1;
			this.streamsListView.UseCompatibleStateImageBehavior = false;
			this.streamsListView.View = System.Windows.Forms.View.Details;
			this.streamsListView.ItemActivate += new System.EventHandler(this.changeColorToolStripMenuItem_Click);
			this.streamsListView.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.streamsListView_ItemChecked);
			// 
			// nameColumnHeader
			// 
			this.nameColumnHeader.Text = "Name";
			this.nameColumnHeader.Width = 214;
			// 
			// streamsContextMenuStrip
			// 
			this.streamsContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeColorToolStripMenuItem});
			this.streamsContextMenuStrip.Name = "streamsContextMenuStrip";
			this.streamsContextMenuStrip.Size = new System.Drawing.Size(148, 26);
			// 
			// changeColorToolStripMenuItem
			// 
			this.changeColorToolStripMenuItem.Name = "changeColorToolStripMenuItem";
			this.changeColorToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
			this.changeColorToolStripMenuItem.Text = "&Change Color";
			this.changeColorToolStripMenuItem.Click += new System.EventHandler(this.changeColorToolStripMenuItem_Click);
			// 
			// viewport
			// 
			this.viewport.BackColor = System.Drawing.Color.Black;
			this.viewport.ClearColor = System.Drawing.Color.Black;
			this.viewport.Dock = System.Windows.Forms.DockStyle.Fill;
			this.viewport.Location = new System.Drawing.Point(0, 0);
			this.viewport.Name = "viewport";
			this.viewport.Size = new System.Drawing.Size(488, 512);
			this.viewport.TabIndex = 0;
			this.viewport.VSync = true;
			// 
			// menuStrip1
			// 
			this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.captureToolStripMenuItem,
            this.plotterToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.gCCollectToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(737, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// captureToolStripMenuItem
			// 
			this.captureToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.exportToolStripMenuItem,
            this.toolStripSeparator1,
            this.clearDataToolStripMenuItem});
			this.captureToolStripMenuItem.Name = "captureToolStripMenuItem";
			this.captureToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
			this.captureToolStripMenuItem.Text = "&Capture";
			// 
			// newToolStripMenuItem
			// 
			this.newToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripMenuItem.Image")));
			this.newToolStripMenuItem.Name = "newToolStripMenuItem";
			this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.newToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
			this.newToolStripMenuItem.Text = "&New...";
			this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
			// 
			// openToolStripMenuItem
			// 
			this.openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.Image")));
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.openToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
			this.openToolStripMenuItem.Text = "&Open...";
			this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
			// 
			// saveToolStripMenuItem
			// 
			this.saveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem.Image")));
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.saveToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
			this.saveToolStripMenuItem.Text = "&Save";
			this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
			// 
			// saveAsToolStripMenuItem
			// 
			this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
			this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
			this.saveAsToolStripMenuItem.Text = "Save &As...";
			this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
			// 
			// exportToolStripMenuItem
			// 
			this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
			this.exportToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
			this.exportToolStripMenuItem.Text = "&Export...";
			this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(152, 6);
			// 
			// clearDataToolStripMenuItem
			// 
			this.clearDataToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("clearDataToolStripMenuItem.Image")));
			this.clearDataToolStripMenuItem.Name = "clearDataToolStripMenuItem";
			this.clearDataToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
			this.clearDataToolStripMenuItem.Text = "&Clear Data";
			this.clearDataToolStripMenuItem.Click += new System.EventHandler(this.clearDataToolStripMenuItem_Click);
			// 
			// plotterToolStripMenuItem
			// 
			this.plotterToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.freezeToolStripMenuItem,
            this.graphExtensionToolStripMenuItem});
			this.plotterToolStripMenuItem.Name = "plotterToolStripMenuItem";
			this.plotterToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
			this.plotterToolStripMenuItem.Text = "&Plotter";
			// 
			// freezeToolStripMenuItem
			// 
			this.freezeToolStripMenuItem.CheckOnClick = true;
			this.freezeToolStripMenuItem.Name = "freezeToolStripMenuItem";
			this.freezeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
			this.freezeToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
			this.freezeToolStripMenuItem.Text = "&Freeze";
			this.freezeToolStripMenuItem.Click += new System.EventHandler(this.freezeToolStripMenuItem_Click);
			// 
			// graphExtensionToolStripMenuItem
			// 
			this.graphExtensionToolStripMenuItem.CheckOnClick = true;
			this.graphExtensionToolStripMenuItem.Name = "graphExtensionToolStripMenuItem";
			this.graphExtensionToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
			this.graphExtensionToolStripMenuItem.Text = "Graph &Extension";
			this.graphExtensionToolStripMenuItem.Click += new System.EventHandler(this.graphExtensionToolStripMenuItem_Click);
			// 
			// viewToolStripMenuItem
			// 
			this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showStreamListToolStripMenuItem,
            this.minimalModeToolStripMenuItem,
            this.toolStripSeparator2,
            this.hideAllGraphsToolStripMenuItem});
			this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
			this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.viewToolStripMenuItem.Text = "&View";
			// 
			// showStreamListToolStripMenuItem
			// 
			this.showStreamListToolStripMenuItem.CheckOnClick = true;
			this.showStreamListToolStripMenuItem.Name = "showStreamListToolStripMenuItem";
			this.showStreamListToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
			this.showStreamListToolStripMenuItem.Text = "Show &Stream List";
			this.showStreamListToolStripMenuItem.Click += new System.EventHandler(this.showStreamListToolStripMenuItem_Click);
			// 
			// minimalModeToolStripMenuItem
			// 
			this.minimalModeToolStripMenuItem.CheckOnClick = true;
			this.minimalModeToolStripMenuItem.Name = "minimalModeToolStripMenuItem";
			this.minimalModeToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
			this.minimalModeToolStripMenuItem.Text = "&Minimal Mode";
			this.minimalModeToolStripMenuItem.Click += new System.EventHandler(this.minimalModeToolStripMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(161, 6);
			// 
			// hideAllGraphsToolStripMenuItem
			// 
			this.hideAllGraphsToolStripMenuItem.Name = "hideAllGraphsToolStripMenuItem";
			this.hideAllGraphsToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
			this.hideAllGraphsToolStripMenuItem.Text = "&Hide all Graphs";
			this.hideAllGraphsToolStripMenuItem.Click += new System.EventHandler(this.hideAllGraphsToolStripMenuItem_Click);
			// 
			// gCCollectToolStripMenuItem
			// 
			this.gCCollectToolStripMenuItem.Name = "gCCollectToolStripMenuItem";
			this.gCCollectToolStripMenuItem.Size = new System.Drawing.Size(75, 20);
			this.gCCollectToolStripMenuItem.Text = "GC.Collect";
			this.gCCollectToolStripMenuItem.Click += new System.EventHandler(this.gCCollectToolStripMenuItem_Click);
			// 
			// colorDialog
			// 
			this.colorDialog.AnyColor = true;
			this.colorDialog.FullOpen = true;
			this.colorDialog.SolidColorOnly = true;
			// 
			// openCaptureFileDialog
			// 
			this.openCaptureFileDialog.DefaultExt = "capture";
			this.openCaptureFileDialog.Filter = "Capture Files|*.capture|All Files|*.*";
			// 
			// saveCaptureFileDialog
			// 
			this.saveCaptureFileDialog.DefaultExt = "capture";
			this.saveCaptureFileDialog.Filter = "Capture Files|*.capture|All Files|*.*";
			// 
			// exportCaptureFileDialog
			// 
			this.exportCaptureFileDialog.DefaultExt = "stream";
			this.exportCaptureFileDialog.Filter = "Text Stream|*.stream|All Files|*.*";
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(737, 558);
			this.Controls.Add(this.toolStripContainer1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "MainWindow";
			this.Text = "<Title>";
			this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
			this.toolStripContainer1.ContentPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.PerformLayout();
			this.toolStripContainer1.ResumeLayout(false);
			this.toolStripContainer1.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.streamsContextMenuStrip.ResumeLayout(false);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ToolStripContainer toolStripContainer1;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel statusLabel;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.ListView streamsListView;
		private System.Windows.Forms.ColumnHeader nameColumnHeader;
		private Graphics.Viewport viewport;
		private System.Windows.Forms.ContextMenuStrip streamsContextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem changeColorToolStripMenuItem;
		private System.Windows.Forms.ColorDialog colorDialog;
		private System.Windows.Forms.ToolStripMenuItem captureToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem clearDataToolStripMenuItem;
		private System.Windows.Forms.OpenFileDialog openCaptureFileDialog;
		private System.Windows.Forms.SaveFileDialog saveCaptureFileDialog;
		private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem showStreamListToolStripMenuItem;
		private System.Windows.Forms.SaveFileDialog exportCaptureFileDialog;
		private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem minimalModeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem plotterToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem freezeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem graphExtensionToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem gCCollectToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem hideAllGraphsToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		
	}
}

