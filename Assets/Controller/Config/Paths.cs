using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.gStudios.isometric.controller.config {

	public static class Paths {

		const string TilesSprites = "Sprites/Tiles/";
		const string CursorSprites = "Sprites/Cursors/";

		public static string CursorSprite(string name) {
			return CursorSprites + name;
		}

		public static string TileSprite(int index) {
			return TileSprite (index.ToString ());
		}

		public static string TileSprite(string name) {
			return TilesSprites + name;
		}
		
	}

}