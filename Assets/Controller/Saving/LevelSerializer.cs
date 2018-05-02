using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

using com.gStudios.isometric.model.world;
using com.gStudios.isometric.model.data.structures;

using com.gStudios.isometric.controller.spriteObservers;

namespace com.gStudios.isometric.controller.saving {

	public class LevelSerializer {
		TileSpriteObserver tileSpriteObserver;

		const string savesFolder = "Saves";
		const string saveName = "save2.binary";
		const string fullSavePath = savesFolder + "/" + saveName;

		public LevelSerializer(TileSpriteObserver tileSpriteObserver) {
			this.tileSpriteObserver = tileSpriteObserver;
		}

		public Level LoadLevel() {
			if (!File.Exists (fullSavePath))
				return NewLevel ();

			FileStream saveFile = File.Open (fullSavePath, FileMode.Open);
			BinaryFormatter formatter = new BinaryFormatter ();
			string data = (string)formatter.Deserialize (saveFile);
			LevelData levelData = LoadJson (data);

			Level level = new Level (levelData.width, levelData.height);

			for (int x = 0; x < level.Width; x++) {
				for (int y = 0; y < level.Height; y++) {
					level.GetTileAt(x,y).Type = levelData.tiles [x + y * level.Width];
					tileSpriteObserver.CreateSprite (level.GetTileAt(x,y));
				}
			}

			return level;
		}

		public Level NewLevel() {
			Level level = new Level (50, 50);
			level.RandomizeTiles ();

			for (int x = 0; x < level.Width; x++) {
				for (int y = 0; y < level.Height; y++) {
					tileSpriteObserver.CreateSprite (level.GetTileAt(x,y));
				}
			}

			return level;
		}

		public void SaveLevel(Level level) {
			Debug.Log ("Saving level");

			string data = CreateJson (level);

			if (!Directory.Exists (savesFolder))
				Directory.CreateDirectory (savesFolder);

			FileStream saveFile = File.Create (fullSavePath);
			BinaryFormatter formatter = new BinaryFormatter ();
			formatter.Serialize (saveFile, data);
			saveFile.Close ();
		}

		string CreateJson(Level level) {
			LevelData data = new LevelData ();
			data.height = level.Height;
			data.width = level.Width;
			data.tiles = FlattenTileArray (level.GetTilesForSerialization (), level.Height, level.Width);

			return JsonUtility.ToJson (data);
		}

		LevelData LoadJson(string data) {
			LevelData levelData = JsonUtility.FromJson<LevelData>(data);
			return levelData;
		}

		public int[] FlattenTileArray(Tile[,] arr, int width, int height) {
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