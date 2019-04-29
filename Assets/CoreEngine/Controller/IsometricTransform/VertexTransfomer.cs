using UnityEngine;

using com.gStudios.isometric.model.world.orientation;
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
            Vector2 coordWorldPos = TileTransformer.CoordToWorld(RemoveRotationOffset(new Vector2Int(x, y)));
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
            return AddRotationOffset(TileTransformer.WorldToCoord(new Vector2(world.x, world.y - Settings.TILE_HEIGHT_HALF)));
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

        public static Vector2Int RotateVertex(Vector2Int original) {
            return AddRotationOffset(TileTransformer.RotateCoord(original));
        }

        public static Vector2Int AddRotationOffset(Vector2Int original) {
            switch (OrientationManager.currentOrientation) {
                case Orientation.West:
                    original.x += 1;
                    break;
                case Orientation.South:
                    original.x += 1;
                    original.y += 1;
                    break;
                case Orientation.East:
                    original.y += 1;
                    break;
            }

            return original;
        }

        public static Vector2Int RemoveRotationOffset(Vector2Int original) {
            switch (OrientationManager.currentOrientation) {
                case Orientation.West:
                    original.x -= 1;
                    break;
                case Orientation.South:
                    original.x -= 1;
                    original.y -= 1;
                    break;
                case Orientation.East:
                    original.y -= 1;
                    break;
            }

            return original;
        }
    }

}