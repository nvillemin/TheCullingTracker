using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheCullingTracker {
	[Serializable]
	class Data {
		public Dictionary<string, Player> players { get; private set; }

		public Data() {
			this.players = new Dictionary<string, Player>();
		}
	}
}
