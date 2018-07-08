using UnityEngine;

using com.gStudios.isometric.controller.config;

namespace com.gStudios.isometric.controller.isometricTransform {

	public static class TileTransformer {
        // Based on the algorithms found on http://clintbellanger.net/articles/isometric_math/

        /// <summary>
		/// Converts isometric tile coordinates to a world position.
		/// </summary>
		/// <returns>The world position.</returns>
		/// <param name="x">The x isometric coordinate.</param>
		/// <param name="y">The y isometric coordinate.</param>
		public static Vector2 CoordToWorld(int x, int y) {
            Vector2 world = new Vector2(
                (y - x) * Settings.TILE_WIDTH_HALF,
                -(x + y) * Settings.TILE_HEIGHT_HALF);

            Vector2 offset = new Vector2(Settings.TILE_WIDTH_HALF, 0f);
            return world - offset;
        }

        /// <summary>
        /// Converts isometric tile coordinates to a world position.
        /// </summary>
        /// <returns>The world position.</returns>
        /// <param name="coords">The isometric coordinates.</param>
        public static Vector2 CoordToWorld(Vector2Int coords) {
            return CoordToWorld(coords.x, coords.y);
        }

        /// <summary>
        /// Converts world position to isometric tile coordinates.
        /// </summary>
        /// <returns>The tile coordinates.</returns>
        /// <param name="world">The world position.</param>
        public static Vector2Int WorldToCoord(Vector2 world) {
            world.x = -world.x;
            world.y = -world.y;

            Vector2Int map = Vector2Int.zero;
            map.x = Mathf.RoundToInt((world.x / Settings.TILE_WIDTH_HALF + world.y / Settings.TILE_HEIGHT_HALF) / 2);
            map.y = Mathf.RoundToInt((world.y / Settings.TILE_HEIGHT_HALF - (world.x / Settings.TILE_WIDTH_HALF)) / 2);

            return map;
        }

        /// <summary>
        /// Converts screen position to isometric tile coordinates.
        /// </summary>
        /// <returns>The tile coordinates.</returns>
        /// <param name="screen">The screen position</param>
        public static Vector2Int ScreenToCoord(Vector2 screen) {
            Vector2 world = Camera.main.ScreenToWorldPoint(screen);
            return WorldToCoord(world);
        }

    }

}