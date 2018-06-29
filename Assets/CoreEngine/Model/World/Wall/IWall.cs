using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.gStudios.isometric.model.world.wall {

	public interface IWall {

		int X {get;}
		int Y {get;}
		int Z {get;}

		int Type {get; set;}

		void Subscribe (IWallObserver observer);

		IWall GetNeighbor (int xOffset, int yOffset, int z);
	}

}