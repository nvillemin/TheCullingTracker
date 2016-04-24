using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheCullingTracker {
	[Serializable]
	class Player {
		public int games { get; private set; }
		public int kills { get; private set; }
		private int deaths; 

		public Player() {
			this.games = this.kills = this.deaths = 0;
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
