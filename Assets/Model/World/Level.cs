using System.Collections;
using System.Collections.Generic;

namespace com.gStudios.isometric.world {

	public class Level {

		Tile[,] tiles;
		int width;
		int height;

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

	}

}