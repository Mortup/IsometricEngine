using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.gStudios.isometric.model.world.wall {

	public class NullWall : AbstractWall {

		public override int Type {
			get {
				return WallIndex.Empty;
			}
			set {
				// Do nothing
			}
		}
			
		public NullWall(Level level, int x, int y, int z) : base(level, x, y, z) {}
			
		public override void Subscribe(IWallObserver observer) {
			// Do nothing.
		}
	}

}