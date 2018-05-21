using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.gStudios.isometric.model.world.wall {

	public class NullWall : IWall {

		int x;
		int y;
		int z;

		public int Type {
			get {
				return WallIndex.EmptyWallIndex;
			}
			set {
				// Do nothing
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

		public int Z {
			get {
				return z;
			}
		}

		public NullWall(int x, int y, int z) {
			this.x = x;
			this.y = y;
			this.z = z;
		}
			
		public void Subscribe(IWallObserver observer) {
			// Do nothing.
		}
	}

}