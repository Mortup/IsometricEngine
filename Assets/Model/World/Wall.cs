using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.gStudios.isometric.model.world {

	public class Wall {

		public const int EmptyWallIndex = 0;
		public const int NewWallIndex = 1;

		int x;
		int y;
		int type = 0;

		// Observers list

		public int Type {
			get {
				return type;
			}
			set {
				type = value;

				/*foreach (ITileObserver tileObserver in observers) {
					tileObserver.NotifyTileTypeChanged (this);
				}*/
			}
		}

		public int X {
			get {
				return x;
			}
		}

		public int Y {
			get {
				return y;
			}
		}

		public Wall(int x, int y) {
			this.x = x;
			this.y = y;
		}
	}

}