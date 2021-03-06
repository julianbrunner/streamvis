// Copyright © Julian Brunner 2009 - 2011

// This file is part of Stream Visualizer (streamvis).
// 
// Stream Visualizer is free software: you can redistribute it and/or modify it
// under the terms of the GNU General Public License as published by the Free
// Software Foundation, either version 3 of the License, or (at your option) any
// later version.
// 
// Stream Visualizer is distributed in the hope that it will be useful, but
// WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
// FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more
// details.
// 
// You should have received a copy of the GNU General Public License along with
// Stream Visualizer. If not, see <http://www.gnu.org/licenses/>.

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
				drawer.Dispose();
				session.Dispose();

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
			this.pathColumnHeader = new System.Windows.Forms.ColumnHeader();
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
			this.streamsListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.diagramToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.frameCounterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.invertColorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.diagramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.resetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toggleFreezeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
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
            this.nameColumnHeader,
            this.pathColumnHeader});
			this.streamsList.ContextMenuStrip = this.streamsContextMenuStrip;
			this.streamsList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.streamsList.FullRowSelect = true;
			this.streamsList.LabelEdit = true;
			this.streamsList.Location = new System.Drawing.Point(0, 0);
			this.streamsList.Name = "streamsList";
			this.streamsList.Size = new System.Drawing.Size(245, 608);
			this.streamsList.TabIndex = 1;
			this.streamsList.UseCompatibleStateImageBehavior = false;
			this.streamsList.View = System.Windows.Forms.View.Details;
			this.streamsList.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.streamsList_ItemChecked);
			this.streamsList.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.streamsList_AfterLabelEdit);
			// 
			// nameColumnHeader
			// 
			this.nameColumnHeader.Text = "Name";
			this.nameColumnHeader.Width = 159;
			// 
			// pathColumnHeader
			// 
			this.pathColumnHeader.Text = "Path";
			// 
			// streamsContextMenuStrip
			// 
			this.streamsContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeNameToolStripMenuItem,
            this.changeColorToolStripMenuItem});
			this.streamsContextMenuStrip.Name = "streamsContextMenuStrip";
			this.streamsContextMenuStrip.Size = new System.Drawing.Size(151, 48);
			this.streamsContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.streamsContextMenuStrip_Opening);
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
            this.diagramToolStripMenuItem,
            this.settingsToolStripMenuItem,
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
            this.streamsListToolStripMenuItem,
            this.propertiesToolStripMenuItem,
            this.toolStripSeparator2,
            this.diagramToolStripMenuItem1,
            this.frameCounterToolStripMenuItem,
            this.toolStripSeparator3,
            this.invertColorsToolStripMenuItem});
			this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
			this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.viewToolStripMenuItem.Text = "&View";
			// 
			// streamsListToolStripMenuItem
			// 
			this.streamsListToolStripMenuItem.Name = "streamsListToolStripMenuItem";
			this.streamsListToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
			this.streamsListToolStripMenuItem.Text = "&Streams List";
			this.streamsListToolStripMenuItem.Click += new System.EventHandler(this.streamsListToolStripMenuItem_Click);
			// 
			// propertiesToolStripMenuItem
			// 
			this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
			this.propertiesToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
			this.propertiesToolStripMenuItem.Text = "&Properties";
			this.propertiesToolStripMenuItem.Click += new System.EventHandler(this.propertiesToolStripMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(150, 6);
			// 
			// diagramToolStripMenuItem1
			// 
			this.diagramToolStripMenuItem1.Name = "diagramToolStripMenuItem1";
			this.diagramToolStripMenuItem1.Size = new System.Drawing.Size(153, 22);
			this.diagramToolStripMenuItem1.Text = "&Diagram";
			this.diagramToolStripMenuItem1.Click += new System.EventHandler(this.diagramToolStripMenuItem1_Click);
			// 
			// frameCounterToolStripMenuItem
			// 
			this.frameCounterToolStripMenuItem.Name = "frameCounterToolStripMenuItem";
			this.frameCounterToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
			this.frameCounterToolStripMenuItem.Text = "&Frame Counter";
			this.frameCounterToolStripMenuItem.Click += new System.EventHandler(this.frameCounterToolStripMenuItem_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(150, 6);
			// 
			// invertColorsToolStripMenuItem
			// 
			this.invertColorsToolStripMenuItem.Name = "invertColorsToolStripMenuItem";
			this.invertColorsToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
			this.invertColorsToolStripMenuItem.Text = "&Invert Colors";
			this.invertColorsToolStripMenuItem.Click += new System.EventHandler(this.invertColorsToolStripMenuItem_Click);
			// 
			// diagramToolStripMenuItem
			// 
			this.diagramToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetToolStripMenuItem,
            this.toggleFreezeToolStripMenuItem});
			this.diagramToolStripMenuItem.Name = "diagramToolStripMenuItem";
			this.diagramToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
			this.diagramToolStripMenuItem.Text = "&Diagram";
			// 
			// resetToolStripMenuItem
			// 
			this.resetToolStripMenuItem.Name = "resetToolStripMenuItem";
			this.resetToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
			this.resetToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
			this.resetToolStripMenuItem.Text = "&Reset";
			this.resetToolStripMenuItem.Click += new System.EventHandler(this.resetToolStripMenuItem_Click);
			// 
			// toggleFreezeToolStripMenuItem
			// 
			this.toggleFreezeToolStripMenuItem.Name = "toggleFreezeToolStripMenuItem";
			this.toggleFreezeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
			this.toggleFreezeToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
			this.toggleFreezeToolStripMenuItem.Text = "Toggle &Freeze";
			this.toggleFreezeToolStripMenuItem.Click += new System.EventHandler(this.toggleFreezeToolStripMenuItem_Click);
			// 
			// settingsToolStripMenuItem
			// 
			this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem1});
			this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
			this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
			this.settingsToolStripMenuItem.Text = "&Settings";
			// 
			// saveToolStripMenuItem1
			// 
			this.saveToolStripMenuItem1.Name = "saveToolStripMenuItem1";
			this.saveToolStripMenuItem1.Size = new System.Drawing.Size(98, 22);
			this.saveToolStripMenuItem1.Text = "&Save";
			this.saveToolStripMenuItem1.Click += new System.EventHandler(this.saveToolStripMenuItem1_Click);
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
			this.KeyPreview = true;
			this.MainMenuStrip = this.mainMenu;
			this.Name = "MainWindow";
			this.Text = "<Title>";
			this.Load += new System.EventHandler(this.MainWindow_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainWindow_KeyDown);
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
		private System.Windows.Forms.ToolStripMenuItem diagramToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem toggleFreezeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem streamsListToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem propertiesToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem diagramToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem frameCounterToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem resetToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem invertColorsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem1;
		private System.Windows.Forms.ColumnHeader pathColumnHeader;
		
	}
}

