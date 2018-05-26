using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.gStudios.isometric.model.world.wall {

	public abstract class AbstractWall : IWall {

		Level level;

		protected int x;
		protected int y;
		protected int z;
		protected int type;

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

		public abstract int Type { get; set; }

		public AbstractWall(Level level, int x, int y, int z) {
			this.level = level;

			this.x = x;
			this.y = y;
			this.z = z;

			this.type = WallIndex.EmptyWallIndex;
		}

		public IWall GetNeighbor(int xOffset, int yOffset, int z) {
			if (!level.IsWallInBounds (x + xOffset, y + yOffset, z))
				return null; // TODO: Or error? Or NullWall?

			return level.GetWallAt (x + xOffset, y + yOffset, z);
		}

		public abstract void Subscribe (IWallObserver observer);


	}

}