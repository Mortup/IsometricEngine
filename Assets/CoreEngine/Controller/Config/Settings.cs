using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.gStudios.isometric.controller.config {

	public static class Settings {

		public const int MaxCursorUndoStackSize = 20;
		public const int CursorPoolInitialSize = 200;

        /* ------------------------
         * GRAPHIC SETTINGS
         * ------------------------*/
        public const bool DRAW_WALL_BORDERS = false;

        // Sprite sizes on Unity's World units.
        public const float TILE_WIDTH = 1;
        public const float TILE_HEIGHT = 0.5f;
        public const float TILE_WIDTH_HALF = TILE_WIDTH / 2f;
        public const float TILE_HEIGHT_HALF = TILE_HEIGHT / 2f;

        public const int PPU = 64;
        public const int sprExtraHeight = 3; // Extra pixels used to add tile thickness.
        public const int sprWidth = PPU + 2;
        public const int sprHeigh = sprWidth / 2 + sprExtraHeight;


        public const bool mipmapEnabled = false;
        public const FilterMode filterMode = FilterMode.Point;
        public const TextureWrapMode wrapMode = TextureWrapMode.Clamp;

        public static readonly Vector2 tilePivot = new Vector2(0, ((float) 26 / 42));
        public static readonly Vector2 furniturePivot = new Vector2(0, ((float)26 / 132));
        public static readonly Vector2 wallPivot = new Vector2(((float)2 / 38), ((float)2 / 130));
        public static readonly Vector2 wallCursorPivot = new Vector2(0.5f, 0.02f);
    }

}