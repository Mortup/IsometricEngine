﻿using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace com.gStudios.isometric.model.world {

	public interface ITileObserver {

		void NotifyTileTypeChanged (Tile tile);

	}

}