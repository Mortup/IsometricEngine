using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.gStudios.isometric.controller.config {

	public static class Paths {

		public const string TilesSprites = "Sprites/Tiles";
		const string CursorSprites = "Sprites/Cursors/";

		public const string CursorPrefab = "Prefabs/Cursor";

		public static string CursorSprite(string name) {
			return CursorSprites + name;
		}

		public static string TileSprite(int index) {
			return TilesSprites + index.ToString ();
		}
		
	}

}