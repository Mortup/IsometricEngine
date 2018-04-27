using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using com.gStudios.isometric.model.data.structures;

namespace com.gStudios.isometric.controller.data {

	public static class DataManager {

		static TileDataLoader _tileData;

		static bool initialized = false;

		public static void Init() {
			if (initialized)
				Debug.LogError ("Trying to initialize DataManager when it's already initialized.");

			_tileData = new TileDataLoader ();
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

	}
}