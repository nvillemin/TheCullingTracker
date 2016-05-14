using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace TheCullingTracker {
	public partial class FormMain : Form {
		public Dictionary<string, int> playerIndex { get; private set; }

		private Parser parser;
		private int nextDgvRow;
		private String path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Victory\\Saved\\Logs");

		public FormMain() {
			InitializeComponent();
			this.CheckLogsPath();
			this.playerIndex = new Dictionary<string, int>();
			this.nextDgvRow = 0;
			this.parser = new Parser(this, this.path);
		}

		// Change main status
		public void SetStatus(string status) {
			MethodInvoker invoker = delegate {
				this.LB_Status.Text = status;
			};
			if(this.Visible) {
				this.Invoke(invoker);
			}
		}

		// Clear the DGV to prepare for a new game
		public void ClearDGV() {
			MethodInvoker invoker = delegate {
				this.nextDgvRow = 0;
				this.playerIndex.Clear();
				this.DGV_Game.Rows.Clear();
				this.DGV_Game.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
				this.DGV_Game.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
				this.ResizeForm();
			};
			if(this.Visible) {
				this.Invoke(invoker);
			}
		}

		// Add a player to the DGV
		public void AddPlayer(string player, int games, int kills) {
			MethodInvoker invoker = delegate {
				this.playerIndex.Add(player, nextDgvRow);
				nextDgvRow++;
				this.DGV_Game.Rows.Add(player, 0f, 0f, games, kills);
				this.ResizeForm();
			};
			if(this.Visible) {
				this.Invoke(invoker);
			}
		}

		// Add damage value to the according player
		public void AddDamage(string player, float damage, bool isReceived) {
			MethodInvoker invoker = delegate {
				int cellIndex = (isReceived) ? 2 : 1;
				float oldValue = (float)this.DGV_Game.Rows[this.playerIndex[player]].Cells[cellIndex].Value;
				float newValue = (float)Math.Round((Decimal)(oldValue + damage), 2, MidpointRounding.AwayFromZero);
				this.DGV_Game.Rows[this.playerIndex[player]].Cells[cellIndex].Value = newValue;
			};
			if(this.Visible) {
				this.Invoke(invoker);
			}
		}

		// Add kill value to the according player
		public void AddKill(string player) {
			MethodInvoker invoker = delegate {
				int oldValue = (int)this.DGV_Game.Rows[this.playerIndex[player]].Cells[4].Value;
				this.DGV_Game.Rows[this.playerIndex[player]].Cells[4].Value = oldValue + 1;
			};
			if(this.Visible) {
				this.Invoke(invoker);
			}
		}

		// Resize the form according to the DGV
		private void ResizeForm() {
			int maxWidth = 230;
			int height = 128;
			foreach(DataGridViewRow row in DGV_Game.Rows) {
				height += 20;
				int rowWidth = 0;
				foreach(DataGridViewCell cell in row.Cells) {
					rowWidth += cell.Size.Width;
				}
				if(rowWidth > maxWidth) {
					maxWidth = rowWidth;
				}
			}
			this.Size = new Size(maxWidth + 33, height);
		}

		// Check the logs path and ask the user if it's not correct
		private void CheckLogsPath() {
			if(!Directory.Exists(Directory.GetCurrentDirectory() + Constants.DataFolder)) {
				// No data, create the settings file
				Directory.CreateDirectory(Constants.DataFolder.Substring(1));
				this.CreateSettingsFile();
			} else {
				if(!File.Exists(Directory.GetCurrentDirectory() + Constants.DataFolder + Constants.SettingsFile)) {
					this.CreateSettingsFile();
				} else {
					// Load the path from the settings file
					XmlDocument dataDoc = new XmlDocument();
					using(FileStream fs = new FileStream(Directory.GetCurrentDirectory() + Constants.DataFolder + Constants.SettingsFile, FileMode.Open, FileAccess.Read, FileShare.Read)) {
						dataDoc.Load(fs);
						this.path = dataDoc.InnerText;
					}
				}
			}

			// Check the log path
			while(!File.Exists(this.path + Constants.LogFile)) {
				MessageBox.Show("The tracker couldn't locate your log files.\nPlease select the folder where \"" + Constants.LogFile.Substring(1) + "\" is located.", "Couldn't locate log files");
				if(this.folderBrowserDialog.ShowDialog() == DialogResult.OK) {
					this.path = this.folderBrowserDialog.SelectedPath;
					XmlDocument dataDoc = new XmlDocument();
					using(FileStream fs = new FileStream(Directory.GetCurrentDirectory() + Constants.DataFolder + Constants.SettingsFile, FileMode.Open, FileAccess.Read, FileShare.Read)) {
						dataDoc.Load(fs);
						dataDoc.SelectSingleNode("path").InnerXml = this.path;
						dataDoc.Save(Directory.GetCurrentDirectory() + Constants.DataFolder + Constants.DataFile);
					}
				} else {
					// No log file path, no tracker
					Environment.Exit(0);
				}
			}
		}

		// Create a new file storing the settings like the path for the log file
		private void CreateSettingsFile() {
			XmlWriterSettings settings = new XmlWriterSettings();
			settings.Indent = true;
			using(XmlWriter writer = XmlWriter.Create(Directory.GetCurrentDirectory() + Constants.DataFolder + Constants.SettingsFile, settings)) {
				writer.WriteStartDocument();
				writer.WriteElementString("path", this.path);
			}
		}

		// When closing the form
		private void FormMain_FormClosing(object sender, FormClosingEventArgs e) {
			if(this.parser != null) {
				this.parser.Stop();
			}
		}

		// Load data from menu
		private void loadDataToolStripMenuItem_Click(object sender, EventArgs e) {
			this.parser.Stop();
			this.playerIndex.Clear();
			this.nextDgvRow = 0;
			if(File.Exists(Directory.GetCurrentDirectory() + Constants.DataFolder + Constants.DataFile)) {
				File.Delete(Directory.GetCurrentDirectory() + Constants.DataFolder + Constants.DataFile);
			}
			this.Hide();
			this.parser = new Parser(this, this.path);
			this.Show();
		}
		
		// Open GitHub latest release
		private void updateToolStripMenuItem_Click(object sender, EventArgs e) {
			Process.Start("https://github.com/nvillemin/TheCullingTracker/releases/latest");
		}

		// Open about form
		private void aboutToolStripMenuItem_Click(object sender, EventArgs e) {
			new FormAbout().ShowDialog();
		}
	}
}
