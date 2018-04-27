using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using com.gStudios.isometric.controller.config;

namespace com.gStudios.isometric.controller.data {

	public class CursorSpriteDataLoader {

		public Sprite defaultSprite;
		public Sprite emptySprite;

		public Sprite buildSprite;
		public Sprite buildInverseSprite;
		public Sprite buildOverTileSprite;
		public Sprite buildInvertedOnEmptySprite;

		public CursorSpriteDataLoader() {
			defaultSprite = Resources.Load<Sprite> (Paths.CursorSprite ("Default"));
			emptySprite = Resources.Load<Sprite> (Paths.CursorSprite ("Empty"));

			buildSprite = Resources.Load<Sprite> (Paths.CursorSprite ("Build"));
			buildInverseSprite = Resources.Load<Sprite> (Paths.CursorSprite ("BuildInverse"));
			buildOverTileSprite = Resources.Load<Sprite> (Paths.CursorSprite ("BuildOverTile"));
			buildInvertedOnEmptySprite = Resources.Load<Sprite> (Paths.CursorSprite ("BuildInverseOnEmpty"));
		}
		
	}

}