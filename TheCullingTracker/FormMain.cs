using System.Threading;
using System.Windows.Forms;
using System;
using System.Drawing;
using System.Collections.Generic;

namespace TheCullingTracker {
	public partial class FormMain : Form {
		private Parser parser;
		private int nextDgvRow;
		private Dictionary<string, int> playerIndex;

		public FormMain() {
			InitializeComponent();
			this.InitializeDGV();
			this.parser = new Parser(this);
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
			this.BeginInvoke(invoker);
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
			this.BeginInvoke(invoker);
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
			this.BeginInvoke(invoker);
		}

		// Add damage value to the according player
		public void AddDamage(string player, float damage, bool isReceived) {
			MethodInvoker invoker = delegate {
				int cellIndex = (isReceived) ? 2 : 1;
				float oldValue = (float)this.DGV_Game.Rows[this.playerIndex[player]].Cells[cellIndex].Value;
				float newValue = (float)Math.Round((Decimal)(oldValue + damage), 2, MidpointRounding.AwayFromZero);
				this.DGV_Game.Rows[this.playerIndex[player]].Cells[cellIndex].Value = newValue;
			};
			this.BeginInvoke(invoker);
		}

		// Add kill value to the according player
		public void AddKill(string player) {
			MethodInvoker invoker = delegate {
				int oldValue = (int)this.DGV_Game.Rows[this.playerIndex[player]].Cells[4].Value;
				this.DGV_Game.Rows[this.playerIndex[player]].Cells[4].Value = oldValue + 1;
			};
			this.BeginInvoke(invoker);
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

		// When closing the form
		private void FormMain_FormClosing(object sender, FormClosingEventArgs e) {
			this.parser.Stop();
		}
	}
}
