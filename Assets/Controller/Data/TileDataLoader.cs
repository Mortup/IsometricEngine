using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using com.gStudios.isometric.controller.config;
using com.gStudios.isometric.model.data.structures;

namespace com.gStudios.isometric.controller.data {

	public static class TileDataLoader {

		static bool initialized = false;

		static FloorsContainer floorsContainer;

		public static void Init() {
			TextAsset floorsJson = Resources.Load<TextAsset> (Paths.JsonData ("Floors"));
			floorsContainer = JsonUtility.FromJson<FloorsContainer>(floorsJson.text);
			initialized = true;
		}

		public static List<FloorData> GetFloors() {
			return floorsContainer.data;
		}
		
	}

}