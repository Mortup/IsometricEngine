using System.Linq;
using UnityEngine;

using com.gStudios.isometric.controller.config;
using com.gStudios.isometric.model.data.structures;

namespace com.gStudios.isometric.controller.data {

	public class TileSpriteDataLoader {

		Sprite[] sprites;

		public TileSpriteDataLoader() {
			Sprite[] unorderedprites = Resources.LoadAll<Sprite> (GamePaths.TilesSprites);
            sprites = unorderedprites.OrderBy(x => x.name, new TrailingNumberComparer()).ToArray();
		}

		public Sprite GetDataById(int id) {
			if (id >= sprites.Length) {
				Debug.LogError ("Can't find a sprite for tile with ID: " + id.ToString ());
			}

			return sprites [id];
		}

		public Sprite GetDataByTileData(TileData td) {
			if (td.id >= sprites.Length) {
				Debug.LogError ("Can't find a sprite for tile with ID: " + td.id.ToString ());
			}

			return GetDataById (td.id);
		}

        public int GetLength() {
            return sprites.Length;
        }
		
	}

}