using System.Threading;
using System.Windows.Forms;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace TheCullingTracker {
	public partial class FormMain : Form {
		private Parser parser;
		private int nextDgvRow;
		private Dictionary<string, int> playerIndex;
		private String path = "C:\\Users\\" + Environment.UserName + "\\AppData\\Local\\Victory\\Saved\\Logs";

		public FormMain() {
			InitializeComponent();
			this.CheckLogsPath();
			this.InitializeDGV();
			this.parser = new Parser(this, this.path);
		}

		// Initialize the DataGridView
		private void InitializeDGV() {
			for(int i = 0; i < 15; ++i) {
				this.DGV_Game.Rows.Add("----------------", 0.0f, 0.0f, 0, 0);
			}
			this.nextDgvRow = 0;
			this.playerIndex = new Dictionary<string, int>();
		}

		// Change main status
		public void SetStatus(string status) {
			MethodInvoker invoker = delegate {
				this.LB_Status.Text = status;
			};
			if(this.Visible) {
				this.BeginInvoke(invoker);
			}
		}

		// Clear the DGV to prepare for a new game
		public void ClearDGV() {
			MethodInvoker invoker = delegate {
				this.playerIndex.Clear();
				for(int i = 0; i < 15; ++i) {
					this.DGV_Game.Rows[i].SetValues("---", 0.0f, 0.0f, 0, 0);
				}
				this.nextDgvRow = 0;
			};
			if(this.Visible) {
				this.BeginInvoke(invoker);
			}
		}

		// Add a player to the DGV
		public void AddPlayer(string player, int games, int kills) {
			MethodInvoker invoker = delegate {
				this.playerIndex.Add(player, nextDgvRow);
				this.DGV_Game.Rows[nextDgvRow].Cells[0].Value = player;
				this.DGV_Game.Rows[nextDgvRow].Cells[3].Value = games;
				this.DGV_Game.Rows[nextDgvRow].Cells[4].Value = kills;
				this.ResizeForm();
				nextDgvRow++;
			};
			if(this.Visible) {
				this.BeginInvoke(invoker);
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
				this.BeginInvoke(invoker);
			}
		}

		// Add kill value to the according player
		public void AddKill(string player) {
			MethodInvoker invoker = delegate {
				int oldValue = (int)this.DGV_Game.Rows[this.playerIndex[player]].Cells[4].Value;
				this.DGV_Game.Rows[this.playerIndex[player]].Cells[4].Value = oldValue + 1;
			};
			if(this.Visible) {
				this.BeginInvoke(invoker);
			}
		}

		// Resize the form according to the DGV
		private void ResizeForm() {
			int maxWidth = 0;
			foreach(DataGridViewRow row in DGV_Game.Rows) {
				int rowWidth = 0;
				foreach(DataGridViewCell cell in row.Cells) {
					rowWidth += cell.Size.Width;
				}
				if(rowWidth > maxWidth) {
					maxWidth = rowWidth;
				}
			}
			this.Size = new Size(maxWidth + 33, 404);
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
	}
}
