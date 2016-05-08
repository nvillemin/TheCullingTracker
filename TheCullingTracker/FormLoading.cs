using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheCullingTracker {
	public partial class FormLoading : Form {
		private Parser parser;
		private string path;
		private Thread loadingThread;

		public FormLoading(Parser parser, string path) {
			this.parser = parser;
			this.path = path;
			this.loadingThread = new Thread(this.CreateOldData);
			loadingThread.IsBackground = true;
			InitializeComponent();
		}

		// Load data from games older than the tracker
		private void CreateOldData() {
			string[] fileNames = Directory.GetFiles(this.path, "Victory-backup-*");
			int nbTotalLines = 0;
			foreach(string fileName in fileNames) {
				nbTotalLines += File.ReadLines(fileName).Count();
			}
			int linesStep = nbTotalLines / 50;
			int progress = 0;
			int nbLines = 0;
			foreach(string fileName in fileNames) {
				// Check each backup file
				using(FileStream fs = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) {
					StreamReader sr = new StreamReader(fs);
					string line;

					while(!sr.EndOfStream) {
						line = sr.ReadLine();
						if(line != null) {
							this.parser.CheckLogLine(new LogLine(line));
							nbLines++;
							if(nbLines >= linesStep) {
								nbLines = 0;
								progress += 2;
								this.SetProgress(progress);
							}
						}
					}
				}
			}
			this.SetClose();
		}

		// Update the progress bar from the thread
		private void SetProgress(int progress) {
			MethodInvoker invoker = delegate {
				this.PB_Loading.Value = progress;
			};
			if(this.Visible) {
				this.Invoke(invoker);
			}
		}

		// Close called from the thread
		private void SetClose() {
			MethodInvoker invoker = delegate {
				this.Close();
			};
			this.BeginInvoke(invoker);
		}

		// Cancel the loading
		private void BT_Cancel_Click(object sender, EventArgs e) {
			this.loadingThread.Abort();
			this.Close();
		}

		// When the form is shown, start a thread to create the data
		private void FormLoading_Shown(object sender, EventArgs e) {
			this.loadingThread.Start();
		}
	}
}
