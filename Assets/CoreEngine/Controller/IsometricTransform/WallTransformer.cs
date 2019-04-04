using UnityEngine;

using com.gStudios.isometric.controller.config;

namespace com.gStudios.isometric.controller.isometricTransform {

    public static class WallTransformer {
        // Based on the algorithms found on http://clintbellanger.net/articles/isometric_math/

        /// <summary>
        /// Converts wall coordinates into a world position.
        /// </summary>
        /// <param name="rotatedX">The x wall coordinate.</param>
        /// <param name="rotatedY">The y wall coordinate.</param>
        /// <param name="rotatedZ">The z wall coordinate.</param>
        /// <returns>The world position.</returns>
        public static Vector2 CoordToWorld(int x, int y, int z) {
            Vector3Int rotatedCoords = RotateCoord(new Vector3Int(x, y, z));
            int rotatedX = rotatedCoords.x;
            int rotatedY = rotatedCoords.y;
            int rotatedZ = rotatedCoords.z;

            Vector2 world = new Vector2(
                (rotatedY - rotatedX) * Settings.TILE_WIDTH_HALF,
                -(rotatedX + rotatedY) * Settings.TILE_HEIGHT_HALF);
            Vector2 offset = Vector2.zero;

            if (rotatedZ == 0)
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

            return InverseRotateCoord(new Vector3Int(x, y, z));
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

        public static Vector3Int RotateCoord(Vector3Int original) {
            Vector2Int vec2rotated = TileTransformer.RotateCoord(new Vector2Int(original.x, original.y));
            return RotateInsideTile(new Vector3Int(vec2rotated.x, vec2rotated.y, original.z));
        }

        public static Vector3Int RotateInsideTile(Vector3Int original) {

            switch (OrientationManager.currentOrientation) {
                case Orientation.West:
                    if (original.z == 0) {
                        original.z = 1;
                    }
                    else {
                        original.z = 0;
                        original.y += 1;
                    }
                    break;
                case Orientation.South:
                    if (original.z == 0) {
                        original.y += 1;
                    }
                    else {
                        original.x += 1;
                    }
                    break;
                case Orientation.East:
                    if (original.z == 0) {
                        original.z = 1;
                        original.x += 1;
                    }
                    else {
                        original.z = 0;
                    }
                    break;
            }

            return original;
        }

        public static Vector3Int InverseRotateCoord(Vector3Int original) {
            Vector2Int vec2rotated = TileTransformer.InverseRotateCoord(new Vector2Int(original.x, original.y));
            return InverseRotateInsideTile(new Vector3Int(vec2rotated.x, vec2rotated.y, original.z));
        }

        public static Vector3Int InverseRotateInsideTile(Vector3Int original) {
            switch (OrientationManager.currentOrientation) {
                case Orientation.West:
                    if (original.z == 0) {
                        original.z = 1;
                        original.x += 1;
                    }
                    else {
                        original.z = 0;
                    }
                    break;
                case Orientation.South:
                    if (original.z == 0) {
                        original.y += 1;
                    }
                    else {
                        original.x += 1;
                    }
                    break;
                case Orientation.East:
                    if (original.z == 0) {
                        original.z = 1;
                    }
                    else {
                        original.z = 0;
                        original.y += 1;
                    }
                    break;
            }

            return original;
        }

        public static Vector2Int GetRotatedWallBase(Vector3Int wallCoords) {
            switch (OrientationManager.currentOrientation) {
                case Orientation.North:
                    return new Vector2Int(wallCoords.x, wallCoords.y);
                case Orientation.West:
                    if(wallCoords.z == 0) {
                        return new Vector2Int(wallCoords.x, wallCoords.y);
                    }
                    else {
                        return new Vector2Int(wallCoords.x-1  , wallCoords.y);
                    }
                case Orientation.East:
                    if (wallCoords.z == 0) {
                        return new Vector2Int(wallCoords.x, wallCoords.y - 1);
                    }
                    else {
                        return new Vector2Int(wallCoords.x, wallCoords.y);
                    }

                case Orientation.South:
                    if (wallCoords.z == 0) {
                        return new Vector2Int(wallCoords.x, wallCoords.y - 1);
                    }
                    else {
                        return new Vector2Int(wallCoords.x - 1, wallCoords.y);
                    }
    
            }

            return new Vector2Int(wallCoords.x, wallCoords.y);
        }

    }
}