using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using com.gStudios.isometric.model.world;
using com.gStudios.isometric.model.world.tile;
using com.gStudios.isometric.model.world.wall;

namespace com.gStudios.isometric.model.saving {

	public class LevelSerializer {
		const string savesFolder = "Saves";
		const string saveName = "save4.binary";
		const string fullSavePath = savesFolder + "/" + saveName;

		public LevelSerializer() {
		}

		public bool ExistsSavedLevel() {
			return File.Exists (fullSavePath);
		}

		public Level LoadLevel() {
			if (!ExistsSavedLevel())
				UnityEngine.Debug.LogError ("Trying to load an unexisting level.");

			FileStream saveFile = File.Open (fullSavePath, FileMode.Open);
			BinaryFormatter formatter = new BinaryFormatter ();
			LevelData levelData = (LevelData)formatter.Deserialize (saveFile);

			Level level = new Level (levelData.width, levelData.height);

			for (int x = 0; x < level.Width; x++) {
				for (int y = 0; y < level.Height; y++) {
					level.GetTileAt(x,y).Type = levelData.tiles [x + y * level.Width];
				}
			}

			for (int x = 0; x < level.Width+1; x++) {
				for (int y = 0; y < level.Height+1; y++) {
                    for (int z = 0; z < 2; z++) {
                        level.GetWallAt(x, y, z).Type = levelData.wallIndexes[Flattened3dIndex(x, y, z, level.Width + 1, level.Height + 1)];
                    }
                }
			}

            saveFile.Close();
			return level;
		}

		public void SaveLevel(Level level, ITile[,] tiles, IWall[,,] walls) {
			UnityEngine.Debug.Log ("Saving level");

			LevelData data = SerializeLevel (level, tiles, walls);

			if (!Directory.Exists (savesFolder))
				Directory.CreateDirectory (savesFolder);

			FileStream saveFile = File.Create (fullSavePath);
			BinaryFormatter formatter = new BinaryFormatter ();
			formatter.Serialize (saveFile, data);
			saveFile.Close ();
		}

		LevelData SerializeLevel(Level level, ITile[,] tiles, IWall[,,] walls) {
            LevelData data = new LevelData {
                height = level.Height,
                width = level.Width,
                tiles = FlattenTileArray(tiles),
                wallIndexes = FlattenWallsArray(walls)
            };

			return data;
		}

        int[] FlattenTileArray(ITile[,] arr) {
            int width = arr.GetLength(0);
            int height = arr.GetLength(1);

            int[] data = new int[width * height];

            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    data[x + y * width] = arr[x, y].Type;
                }
            }

            return data;
        }

        int[] FlattenWallsArray(IWall[,,] arr) {
            int width = arr.GetLength(0);
            int height = arr.GetLength(1);
            int depth = arr.GetLength(2);

            int[] data = new int[width * height * depth];

            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    for (int z = 0; z < depth; z++) {
                        data[Flattened3dIndex(x, y, z, width, height)] = arr[x, y, z].Type;
                    }
                }
            }

            return data;
        }

        int Flattened3dIndex(int x, int y, int z, int width, int height) {
            return x + y * width + z * width * height;
        }

    }

}