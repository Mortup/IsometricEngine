using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace com.gStudios.isometric.controller {

	public static class IsometricTransformer {
		// Based on the algorithms found on http://clintbellanger.net/articles/isometric_math/

		public const float TILE_WIDTH = 1;
		public const float TILE_HEIGHT = 0.5f;

		public const float TILE_WIDTH_HALF = 0.5f;
		public const float TILE_HEIGHT_HALF = 0.25f;

		/// <summary>
		/// Converts isometric tile coordinates to a screen position.
		/// </summary>
		/// <returns>The screen position.</returns>
		/// <param name="x">The x isometric coordinate.</param>
		/// <param name="y">The y isometric coordinate.</param>
		public static Vector2 CoordToScreen(int x, int y) {
			return new Vector2((x - y) * TILE_WIDTH_HALF, (x + y) * TILE_HEIGHT_HALF);
		}

	}

}