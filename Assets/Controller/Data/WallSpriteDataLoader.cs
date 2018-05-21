using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using com.gStudios.isometric.controller.config;

namespace com.gStudios.isometric.controller.data {

	public class WallSpriteDataLoader {

		Sprite[] sprites;

		public WallSpriteDataLoader() {
			sprites = Resources.LoadAll<Sprite> (Paths.WallSprites);
		}

		public Sprite[] GetData() {
			return sprites;
		}

		public Sprite GetDataById(int id) {
			return sprites [id];
		}
		
	}

}