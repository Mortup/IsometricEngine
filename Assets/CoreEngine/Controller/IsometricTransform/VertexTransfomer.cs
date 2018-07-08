using UnityEngine;

using com.gStudios.isometric.controller.config;

namespace com.gStudios.isometric.controller.isometricTransform {

	public static class VertexTransfomer {
        // Based on the algorithms found on http://clintbellanger.net/articles/isometric_math/

        /// <summary>
        /// Converts a vertex coordinate into a world position.
        /// </summary>
        /// <param name="x">The x vertex coordinate.</param>
        /// <param name="y">The y vertex coordinate.</param>
        /// <returns>The world position.</returns>
        public static Vector2 VertexToWorld(int x, int y) {
            Vector2 coordWorldPos = TileTransformer.CoordToWorld(x, y);
            return coordWorldPos + new Vector2(Settings.TILE_WIDTH_HALF, Settings.TILE_HEIGHT_HALF);
        }

        /// <summary>
        /// Converts a vertex coordinate into a world position.
        /// </summary>
        /// <param name="vertex">The vertex coordinates.</param>
        /// <returns>The world position.</returns>
        public static Vector2 VertexToWorld(Vector2Int vertex) {
            return VertexToWorld(vertex.x, vertex.y);
        }

        /// <summary>
        /// Converts a world position into a vertex position.
        /// </summary>
        /// <param name="world">The world position.</param>
        /// <returns>The vertex position.</returns>
        public static Vector2Int WorldToVertex(Vector2 world) {
            return TileTransformer.WorldToCoord(new Vector2(world.x, world.y - Settings.TILE_HEIGHT_HALF));
        }

        /// <summary>
        /// Converts a screen position into a vertex position.
        /// </summary>
        /// <param name="screen">The screen position.</param>
        /// <returns>The vertex position.</returns>
        public static Vector2Int ScreenToVertex(Vector2 screen) {
            Vector2 world = Camera.main.ScreenToWorldPoint(screen);
            return WorldToVertex(world);
        }

    }

}