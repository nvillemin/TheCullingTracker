using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;

namespace TheCullingTracker {
	public class Parser {
		private const int TICK = 1000; // ms before checking the log again

		private String path;
		private bool isActive;
		private FormMain form;
		private LogLine lastLine;
		private Dictionary<string, Player> players;
		private List<string> currentPlayers;

		public Parser(FormMain f, string p) {
			this.isActive = true;
			this.form = f;
			this.path = p;
			this.players = new Dictionary<string, Player>();
			this.currentPlayers = new List<string>();
			this.LoadData();
			Thread parserThread = new Thread(this.Run);
			parserThread.IsBackground = true;
			parserThread.Start();
		}

		// Store data
		private void SaveData() {
			File.WriteAllText(Directory.GetCurrentDirectory() + Constants.DataFolder + Constants.DataFile, string.Empty);
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
				// Wow you just hit someone
				case LogLine.LineType.DmgTo:
					// Check if we already encountered the player
					if(!this.currentPlayers.Contains(logLine.player)) {
						this.AddPlayer(logLine.player);
					}

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
						// Check if we already encountered the player
						if(!this.currentPlayers.Contains(logLine.player)) {
							this.AddPlayer(logLine.player);
						}
						this.form.AddDamage(logLine.player, logLine.damage, true);
					}
					break;

				// New player recorded at the start of the game
				case LogLine.LineType.NewPlayer:
					this.AddPlayer(logLine.player);
					break;

				// New state: Playing or MainMenu
				case LogLine.LineType.NewState:
					if(logLine.state == "Playing") {
						// New game
						this.currentPlayers.Clear();
						this.form.ClearDGV();
						this.form.SetStatus("IN GAME");
					} else {
						// Back to main menu
						this.form.SetStatus("IN MAIN MENU");
					}
					break;
			}

			if(logLine.lineType != LogLine.LineType.Other) {
				this.lastLine = logLine;
			}
		}

		// Add a new player to the game
		private void AddPlayer(string player) {
			this.currentPlayers.Add(player);
			int games = 0, kills = 0;
			if(this.players.ContainsKey(player)) {
				// Already played with this player before, load data
				Player playerData = this.players[player];
				games = playerData.games;
				kills = playerData.kills;
			} else {
				// Never played with this player, create data
				this.players.Add(player, new Player(player));
			}
			this.players[player].AddGame();
			this.form.AddPlayer(player, games, kills);
		}

		// Stop the parser
		public void Stop() {
			this.isActive = false;
			this.SaveData();
		}
	}
}
