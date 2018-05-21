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
		WallSpriteObserver wallSpriteObserver;

		const string savesFolder = "Saves";
		const string saveName = "save3.binary";
		const string fullSavePath = savesFolder + "/" + saveName;

		public LevelSerializer(TileSpriteObserver tileSpriteObserver, WallSpriteObserver wallSpriteObserver) {
			this.tileSpriteObserver = tileSpriteObserver;
			this.wallSpriteObserver = wallSpriteObserver;
		}

		public Level LoadLevel() {
			if (!File.Exists (fullSavePath))
				return NewLevel ();

			FileStream saveFile = File.Open (fullSavePath, FileMode.Open);
			BinaryFormatter formatter = new BinaryFormatter ();
			LevelData levelData = (LevelData)formatter.Deserialize (saveFile);

			Level level = new Level (levelData.width, levelData.height);

			for (int x = 0; x < level.Width; x++) {
				for (int y = 0; y < level.Height; y++) {
					level.GetTileAt(x,y).Type = levelData.tiles [x + y * level.Width];
					tileSpriteObserver.CreateSprite (level.GetTileAt(x,y));
				}
			}

			for (int x = 0; x < level.Width+1; x++) {
				for (int y = 0; y < level.Height+1; y++) {
					level.GetWallAt (x, y, 0).Type = 0; // TODO: Load the type
					level.GetWallAt (x, y, 1).Type = 0; // TODO: Load the type
					wallSpriteObserver.CreateSprite(level.GetWallAt(x,y,0));
					wallSpriteObserver.CreateSprite(level.GetWallAt(x,y,1));
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

			LevelData data = SerializeLevel (level);

			if (!Directory.Exists (savesFolder))
				Directory.CreateDirectory (savesFolder);

			FileStream saveFile = File.Create (fullSavePath);
			BinaryFormatter formatter = new BinaryFormatter ();
			formatter.Serialize (saveFile, data);
			saveFile.Close ();
		}

		LevelData SerializeLevel(Level level) {
			LevelData data = new LevelData ();
			data.height = level.Height;
			data.width = level.Width;
			data.tiles = FlattenTileArray (level.GetTilesForSerialization (), level.Height, level.Width);

			return data;
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