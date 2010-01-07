// Copyright © Julian Brunner 2009

// This file is part of Stream Visualizer (streamvis).
// 
// Stream Visualizer is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Stream Visualizer is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with Stream Visualizer.  If not, see <http://www.gnu.org/licenses/>.

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
				drawer.Dispose();

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
			this.mainContainer = new System.Windows.Forms.ToolStripContainer();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.coordinateStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.propertiesContainer = new System.Windows.Forms.SplitContainer();
			this.streamsListContainer = new System.Windows.Forms.SplitContainer();
			this.streamsList = new System.Windows.Forms.ListView();
			this.nameColumnHeader = new System.Windows.Forms.ColumnHeader();
			this.streamsContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.changeNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.changeColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.viewport = new Graphics.Viewport();
			this.properties = new System.Windows.Forms.PropertyGrid();
			this.mainMenu = new System.Windows.Forms.MenuStrip();
			this.captureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.clearDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.resetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exportCaptureFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.colorDialog = new System.Windows.Forms.ColorDialog();
			this.openCaptureFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.saveCaptureFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.mainContainer.BottomToolStripPanel.SuspendLayout();
			this.mainContainer.ContentPanel.SuspendLayout();
			this.mainContainer.TopToolStripPanel.SuspendLayout();
			this.mainContainer.SuspendLayout();
			this.statusStrip.SuspendLayout();
			this.propertiesContainer.Panel1.SuspendLayout();
			this.propertiesContainer.Panel2.SuspendLayout();
			this.propertiesContainer.SuspendLayout();
			this.streamsListContainer.Panel1.SuspendLayout();
			this.streamsListContainer.Panel2.SuspendLayout();
			this.streamsListContainer.SuspendLayout();
			this.streamsContextMenuStrip.SuspendLayout();
			this.mainMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainContainer
			// 
			// 
			// mainContainer.BottomToolStripPanel
			// 
			this.mainContainer.BottomToolStripPanel.Controls.Add(this.statusStrip);
			// 
			// mainContainer.ContentPanel
			// 
			this.mainContainer.ContentPanel.Controls.Add(this.propertiesContainer);
			this.mainContainer.ContentPanel.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
			this.mainContainer.ContentPanel.Size = new System.Drawing.Size(1073, 608);
			this.mainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mainContainer.Location = new System.Drawing.Point(0, 0);
			this.mainContainer.Name = "mainContainer";
			this.mainContainer.Size = new System.Drawing.Size(1073, 654);
			this.mainContainer.TabIndex = 0;
			this.mainContainer.Text = "toolStripContainer1";
			// 
			// mainContainer.TopToolStripPanel
			// 
			this.mainContainer.TopToolStripPanel.Controls.Add(this.mainMenu);
			// 
			// statusStrip
			// 
			this.statusStrip.Dock = System.Windows.Forms.DockStyle.None;
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel,
            this.coordinateStatusLabel});
			this.statusStrip.Location = new System.Drawing.Point(0, 0);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
			this.statusStrip.Size = new System.Drawing.Size(1073, 22);
			this.statusStrip.TabIndex = 0;
			// 
			// statusLabel
			// 
			this.statusLabel.Name = "statusLabel";
			this.statusLabel.Size = new System.Drawing.Size(1058, 17);
			this.statusLabel.Spring = true;
			this.statusLabel.Text = "Ready";
			this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// coordinateStatusLabel
			// 
			this.coordinateStatusLabel.Name = "coordinateStatusLabel";
			this.coordinateStatusLabel.Size = new System.Drawing.Size(0, 17);
			this.coordinateStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.coordinateStatusLabel.Visible = false;
			// 
			// propertiesContainer
			// 
			this.propertiesContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.propertiesContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.propertiesContainer.Location = new System.Drawing.Point(0, 0);
			this.propertiesContainer.Name = "propertiesContainer";
			// 
			// propertiesContainer.Panel1
			// 
			this.propertiesContainer.Panel1.Controls.Add(this.streamsListContainer);
			// 
			// propertiesContainer.Panel2
			// 
			this.propertiesContainer.Panel2.Controls.Add(this.properties);
			this.propertiesContainer.Size = new System.Drawing.Size(1073, 608);
			this.propertiesContainer.SplitterDistance = 816;
			this.propertiesContainer.TabIndex = 1;
			// 
			// streamsListContainer
			// 
			this.streamsListContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.streamsListContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.streamsListContainer.Location = new System.Drawing.Point(0, 0);
			this.streamsListContainer.Name = "streamsListContainer";
			// 
			// streamsListContainer.Panel1
			// 
			this.streamsListContainer.Panel1.Controls.Add(this.streamsList);
			// 
			// streamsListContainer.Panel2
			// 
			this.streamsListContainer.Panel2.Controls.Add(this.viewport);
			this.streamsListContainer.Size = new System.Drawing.Size(816, 608);
			this.streamsListContainer.SplitterDistance = 245;
			this.streamsListContainer.TabIndex = 0;
			// 
			// streamsList
			// 
			this.streamsList.CheckBoxes = true;
			this.streamsList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameColumnHeader});
			this.streamsList.ContextMenuStrip = this.streamsContextMenuStrip;
			this.streamsList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.streamsList.FullRowSelect = true;
			this.streamsList.Location = new System.Drawing.Point(0, 0);
			this.streamsList.Name = "streamsList";
			this.streamsList.Size = new System.Drawing.Size(245, 608);
			this.streamsList.TabIndex = 1;
			this.streamsList.UseCompatibleStateImageBehavior = false;
			this.streamsList.View = System.Windows.Forms.View.Details;
			this.streamsList.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.streamsList_ItemChecked);
			// 
			// nameColumnHeader
			// 
			this.nameColumnHeader.Text = "Name";
			this.nameColumnHeader.Width = 214;
			// 
			// streamsContextMenuStrip
			// 
			this.streamsContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeNameToolStripMenuItem,
            this.changeColorToolStripMenuItem});
			this.streamsContextMenuStrip.Name = "streamsContextMenuStrip";
			this.streamsContextMenuStrip.Size = new System.Drawing.Size(151, 48);
			// 
			// changeNameToolStripMenuItem
			// 
			this.changeNameToolStripMenuItem.Name = "changeNameToolStripMenuItem";
			this.changeNameToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
			this.changeNameToolStripMenuItem.Text = "Change &Name";
			this.changeNameToolStripMenuItem.Click += new System.EventHandler(this.changeNameToolStripMenuItem_Click);
			// 
			// changeColorToolStripMenuItem
			// 
			this.changeColorToolStripMenuItem.Name = "changeColorToolStripMenuItem";
			this.changeColorToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
			this.changeColorToolStripMenuItem.Text = "Change &Color";
			this.changeColorToolStripMenuItem.Click += new System.EventHandler(this.changeColorToolStripMenuItem_Click);
			// 
			// viewport
			// 
			this.viewport.BackColor = System.Drawing.Color.Black;
			this.viewport.Dock = System.Windows.Forms.DockStyle.Fill;
			this.viewport.Location = new System.Drawing.Point(0, 0);
			this.viewport.Name = "viewport";
			this.viewport.Size = new System.Drawing.Size(567, 608);
			this.viewport.TabIndex = 0;
			this.viewport.VSync = true;
			this.viewport.DoubleClick += new System.EventHandler(this.viewport_DoubleClick);
			this.viewport.Layout += new System.Windows.Forms.LayoutEventHandler(this.viewport_Layout);
			// 
			// properties
			// 
			this.properties.Dock = System.Windows.Forms.DockStyle.Fill;
			this.properties.Location = new System.Drawing.Point(0, 0);
			this.properties.Name = "properties";
			this.properties.PropertySort = System.Windows.Forms.PropertySort.NoSort;
			this.properties.Size = new System.Drawing.Size(253, 608);
			this.properties.TabIndex = 0;
			this.properties.ToolbarVisible = false;
			// 
			// mainMenu
			// 
			this.mainMenu.Dock = System.Windows.Forms.DockStyle.None;
			this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.captureToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.helpToolStripMenuItem});
			this.mainMenu.Location = new System.Drawing.Point(0, 0);
			this.mainMenu.Name = "mainMenu";
			this.mainMenu.Size = new System.Drawing.Size(1073, 24);
			this.mainMenu.TabIndex = 0;
			this.mainMenu.Text = "menuStrip1";
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
			// viewToolStripMenuItem
			// 
			this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetToolStripMenuItem});
			this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
			this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.viewToolStripMenuItem.Text = "&View";
			// 
			// resetToolStripMenuItem
			// 
			this.resetToolStripMenuItem.Name = "resetToolStripMenuItem";
			this.resetToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
			this.resetToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
			this.resetToolStripMenuItem.Text = "&Reset";
			this.resetToolStripMenuItem.Click += new System.EventHandler(this.resetToolStripMenuItem_Click);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.helpToolStripMenuItem.Text = "&Help";
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
			this.aboutToolStripMenuItem.Text = "&About...";
			this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
			// 
			// exportCaptureFileDialog
			// 
			this.exportCaptureFileDialog.DefaultExt = "stream";
			this.exportCaptureFileDialog.Filter = "Text Stream|*.stream|All Files|*.*";
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
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1073, 654);
			this.Controls.Add(this.mainContainer);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.mainMenu;
			this.Name = "MainWindow";
			this.Text = "<Title>";
			this.Load += new System.EventHandler(this.MainWindow_Load);
			this.mainContainer.BottomToolStripPanel.ResumeLayout(false);
			this.mainContainer.BottomToolStripPanel.PerformLayout();
			this.mainContainer.ContentPanel.ResumeLayout(false);
			this.mainContainer.TopToolStripPanel.ResumeLayout(false);
			this.mainContainer.TopToolStripPanel.PerformLayout();
			this.mainContainer.ResumeLayout(false);
			this.mainContainer.PerformLayout();
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			this.propertiesContainer.Panel1.ResumeLayout(false);
			this.propertiesContainer.Panel2.ResumeLayout(false);
			this.propertiesContainer.ResumeLayout(false);
			this.streamsListContainer.Panel1.ResumeLayout(false);
			this.streamsListContainer.Panel2.ResumeLayout(false);
			this.streamsListContainer.ResumeLayout(false);
			this.streamsContextMenuStrip.ResumeLayout(false);
			this.mainMenu.ResumeLayout(false);
			this.mainMenu.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ToolStripContainer mainContainer;
		private System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.ToolStripStatusLabel statusLabel;
		private System.Windows.Forms.MenuStrip mainMenu;
		private System.Windows.Forms.SplitContainer streamsListContainer;
		private System.Windows.Forms.ListView streamsList;
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
		private System.Windows.Forms.SaveFileDialog exportCaptureFileDialog;
		private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
		private System.Windows.Forms.ToolStripStatusLabel coordinateStatusLabel;
		private System.Windows.Forms.SplitContainer propertiesContainer;
		private System.Windows.Forms.PropertyGrid properties;
		private System.Windows.Forms.ToolStripMenuItem changeNameToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem resetToolStripMenuItem;
		
	}
}

