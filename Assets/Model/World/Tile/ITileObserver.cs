using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace com.gStudios.isometric.model.world.tile {

	public interface ITileObserver {

		void NotifyTileTypeChanged (ITile tile);

	}

}