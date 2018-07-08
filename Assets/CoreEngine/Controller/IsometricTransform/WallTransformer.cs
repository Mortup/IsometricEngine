using UnityEngine;

using com.gStudios.isometric.controller.config;

namespace com.gStudios.isometric.controller.isometricTransform {

	public static class WallTransformer {
        // Based on the algorithms found on http://clintbellanger.net/articles/isometric_math/

        /// <summary>
        /// Converts wall coordinates into a world position.
        /// </summary>
        /// <param name="x">The x wall coordinate.</param>
        /// <param name="y">The y wall coordinate.</param>
        /// <param name="z">The z wall coordinate.</param>
        /// <returns>The world position.</returns>
        public static Vector2 CoordToWorld(int x, int y, int z) {
            Vector2 world = new Vector2(
                (y - x) * Settings.TILE_WIDTH_HALF,
                -(x + y) * Settings.TILE_HEIGHT_HALF);
            Vector2 offset = Vector2.zero;

            if (z == 0)
                offset += new Vector2(Settings.TILE_WIDTH_HALF, 0f);

            return world - offset;
        }

        /// <summary>
        /// Converts wall coordinates into a world position.
        /// </summary>
        /// <param name="wallPos">The wall coordinates.</param>
        /// <returns>The world position.</returns>
		public static Vector2 CoordToWorld(Vector3Int wallPos) {
            return CoordToWorld(wallPos.x, wallPos.y, wallPos.z);
        }

        /// <summary>
        /// Converts a world position to wall coordinates.
        /// </summary>
        /// <param name="world">The world position.</param>
        /// <returns>The wall coords.</returns>
		public static Vector3Int WorldToCoord(Vector2 world) {
            int gridX = Mathf.FloorToInt(world.x / Settings.TILE_WIDTH_HALF) + 1;
            int gridY = Mathf.FloorToInt(-world.y / Settings.TILE_HEIGHT_HALF) + 1;

            int x = Mathf.CeilToInt((gridY / 2f) - (gridX / 2f));
            int y = Mathf.FloorToInt((gridX / 2f) + (gridY / 2f));
            int z = Mathf.Abs(Mathf.Abs(gridX % 2) - Mathf.Abs(gridY % 2));

            return new Vector3Int(x, y, z);
        }

        /// <summary>
        /// Converts a screen position to a wall position.
        /// </summary>
        /// <returns>The to wall position.</returns>
        /// <param name="screen">The screen position.</param>
        public static Vector3Int ScreenToCoord(Vector2 screen) {
            Vector2 world = Camera.main.ScreenToWorldPoint(screen);
            return WorldToCoord(world);
        }

    }

}