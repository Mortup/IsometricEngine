using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace com.gStudios.isometric.model.world {

	public interface IWallObserver {

		void NotifyWallTypeChanged (Wall wall);
		
	}

}