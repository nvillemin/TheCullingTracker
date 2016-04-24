using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TheCullingTracker {
	class LogLine {
		public enum LineType { Other, Open, Close, NewState, NewPlayer, DmgTo, DmgFrom, Kill }

		public LineType lineType { get; private set; }
		public String state { get; private set; }
		public String player { get; private set; }
		public float damage { get; private set; }

		public LogLine(string line) {
			this.ParseLine(line);
		}

		// Parse the current log line
		private void ParseLine(string line) {
			Match match = Regex.Match(line, "Log file open");
			if(match.Success) {
				this.lineType = LineType.Open;
				return;
			}

			match = Regex.Match(line, "Log file closed");
			if(match.Success) {
				this.lineType = LineType.Close;
				return;
			}

			match = Regex.Match(line, "NewState: ([A-Za-z]+)$");
			if(match.Success) {
				this.lineType = LineType.NewState;
				this.state = match.Groups[1].Value;
				return;
			}

			match = Regex.Match(line, "for (.+) to Switch_3P$");
			if(match.Success) {
				this.lineType = LineType.NewPlayer;
				this.player = match.Groups[1].Value;
				return;
			}

			match = Regex.Match(line, "You Hit (.+) for ([0-9]+\\.[0-9]+) damage");
			if(match.Success) {
				this.lineType = LineType.DmgTo;
				this.player = match.Groups[1].Value;
				this.damage = float.Parse(match.Groups[2].Value, CultureInfo.InvariantCulture);
				return;
			}

			match = Regex.Match(line, "Struck by (.+) for ([0-9]+\\.[0-9]+) damage");
			if(match.Success) {
				this.lineType = LineType.DmgFrom;
				this.player = match.Groups[1].Value;
				this.damage = float.Parse(match.Groups[2].Value, CultureInfo.InvariantCulture);
				return;
			}

			match = Regex.Match(line, "ACH_FRAG_SOMEONE");
			if(match.Success) {
				this.lineType = LineType.Kill;
				return;
			}

			this.lineType = LineType.Other;
		}
	}
}
