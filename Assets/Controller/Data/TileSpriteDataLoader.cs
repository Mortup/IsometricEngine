using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using com.gStudios.isometric.controller.config;
using com.gStudios.isometric.model.data.structures;

namespace com.gStudios.isometric.controller.data {

	public class TileSpriteDataLoader {

		Sprite[] sprites;

		public TileSpriteDataLoader() {
			sprites = Resources.LoadAll<Sprite> (Paths.TilesSprites);
		}

		public Sprite[] GetData() {
			return sprites;
		}

		public Sprite GetDataById(int id) {
			return sprites [id];
		}

		public Sprite GetDataByTileData(TileData td) {
			return GetDataById (td.id);
		}
		
	}

}