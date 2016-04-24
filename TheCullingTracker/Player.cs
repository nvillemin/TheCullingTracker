using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TheCullingTracker {
	public class Player {
		public string name { get; set; }
		public int games { get; set; }
		public int kills { get; set; }

		public Player() {
			this.games = this.kills = 0;
		}

		public Player(string n) : this() {
			this.name = n;
		}

		// You killed this guy
		public void AddKill() {
			this.kills++;
		}

		// You played with this guy
		public void AddGame() {
			this.games++;
		}
	}
}
