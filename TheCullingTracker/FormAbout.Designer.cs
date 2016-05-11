namespace TheCullingTracker {
	partial class FormAbout {
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
			this.BT_Ok = new System.Windows.Forms.Button();
			this.LB_Description = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.LL_Email = new System.Windows.Forms.LinkLabel();
			this.LL_Source = new System.Windows.Forms.LinkLabel();
			this.LB_Version = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// BT_Ok
			// 
			this.BT_Ok.Location = new System.Drawing.Point(163, 65);
			this.BT_Ok.Name = "BT_Ok";
			this.BT_Ok.Size = new System.Drawing.Size(75, 23);
			this.BT_Ok.TabIndex = 0;
			this.BT_Ok.Text = "OK";
			this.BT_Ok.UseVisualStyleBackColor = true;
			this.BT_Ok.Click += new System.EventHandler(this.BT_Ok_Click);
			// 
			// LB_Description
			// 
			this.LB_Description.AutoSize = true;
			this.LB_Description.Location = new System.Drawing.Point(12, 9);
			this.LB_Description.Name = "LB_Description";
			this.LB_Description.Size = new System.Drawing.Size(376, 13);
			this.LB_Description.TabIndex = 1;
			this.LB_Description.Text = "The Culling Tracker is an open-source software created by Nathanaël Villemin.";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(18, 33);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(256, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "For any questions or suggestions feel free to contact:";
			// 
			// LL_Email
			// 
			this.LL_Email.AutoSize = true;
			this.LL_Email.Location = new System.Drawing.Point(271, 33);
			this.LL_Email.Name = "LL_Email";
			this.LL_Email.Size = new System.Drawing.Size(108, 13);
			this.LL_Email.TabIndex = 3;
			this.LL_Email.TabStop = true;
			this.LL_Email.Text = "villemin.n@gmail.com";
			this.LL_Email.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LL_Email_LinkClicked);
			// 
			// LL_Source
			// 
			this.LL_Source.AutoSize = true;
			this.LL_Source.Location = new System.Drawing.Point(12, 70);
			this.LL_Source.Name = "LL_Source";
			this.LL_Source.Size = new System.Drawing.Size(68, 13);
			this.LL_Source.TabIndex = 4;
			this.LL_Source.TabStop = true;
			this.LL_Source.Text = "Source code";
			this.LL_Source.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LL_Source_LinkClicked);
			// 
			// LB_Version
			// 
			this.LB_Version.AutoSize = true;
			this.LB_Version.Location = new System.Drawing.Point(313, 70);
			this.LB_Version.Name = "LB_Version";
			this.LB_Version.Size = new System.Drawing.Size(75, 13);
			this.LB_Version.TabIndex = 5;
			this.LB_Version.Text = "Version: X.X.X";
			// 
			// FormAbout
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(400, 95);
			this.Controls.Add(this.LB_Version);
			this.Controls.Add(this.LL_Source);
			this.Controls.Add(this.LL_Email);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.LB_Description);
			this.Controls.Add(this.BT_Ok);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormAbout";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "About The Culling Tracker";
			this.Load += new System.EventHandler(this.FormAbout_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button BT_Ok;
		private System.Windows.Forms.Label LB_Description;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.LinkLabel LL_Email;
		private System.Windows.Forms.LinkLabel LL_Source;
		private System.Windows.Forms.Label LB_Version;
	}
}