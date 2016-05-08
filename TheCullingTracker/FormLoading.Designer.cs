namespace TheCullingTracker {
	partial class FormLoading {
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
			this.LB_Loading = new System.Windows.Forms.Label();
			this.PB_Loading = new System.Windows.Forms.ProgressBar();
			this.BT_Cancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// LB_Loading
			// 
			this.LB_Loading.AutoSize = true;
			this.LB_Loading.Location = new System.Drawing.Point(15, 9);
			this.LB_Loading.Name = "LB_Loading";
			this.LB_Loading.Size = new System.Drawing.Size(253, 13);
			this.LB_Loading.TabIndex = 0;
			this.LB_Loading.Text = "Please wait while your old games are being loaded...";
			// 
			// PB_Loading
			// 
			this.PB_Loading.Location = new System.Drawing.Point(12, 34);
			this.PB_Loading.Name = "PB_Loading";
			this.PB_Loading.Size = new System.Drawing.Size(179, 23);
			this.PB_Loading.TabIndex = 1;
			// 
			// BT_Cancel
			// 
			this.BT_Cancel.Location = new System.Drawing.Point(197, 34);
			this.BT_Cancel.Name = "BT_Cancel";
			this.BT_Cancel.Size = new System.Drawing.Size(75, 23);
			this.BT_Cancel.TabIndex = 2;
			this.BT_Cancel.Text = "Cancel";
			this.BT_Cancel.UseVisualStyleBackColor = true;
			this.BT_Cancel.Click += new System.EventHandler(this.BT_Cancel_Click);
			// 
			// FormLoading
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 63);
			this.Controls.Add(this.BT_Cancel);
			this.Controls.Add(this.PB_Loading);
			this.Controls.Add(this.LB_Loading);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.MaximizeBox = false;
			this.Name = "FormLoading";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Loading old data";
			this.TopMost = true;
			this.Shown += new System.EventHandler(this.FormLoading_Shown);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ProgressBar PB_Loading;
		private System.Windows.Forms.Button BT_Cancel;
		private System.Windows.Forms.Label LB_Loading;
	}
}