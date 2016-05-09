using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace TheCullingTracker {
	public partial class FormLoading : Form {
		private Parser parser;
		private string path;

		public FormLoading(Parser parser, string path) {
			this.parser = parser;
			this.path = path;
			InitializeComponent();
		}

		// Cancel the loading
		private void BT_Cancel_Click(object sender, EventArgs e) {
			this.backgroundWorker.CancelAsync();
			this.Close();
		}

		// When the form is shown, start a thread to create the data
		private void FormLoading_Shown(object sender, EventArgs e) {
			this.backgroundWorker.RunWorkerAsync();
		}

		// Load data from games older than the tracker
		private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e) {
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

					while(!sr.EndOfStream && !this.backgroundWorker.CancellationPending) {
						line = sr.ReadLine();
						if(line != null) {
							this.parser.CheckLogLine(new LogLine(line));
							nbLines++;
							if(nbLines >= linesStep) {
								nbLines = 0;
								progress += 2;
								this.backgroundWorker.ReportProgress(progress);
							}
						}
					}
				}
			}
		}

		// Update progress bar
		private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e) {
			if(this.PB_Loading.Value > 0) {
				this.PB_Loading.Value--;
			}
			this.PB_Loading.Value = e.ProgressPercentage;
			if(this.PB_Loading.Value == 100) {
				this.Close();
			}
		}
	}
}
