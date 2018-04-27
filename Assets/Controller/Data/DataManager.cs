using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using com.gStudios.isometric.model.data.structures;

namespace com.gStudios.isometric.controller.data {

	public static class DataManager {

		static TileDataLoader _tileData;
		static TileSpriteDataLoader _tileSpriteData;

		static bool initialized = false;

		public static void Init() {
			if (initialized)
				Debug.LogError ("Trying to initialize DataManager when it's already initialized.");

			_tileData = new TileDataLoader ();
			_tileSpriteData = new TileSpriteDataLoader ();

			initialized = true;
		}

		private static void CheckInitialization() {
			if (!initialized)
				Debug.LogError ("Using DataManager before initialization.");
		}

		// GETTERS
		public static TileDataLoader tileData {
			get {
				CheckInitialization ();
				return _tileData;
			}
		}

		public static TileSpriteDataLoader tileSpriteData {
			get {
				CheckInitialization ();
				return _tileSpriteData;
			}
		}

	}
}