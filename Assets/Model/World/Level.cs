using System.Collections;
using System.Collections.Generic;

using com.gStudios.isometric.model.world.tile;
using com.gStudios.isometric.model.world.wall;
using com.gStudios.isometric.model.world.generation;

namespace com.gStudios.isometric.model.world {

	public class Level {

		ITile[,] tiles;
		IWall[,,] walls;

		int width;

		public int Width {
			get {
				return width;
			}
		}

		int height;

		public int Height {
			get {
				return height;
			}
		}

		public Level(int width, int height) {
			this.width = width;
			this.height = height;

			tiles = TileGenerator.Generate(width, height);
			walls = WallGenerator.Generate(width, height);
		}

		public ITile GetTileAt(int x, int y) {
			if (x > width || x < 0 || y > height || y < 0) {
				UnityEngine.Debug.LogError("Tile ("+x+","+y+") is out of range.");
				return null;
			}

			return tiles [x, y];
		}

		public IWall GetWallAt(int x, int y, int z) {
			if (x > width+1 || x < 0 || y > height+1 || y < 0) {
				UnityEngine.Debug.LogError("Wall ("+x+","+y+","+z+") is out of range.");
				return null;
			}
			if (z != 0 && z != 1) {
				UnityEngine.Debug.LogError("Wall ("+x+","+y+","+z+") is out of range.");
				return null;
			}

			return walls [x, y, z];
		}

		public bool IsInBounds(int x, int y) {
			if (x < 0 || y < 0)
				return false;
			if (x >= width || y >= height)
				return false;

			return true;
		}

		public void RandomizeTiles() {
			for (int x = 0; x < width; x++) {
				for (int y = 0; y < height; y++) {
					if (UnityEngine.Random.Range(0,2) == 0) {
						tiles [x, y].Type = 0;
					}
					else {
						tiles [x, y].Type = 1;
					}
				}
			}
		}

		public void RandomizeWalls() {
			for (int x = 0; x < width + 1; x++) {
				for (int y = 0; y < height + 1; y++) {

					walls [x, y, 0].Type = UnityEngine.Random.Range (0, 2) == 0 ? 0 : 1;
					walls [x, y, 1].Type = UnityEngine.Random.Range (0, 2) == 0 ? 0 : 1;

				}
			}
		}

		public ITile[,] GetTilesForSerialization() {
			return tiles;
		}
	}

}