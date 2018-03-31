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
		/// Converts isometric tile coordinates to a world position.
		/// </summary>
		/// <returns>The world position.</returns>
		/// <param name="x">The x isometric coordinate.</param>
		/// <param name="y">The y isometric coordinate.</param>
		public static Vector2 CoordToWorld(int x, int y) {
			Vector2 world = new Vector2 ((y - x) * TILE_WIDTH_HALF, -(x + y) * TILE_HEIGHT_HALF);
			Vector2 offset = new Vector2 (TILE_WIDTH_HALF, -TILE_HEIGHT_HALF);
			return world - offset;
		}

		/// <summary>
		/// Converts world position to isometric tile coordinates.
		/// </summary>
		/// <returns>The tile coordinate coordinate.</returns>
		/// <param name="screen">The screen position.</param>
		public static Vector2Int WorldToCoord(Vector2 world) {
			world.x = -world.x;
			world.y = -world.y;

			Vector2Int map = Vector2Int.zero;
			map.x = Mathf.RoundToInt((world.x / TILE_WIDTH_HALF + world.y / TILE_HEIGHT_HALF) /2);
			map.y = Mathf.RoundToInt((world.y / TILE_HEIGHT_HALF - (world.x / TILE_WIDTH_HALF)) / 2);

			return map;
		}

	}

}