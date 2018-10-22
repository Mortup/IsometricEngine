using System.IO;

namespace com.gStudios.isometric.controller.config {

	public static class GamePaths {

        public static readonly string TilesSprites = "Sprites/Tiles";
        public static readonly string WallSprites = "Sprites/Walls";

        private static readonly string CursorSprites = "Sprites/Cursors";
        private static readonly string TileCursorSprites = CursorSprites + "/TileCursors";
        private static readonly string WallCursorSprites = CursorSprites + "/WallCursors";

		private static readonly string JsonDatas = "Data";

        public static readonly string CursorPrefab = "Prefabs/Cursor";

        public static string CursorSprite(string name) {
            return Path.Combine(CursorSprites, name);
        }

        public static string TileCursorSprite(string name) {
            return Path.Combine(TileCursorSprites, name);
        }

        public static string WallCursorSprite(string name) {
            return Path.Combine(WallCursorSprites, name);
        }

		public static string JsonData(string name) {
			return Path.Combine(JsonDatas, name + "Data");
		}
		
	}

}