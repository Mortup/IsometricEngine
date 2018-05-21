using System.Collections;
using System.Collections.Generic;

namespace com.gStudios.isometric.model.world {

	public class Level {

		Tile[,] tiles;
		Wall[,,] walls;

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

			// Init tiles
			tiles = new Tile[width,height];

			for (int x = 0; x < width; x++) {
				for (int y = 0; y < height; y++) {
					tiles [x, y] = new Tile (x, y);
				}
			}

			// Init walls
			walls = new Wall[width+1, height+1, 2];

			for (int x = 0; x < width+1; x++) {
				for (int y = 0; y < height+1; y++) {

					walls [x, y, 0] = new Wall (x,y,0);
					walls [x, y, 1] = new Wall (x,y,1);

				}
			}

		}

		public Tile GetTileAt(int x, int y) {
			if (x > width || x < 0 || y > height || y < 0) {
				UnityEngine.Debug.LogError("Tile ("+x+","+y+") is out of range.");
				return null;
			}

			return tiles [x, y];
		}

		public Wall GetWallAt(int x, int y, int z) {
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

		public Tile[,] GetTilesForSerialization() {
			return tiles;
		}
	}

}