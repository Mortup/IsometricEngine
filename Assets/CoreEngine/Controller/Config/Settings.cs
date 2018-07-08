using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.gStudios.isometric.controller.config {

	public static class Settings {

		public const int MaxCursorUndoStackSize = 20;
		public const int CursorPoolInitialSize = 200;

        // Sprite sizes on Unity's World units.
        public const float TILE_WIDTH = 1;
        public const float TILE_HEIGHT = 0.5f;
        public const float TILE_WIDTH_HALF = TILE_WIDTH / 2f;
        public const float TILE_HEIGHT_HALF = TILE_HEIGHT / 2f;
    }

}