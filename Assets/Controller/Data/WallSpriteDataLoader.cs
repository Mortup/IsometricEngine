using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using com.gStudios.isometric.controller.config;

namespace com.gStudios.isometric.controller.data {

	public class WallSpriteDataLoader {

		Sprite[,,] sprites;

		public WallSpriteDataLoader() {
			Sprite[] rawSprites = Resources.LoadAll<Sprite> (Paths.WallSprites);

			if (rawSprites.Length % 4 != 0) {
				Debug.LogError ("Uneven number of wall sprites.");
			}

			sprites = new Sprite[rawSprites.Length / 4, 2, 2];

			int i = 0;
			foreach(Sprite s in rawSprites) {
				int currentIndex = i / 4;
				int currentOrientation = i % 2;
				int currentClipping = (i % 4) / 2;

				string nameIndex = currentIndex.ToString () + '_' + (currentOrientation + 2*currentClipping).ToString ();
				if (s.name.Contains(nameIndex) == false) {
					Debug.LogError("Unordered wall sprites. Expected name containing: " + nameIndex + ". Recieved: " + s.name);
				}

				sprites [currentIndex, currentOrientation, currentClipping] = s;

				i++;
			}
		}

		public Sprite GetDataById(int id, int orientation, int clipping) {
			return sprites [id, orientation, clipping];
		}
		
	}

}