using System.Collections;
using System.Collections.Generic;

using com.gStudios.isometric.model.saving;
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
			walls = WallGenerator.Generate(this, width, height);

			RandomizeTiles ();
            RandomizeWalls();
		}

		public bool IsTileInBounds(int x, int y) {
			return (x < width && x >= 0 && y < height && y >= 0);
		}

		public ITile GetTileAt(int x, int y) {
			if (!IsTileInBounds(x, y)) {
				UnityEngine.Debug.LogError("Tile ("+x+","+y+") is out of range.");
				return null;
			}

			return tiles [x, y];
		}

		public bool IsWallInBounds(int x, int y, int z) {
            if (z != 0 && z != 1)
                return false;

            if (x < 0 || y < 0)
                return false;

            if (x > width || y > height)
                return false;
            return true;
		}

        public bool IsVertexInBounds(int x, int y) {
            return IsWallInBounds(x, y, 0);
        }

		public IWall GetWallAt(int x, int y, int z) {
			if (!IsWallInBounds(x,y,z)) {
				UnityEngine.Debug.LogError("Wall ("+x+","+y+","+z+") is out of range.");
				return null;
			}
			if (z != 0 && z != 1) {
				UnityEngine.Debug.LogError("Wall ("+x+","+y+","+z+") is out of range.");
				return null;
			}

			return walls [x, y, z];
		}

        public void Save(LevelSerializer levelSerializer) {
            levelSerializer.SaveLevel(this, tiles, walls);
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
	}

}