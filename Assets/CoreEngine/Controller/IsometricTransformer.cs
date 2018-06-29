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
			Vector2 offset = new Vector2 (TILE_WIDTH_HALF, 0f);
			return world - offset;
		}

		/// <summary>
		/// Converts isometric tile coordinates to a world position.
		/// </summary>
		/// <returns>The world position.</returns>
		/// <param name="coords">The isometric coordinates.</param>
		public static Vector2 CoordToWorld(Vector2Int coords) {
			return CoordToWorld (coords.x, coords.y);
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
			map.x = Mathf.RoundToInt((world.x / TILE_WIDTH_HALF + world.y / TILE_HEIGHT_HALF) /2);
			map.y = Mathf.RoundToInt((world.y / TILE_HEIGHT_HALF - (world.x / TILE_WIDTH_HALF)) / 2);

			return map;
		}

		/// <summary>
		/// Converts screen position to isometric tile coordinates.
		/// </summary>
		/// <returns>The tile coordinates.</returns>
		/// <param name="screen">The screen position</param>
		public static Vector2Int ScreenToCoord(Vector2 screen) {
			Vector2 world = Camera.main.ScreenToWorldPoint (screen);
			return WorldToCoord (world);
		}

		public static Vector2 WallPosToWorld(int x, int y, int z) {
			Vector2 world = new Vector2 ((y - x) * TILE_WIDTH_HALF, -(x + y) * TILE_HEIGHT_HALF);
			Vector2 offset = Vector2.zero;

			if (z == 0)
				offset += new Vector2 (TILE_WIDTH_HALF, 0f);

			return world - offset;
		}

		public static Vector2 WallPosToWorld(Vector3Int wallPos) {
			return WallPosToWorld (wallPos.x, wallPos.y, wallPos.z);
		}

		public static Vector3Int WorldToWallPos(Vector2 world) {			
			int gridX = Mathf.FloorToInt(world.x / TILE_WIDTH_HALF) + 1;
			int gridY = Mathf.FloorToInt(-world.y / TILE_HEIGHT_HALF) + 1;

			int x = Mathf.CeilToInt ((gridY / 2f) - (gridX / 2f));
			int y = Mathf.FloorToInt((gridX / 2f) + (gridY / 2f));
			int z = Mathf.Abs (Mathf.Abs(gridX % 2) - Mathf.Abs(gridY % 2));

			return new Vector3Int(x,y,z);
		}

		/// <summary>
		/// Converts screen position to a wall position.
		/// </summary>
		/// <returns>The to wall position.</returns>
		/// <param name="screen">The screen position.</param>
		public static Vector3Int ScreenToWallPos(Vector2 screen) {
			Vector2 world = Camera.main.ScreenToWorldPoint (screen);
			return WorldToWallPos (world);
		}

        public static Vector2 VertexToWorld(int x, int y) {
            Vector2 coordWorldPos = CoordToWorld(x, y);
            return coordWorldPos + new Vector2(TILE_WIDTH_HALF, TILE_HEIGHT_HALF);
        }

        public static Vector2 VertexToWorld(Vector2Int vertex) {
            return VertexToWorld(vertex.x, vertex.y);
        }

        public static Vector2Int WorldToVertex(Vector2 world) {
            return WorldToCoord(new Vector2(world.x, world.y - TILE_HEIGHT_HALF));
        }

        public static Vector2Int ScreenToVertex(Vector2 screen) {
            Vector2 world = Camera.main.ScreenToWorldPoint(screen);
            return WorldToVertex(world);
        }
	}

}