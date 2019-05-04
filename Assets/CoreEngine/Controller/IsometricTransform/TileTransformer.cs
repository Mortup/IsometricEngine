using UnityEngine;

using com.gStudios.isometric.model.world.orientation;
using com.gStudios.isometric.controller.config;

namespace com.gStudios.isometric.controller.isometricTransform {

	public static class TileTransformer {
        // Based on the algorithms found on http://clintbellanger.net/articles/isometric_math/

        /// <summary>
		/// Converts isometric tile coordinates to a world position.
		/// </summary>
		/// <returns>The world position.</returns>
		/// <param name="rotatedX">The x isometric coordinate.</param>
		/// <param name="rotatedY">The y isometric coordinate.</param>
		public static Vector2 CoordToWorld(int x, int y) {
            Vector2Int rotatedCoords = RotateCoord(new Vector2Int(x,y));
            int rotatedX = rotatedCoords.x;
            int rotatedY = rotatedCoords.y;

            Vector2 world = new Vector2(
                (rotatedY - rotatedX)  * Settings.TILE_WIDTH_HALF,
                -(rotatedX + rotatedY) * Settings.TILE_HEIGHT_HALF);

            Vector2 offset = new Vector2(Settings.TILE_WIDTH_HALF, 0f);
            return world - offset;
        }

        public static Vector2 CoordToWorld(float x, float y) {
            Vector2 rotatedCoords = RotateCoord(new Vector2(x, y));
            float rotatedX = rotatedCoords.x;
            float rotatedY = rotatedCoords.y;

            Vector2 world = new Vector2(
                (rotatedY - rotatedX) * Settings.TILE_WIDTH_HALF,
                -(rotatedX + rotatedY) * Settings.TILE_HEIGHT_HALF);

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

            Vector2Int coord = new Vector2Int(
                Mathf.RoundToInt((world.x / Settings.TILE_WIDTH_HALF + world.y / Settings.TILE_HEIGHT_HALF) / 2)
                ,Mathf.RoundToInt((world.y / Settings.TILE_HEIGHT_HALF - (world.x / Settings.TILE_WIDTH_HALF)) / 2));

            return InverseRotateCoord(coord);
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

        public static Vector2Int RotateCoord(Vector2Int original) {
            switch (OrientationManager.currentOrientation) {
                case Orientation.North:
                    return original;
                case Orientation.West:
                    return new Vector2Int(original.y, -original.x);
                case Orientation.South:
                    return new Vector2Int(-original.x, -original.y);
                case Orientation.East:
                    return new Vector2Int(-original.y, original.x);
            }

            Debug.Log("Trying to rotate to an unknown orientation.");
            return Vector2Int.zero;
        }

        public static Vector2 RotateCoord(Vector2 original) {
            switch (OrientationManager.currentOrientation) {
                case Orientation.North:
                    return original;
                case Orientation.West:
                    return new Vector2(original.y, -original.x);
                case Orientation.South:
                    return new Vector2(-original.x, -original.y);
                case Orientation.East:
                    return new Vector2(-original.y, original.x);
            }

            Debug.Log("Trying to rotate to an unknown orientation.");
            return Vector2.zero;
        }

        public static Vector2Int InverseRotateCoord(Vector2Int original) {
            switch (OrientationManager.currentOrientation) {
                case Orientation.North:
                    return original;
                case Orientation.West:
                    return new Vector2Int(-original.y, original.x);
                case Orientation.South:
                    return new Vector2Int(-original.x, -original.y);
                case Orientation.East:
                    return new Vector2Int(original.y, -original.x);
            }

            Debug.Log("Trying to rotate to an unknown orientation.");
            return Vector2Int.zero;
        }

        public static Vector2 InverseRotateCoord(Vector2 original) {
            switch (OrientationManager.currentOrientation) {
                case Orientation.North:
                    return original;
                case Orientation.West:
                    return new Vector2(-original.y, original.x);
                case Orientation.South:
                    return new Vector2(-original.x, -original.y);
                case Orientation.East:
                    return new Vector2(original.y, -original.x);
            }

            Debug.Log("Trying to rotate to an unknown orientation.");
            return Vector2.zero;
        }
    }

}