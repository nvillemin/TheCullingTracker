namespace TheCullingTracker {
	partial class FormMain {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if(disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.LB_Status = new System.Windows.Forms.Label();
			this.DGV_Game = new System.Windows.Forms.DataGridView();
			this.PlayerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.DmgTo = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.DmgFrom = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Games = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Kills = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.MS_Main = new System.Windows.Forms.MenuStrip();
			this.loadDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.DGV_Game)).BeginInit();
			this.MS_Main.SuspendLayout();
			this.SuspendLayout();
			// 
			// LB_Status
			// 
			this.LB_Status.Dock = System.Windows.Forms.DockStyle.Top;
			this.LB_Status.Font = new System.Drawing.Font("Trebuchet MS", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.LB_Status.ForeColor = System.Drawing.Color.DimGray;
			this.LB_Status.Location = new System.Drawing.Point(0, 24);
			this.LB_Status.Name = "LB_Status";
			this.LB_Status.Size = new System.Drawing.Size(244, 35);
			this.LB_Status.TabIndex = 0;
			this.LB_Status.Text = "STARTING...";
			this.LB_Status.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// DGV_Game
			// 
			this.DGV_Game.AllowUserToAddRows = false;
			this.DGV_Game.AllowUserToDeleteRows = false;
			this.DGV_Game.AllowUserToResizeColumns = false;
			this.DGV_Game.AllowUserToResizeRows = false;
			this.DGV_Game.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.DGV_Game.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
			this.DGV_Game.BackgroundColor = System.Drawing.SystemColors.Control;
			this.DGV_Game.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.DGV_Game.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			this.DGV_Game.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.DGV_Game.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PlayerName,
            this.DmgTo,
            this.DmgFrom,
            this.Games,
            this.Kills});
			this.DGV_Game.Location = new System.Drawing.Point(6, 62);
			this.DGV_Game.MultiSelect = false;
			this.DGV_Game.Name = "DGV_Game";
			this.DGV_Game.ReadOnly = true;
			this.DGV_Game.RowHeadersVisible = false;
			this.DGV_Game.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
			this.DGV_Game.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.White;
			this.DGV_Game.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
			this.DGV_Game.RowTemplate.ReadOnly = true;
			this.DGV_Game.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.DGV_Game.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this.DGV_Game.Size = new System.Drawing.Size(442, 342);
			this.DGV_Game.TabIndex = 2;
			// 
			// PlayerName
			// 
			this.PlayerName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.PlayerName.HeaderText = "Player";
			this.PlayerName.MinimumWidth = 10;
			this.PlayerName.Name = "PlayerName";
			this.PlayerName.ReadOnly = true;
			this.PlayerName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.PlayerName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.PlayerName.Width = 42;
			// 
			// DmgTo
			// 
			this.DmgTo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.DmgTo.HeaderText = "Dmg To";
			this.DmgTo.Name = "DmgTo";
			this.DmgTo.ReadOnly = true;
			this.DmgTo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.DmgTo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.DmgTo.Width = 51;
			// 
			// DmgFrom
			// 
			this.DmgFrom.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.DmgFrom.HeaderText = "Dmg From";
			this.DmgFrom.Name = "DmgFrom";
			this.DmgFrom.ReadOnly = true;
			this.DmgFrom.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.DmgFrom.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.DmgFrom.Width = 61;
			// 
			// Games
			// 
			this.Games.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.Games.HeaderText = "Games";
			this.Games.Name = "Games";
			this.Games.ReadOnly = true;
			this.Games.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.Games.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.Games.Width = 46;
			// 
			// Kills
			// 
			this.Kills.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.Kills.HeaderText = "Kills";
			this.Kills.Name = "Kills";
			this.Kills.ReadOnly = true;
			this.Kills.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.Kills.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.Kills.Width = 31;
			// 
			// MS_Main
			// 
			this.MS_Main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadDataToolStripMenuItem,
            this.optionsToolStripMenuItem});
			this.MS_Main.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
			this.MS_Main.Location = new System.Drawing.Point(0, 0);
			this.MS_Main.Name = "MS_Main";
			this.MS_Main.Size = new System.Drawing.Size(244, 24);
			this.MS_Main.TabIndex = 3;
			this.MS_Main.Text = "Menu";
			// 
			// loadDataToolStripMenuItem
			// 
			this.loadDataToolStripMenuItem.Name = "loadDataToolStripMenuItem";
			this.loadDataToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
			this.loadDataToolStripMenuItem.Text = "Load data";
			this.loadDataToolStripMenuItem.Click += new System.EventHandler(this.loadDataToolStripMenuItem_Click);
			// 
			// optionsToolStripMenuItem
			// 
			this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
			this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
			this.optionsToolStripMenuItem.Text = "Options";
			this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
			// 
			// FormMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(244, 386);
			this.Controls.Add(this.DGV_Game);
			this.Controls.Add(this.LB_Status);
			this.Controls.Add(this.MS_Main);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.MainMenuStrip = this.MS_Main;
			this.MaximizeBox = false;
			this.Name = "FormMain";
			this.ShowIcon = false;
			this.Text = "The Culling Tracker";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
			((System.ComponentModel.ISupportInitialize)(this.DGV_Game)).EndInit();
			this.MS_Main.ResumeLayout(false);
			this.MS_Main.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.DataGridView DGV_Game;
		private System.Windows.Forms.Label LB_Status;
		private System.Windows.Forms.DataGridViewTextBoxColumn PlayerName;
		private System.Windows.Forms.DataGridViewTextBoxColumn DmgTo;
		private System.Windows.Forms.DataGridViewTextBoxColumn DmgFrom;
		private System.Windows.Forms.DataGridViewTextBoxColumn Games;
		private System.Windows.Forms.DataGridViewTextBoxColumn Kills;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
		private System.Windows.Forms.MenuStrip MS_Main;
		private System.Windows.Forms.ToolStripMenuItem loadDataToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
	}
}

