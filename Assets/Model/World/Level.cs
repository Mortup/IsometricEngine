using System.Collections;
using System.Collections.Generic;

namespace com.gStudios.isometric.model.world {

	public class Level {

		Tile[,] tiles;
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

			tiles = new Tile[width,height];

			for (int x = 0; x < width; x++) {
				for (int y = 0; y < height; y++) {
					tiles [x, y] = new Tile (this, x, y);
				}
			}

			UnityEngine.Debug.Log ("World created with " + (width*height) + " tiles.");
		}

		public Tile GetTileAt(int x, int y) {
			if (x > width || x < 0 || y > height || y < 0) {
				UnityEngine.Debug.LogError("Tile ("+x+","+y+") is out of range.");
				return null;
			}

			return tiles [x, y];
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
	}

}