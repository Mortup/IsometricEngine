using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using com.gStudios.isometric.controller.config;
using com.gStudios.isometric.model.data.structures;

namespace com.gStudios.isometric.controller.data {

	public class TileDataLoader {

		FloorsContainer floorsContainer;

		public TileDataLoader() {
			TextAsset floorsJson = Resources.Load<TextAsset> (Paths.JsonData ("Floors"));
			floorsContainer = JsonUtility.FromJson<FloorsContainer>(floorsJson.text);
		}

		public List<TileData> GetData() {
			return floorsContainer.data;
		}

		public TileData GetDataById(int id) {
			return floorsContainer.data [id];
		}
		
	}

}