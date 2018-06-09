using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using com.gStudios.isometric.model.world;
using com.gStudios.isometric.model.world.tile;

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
					level.GetWallAt (x, y, 0).Type = 0; // TODO: Load the type
					level.GetWallAt (x, y, 1).Type = 0; // TODO: Load the type
				}
			}

			return level;
		}

		public void SaveLevel(Level level, ITile[,] tiles) {
			UnityEngine.Debug.Log ("Saving level");

			LevelData data = SerializeLevel (level, tiles);

			if (!Directory.Exists (savesFolder))
				Directory.CreateDirectory (savesFolder);

			FileStream saveFile = File.Create (fullSavePath);
			BinaryFormatter formatter = new BinaryFormatter ();
			formatter.Serialize (saveFile, data);
			saveFile.Close ();
		}

		LevelData SerializeLevel(Level level, ITile[,] tiles) {
			LevelData data = new LevelData ();
			data.height = level.Height;
			data.width = level.Width;
			data.tiles = FlattenTileArray (tiles, level.Height, level.Width);

			return data;
		}

		public int[] FlattenTileArray(ITile[,] arr, int width, int height) {
			int[] data = new int[width * height];

			for (int x = 0; x < width; x++) {
				for (int y = 0; y < height; y++) {
					data [x + y * width] = arr [x, y].Type;
				}
			}

			return data;
		}
	}

}