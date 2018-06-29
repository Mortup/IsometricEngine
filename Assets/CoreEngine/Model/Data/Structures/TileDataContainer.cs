using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.gStudios.isometric.model.data.structures {

	// Used just to load json files. We wanted to have all tiles information on
	// a single json file, so we load it here and then load all the single
	// TileData info.

	[Serializable]
	public class TileDataContainer {

		public string cosa;
		public List<TileData> data;
		
	}

}