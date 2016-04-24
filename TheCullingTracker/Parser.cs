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

namespace TheCullingTracker {
	class Parser {
		private const String PATH = "C:\\Users\\Nath\\AppData\\Local\\Victory\\Saved\\Logs\\Victory.log";
		private const int TICK = 250;

		private bool isActive;
		private FormMain form;
		private LogLine lastLine;
		private Data savedData;

		public Parser(FormMain f) {
			this.isActive = true;
			this.form = f;
			this.LoadData();
			Thread parserThread = new Thread(this.Run);
			parserThread.Start();
		}

		// Load data stored previously
		private void LoadData() {
			if(!Directory.Exists(Directory.GetCurrentDirectory() + "/data")) {
				// No data, create the data folder and load old games data
				Directory.CreateDirectory("data");
				File.Create(Directory.GetCurrentDirectory() + "/data/players").Close();
				this.savedData = new Data();
				this.CreateOldData();
			} else {
				// Load data
				using(Stream stream = new FileStream(Directory.GetCurrentDirectory() + "/data/players", FileMode.Open, FileAccess.Read, FileShare.Read)) {
					IFormatter formatter = new BinaryFormatter();
					this.savedData = (Data)formatter.Deserialize(stream);
				}
			}
		}

		// Load data from games older than the tracker
		private void CreateOldData() {
			// TODO
		}

		// Store data
		private void SerializeData() {
			using(Stream stream = new FileStream(Directory.GetCurrentDirectory() + "/data/players", FileMode.Create, FileAccess.Write, FileShare.None)) {
				IFormatter formatter = new BinaryFormatter();
				formatter.Serialize(stream, this.savedData);
			}
		}

		// Start the parser to check the log file
		internal void Run() {
			try {
				FileStream fs = File.Open(PATH, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
				StreamReader sr = new StreamReader(fs);
				string line;

				while(isActive) {
					while(sr.EndOfStream && isActive)
						Thread.Sleep(TICK);

					line = sr.ReadLine();
					if(line != null) {
						LogLine logLine = new LogLine(line);

						switch(logLine.lineType) {
							// New state: Playing or MainMenu
							case LogLine.LineType.NewState:
								this.form.SetStatus(logLine.state.ToUpper());
								if(logLine.state == "Playing") {
									// New game
									this.form.ClearDGV();
								} else {
									// Back to main menu
									this.SerializeData();
									if(this.lastLine.lineType == LogLine.LineType.DmgFrom) {
										// TODO record death
									}
								}
								break;

							// New player recorded at the start of the game
							case LogLine.LineType.NewPlayer:
								int games = 0, kills = 0;
								if(this.savedData.players.ContainsKey(logLine.player)) {
									// Already played with this player before, load data
									Player playerData = this.savedData.players[logLine.player];
									games = playerData.games;
									kills = playerData.kills;
								} else {
									// Never played with this player, create data
									this.savedData.players.Add(logLine.player, new Player());
								}
								this.savedData.players[logLine.player].AddGame();
								this.form.AddPlayer(logLine.player, games, kills);
								break;

							// Wow you just hit someone
							case LogLine.LineType.DmgTo:
								this.form.AddDamage(logLine.player, logLine.damage, false);
								if(this.lastLine.lineType == LogLine.LineType.Kill) {
									// You killed someone, what a beast
									this.savedData.players[logLine.player].AddKill();
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
								this.SerializeData();
								break;
						}

						if(logLine.lineType != LogLine.LineType.Other) {
							this.lastLine = logLine;
						}
					}
				}

				sr.Close();
			} catch(FileNotFoundException) {
				// TODO
			}
		}

		// Stop the parser
		public void Stop() {
			this.isActive = false;
		}
	}
}
