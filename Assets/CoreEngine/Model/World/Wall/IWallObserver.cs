using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace com.gStudios.isometric.model.world.wall {

	public interface IWallObserver {

		void NotifyWallTypeChanged (IWall wall);
		
	}

}