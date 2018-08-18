using UnityEngine;

using com.gStudios.isometric.controller.isometricTransform;

namespace com.gStudios.isometric.controller.spriteObservers {

	public static class SortingOrders {

        public static int FloorOrder(int x, int y, FloorSubLayer layer) {
            Vector2Int rotatedCoords = TileTransformer.RotateCoord(new Vector2Int(x, y));
            return ((rotatedCoords.x + rotatedCoords.y) * 10) + (int)layer;
        }

        public static int WallOrder(int x, int y, int z, TileSubLayer layer) {
            Vector3Int rotatedCoords = WallTransformer.RotateCoord(new Vector3Int(x, y, z));
            return (rotatedCoords.x + rotatedCoords.y) * 20 + ((int)layer * 2) + 1 - rotatedCoords.z;
        }

        public static int TileOrder(int x, int y, TileSubLayer layer) {
            Vector2Int rotatedCoords = TileTransformer.RotateCoord(new Vector2Int(x, y));
            return (rotatedCoords.x + rotatedCoords.y) * 20 + ((int)layer * 2);
        }

    }
	
}