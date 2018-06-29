using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.gStudios.isometric.model.world.tile {

	public interface ITile {

		int X {get;}
		int Y {get;}

		int Type {get; set;}

		void Subscribe (ITileObserver observer);
		
	}

}