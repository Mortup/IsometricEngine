using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using com.gStudios.isometric.model.data.structures;

namespace com.gStudios.isometric.controller.data {

	public static class DataManager {

		static TileSpriteDataLoader _tileSpriteData;
		static WallSpriteDataLoader _wallSpriteData;
		static CursorSpriteDataLoader _cursorSpriteData;

		static bool initialized = false;

		public static void Init() {
			if (initialized)
				Debug.LogError ("Trying to initialize DataManager when it's already initialized.");

			_tileSpriteData = new TileSpriteDataLoader ();
			_wallSpriteData = new WallSpriteDataLoader ();
			_cursorSpriteData = new CursorSpriteDataLoader ();

			initialized = true;
		}

		private static void CheckInitialization() {
			if (!initialized)
				Debug.LogError ("Using DataManager before initialization.");
		}

		// GETTERS
		public static TileSpriteDataLoader tileSpriteData {
			get {
				CheckInitialization ();
				return _tileSpriteData;
			}
		}

		public static WallSpriteDataLoader wallSpriteData {
			get {
				CheckInitialization ();
				return _wallSpriteData;
			}
		}

		public static CursorSpriteDataLoader cursorSpriteData {
			get {
				CheckInitialization ();
				return _cursorSpriteData;
			}
		}
        // END GETTERS

	}
}