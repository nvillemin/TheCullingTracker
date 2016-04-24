using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace TheCullingTracker {
	public class Parser {
		private const int TICK = 250; // ms before checking the log again

		private String path;
		private bool isActive;
		private FormMain form;
		private LogLine lastLine;
		private Dictionary<string, Player> players;

		public Parser(FormMain f, string p) {
			this.isActive = true;
			this.form = f;
			this.path = p;
			this.players = new Dictionary<string, Player>();
			this.LoadData();
			Thread parserThread = new Thread(this.Run);
			parserThread.IsBackground = true;
			parserThread.Start();
		}

		// Store data
		private void SaveData() {
			XmlSerializer writer = new XmlSerializer(typeof(List<Player>));
			using(FileStream fs = File.Open(Directory.GetCurrentDirectory() + Constants.DataFolder + Constants.DataFile, FileMode.Open, FileAccess.Write, FileShare.None)) {
				writer.Serialize(fs, this.players.Values.ToList());
			}
		}

		// Load data stored previously
		private void LoadData() {
			if(!File.Exists(Directory.GetCurrentDirectory() + Constants.DataFolder + Constants.DataFile)) {
				using(XmlWriter writer = XmlWriter.Create(Directory.GetCurrentDirectory() + Constants.DataFolder + Constants.DataFile)) {
					writer.WriteStartDocument();
				}
				// Create old data
				FormLoading formLoading = new FormLoading(this, this.path);
				formLoading.ShowDialog();
			} else {
				XmlSerializer writer = new XmlSerializer(typeof(List<Player>));
				using(FileStream fs = File.Open(Directory.GetCurrentDirectory() + Constants.DataFolder + Constants.DataFile, FileMode.Open, FileAccess.Read, FileShare.Read)) {
					List<Player> players = (List<Player>)writer.Deserialize(fs);
					foreach(Player player in players) {
						this.players.Add(player.name, player);
					}
				}
			}
		}

		// Start the parser to check the log file
		internal void Run() {
			using(FileStream fs = File.Open(this.path + Constants.LogFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) {
				StreamReader sr = new StreamReader(fs);
				string line;

				while(isActive) {
					while(sr.EndOfStream && isActive)
						Thread.Sleep(TICK);

					line = sr.ReadLine();
					if(line != null) {
						this.CheckLogLine(new LogLine(line));
					}
				}
			}
		}

		// Check the log line
		public void CheckLogLine(LogLine logLine) {
			switch(logLine.lineType) {
				// New state: Playing or MainMenu
				case LogLine.LineType.NewState:
					if(logLine.state == "Playing") {
						// New game
						this.form.ClearDGV();
						this.form.SetStatus("IN GAME");
					} else {
						// Back to main menu
						this.SaveData();
						if(this.lastLine.lineType == LogLine.LineType.DmgFrom) {
							// TODO record death?
						}
						this.form.SetStatus("IN MAIN MENU");
					}
					break;

				// New player recorded at the start of the game
				case LogLine.LineType.NewPlayer:
					int games = 0, kills = 0;
					if(this.players.ContainsKey(logLine.player)) {
						// Already played with this player before, load data
						Player playerData = this.players[logLine.player];
						games = playerData.games;
						kills = playerData.kills;
					} else {
						// Never played with this player, create data
						this.players.Add(logLine.player, new Player(logLine.player));
					}
					this.players[logLine.player].AddGame();
					this.form.AddPlayer(logLine.player, games, kills);
					break;

				// Wow you just hit someone
				case LogLine.LineType.DmgTo:
					this.form.AddDamage(logLine.player, logLine.damage, false);
					if(this.lastLine.lineType == LogLine.LineType.Kill) {
						// You killed someone, what a beast
						this.players[logLine.player].AddKill();
						this.form.AddKill(logLine.player);
					}
					break;

				// Wow you just got hit
				case LogLine.LineType.DmgFrom:
					// Unknown player is fall damage so it's not recorded
					if(logLine.player != "UNKNOWN") {
						this.form.AddDamage(logLine.player, logLine.damage, true);
					}
					break;

				// End of the file, save the data
				case LogLine.LineType.Close:
					this.SaveData();
					break;
			}

			if(logLine.lineType != LogLine.LineType.Other) {
				this.lastLine = logLine;
			}
		}

		// Stop the parser
		public void Stop() {
			this.isActive = false;
		}
	}
}
