using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace TheCullingTracker {
	public partial class FormAbout : Form {
		private string version = "1.3.1";

		public FormAbout() {
			InitializeComponent();
		}

		private void BT_Ok_Click(object sender, EventArgs e) {
			this.Close();
		}

		private void LL_Source_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			Process.Start("https://github.com/nvillemin/TheCullingTracker");
		}

		private void LL_Email_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			Process.Start("mailto:" + LL_Email.Text);
		}

		private void FormAbout_Load(object sender, EventArgs e) {
			LB_Version.Text = "Version: " + this.version;
		}
	}
}
