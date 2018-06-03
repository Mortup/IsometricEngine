﻿using System.IO;

namespace com.gStudios.isometric.controller.config {

	public static class GamePaths {

        public static readonly string TilesSprites = Path.Combine("Sprites", "Tiles");
        public static readonly string WallSprites = Path.Combine("Sprites", "Walls");

		private static readonly string CursorSprites = Path.Combine("Sprites", "Cursors");
		private static readonly string JsonDatas = "Data";

		public static readonly string CursorPrefab = Path.Combine("Prefabs", "Cursor");

        public static readonly string ResourcesBase = Path.Combine(Directory.GetCurrentDirectory(), Path.Combine("Assets", "Resources"));

        public static string CursorSprite(string name) {
			return Path.Combine(CursorSprites, name);
		}

		public static string TileSprite(int index) {
			return Path.Combine(TilesSprites, "Floor" + index.ToString ());
		}

		public static string JsonData(string name) {
			return Path.Combine(JsonDatas, name + "Data");
		}
		
	}

}