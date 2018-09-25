using System.Linq;

using UnityEngine;

using com.gStudios.isometric.controller.config;

namespace com.gStudios.isometric.controller.data {

    public class FurnitureSpriteDataManager {

        Sprite[] sprites;

        public FurnitureSpriteDataManager() {
            Sprite[] unorderedprites = Resources.LoadAll<Sprite>(GamePaths.FurnitureSprites);
            sprites = unorderedprites.OrderBy(x => x.name, new TrailingNumberComparer()).ToArray();
        }

        public Sprite GetDataById(int id, string variation) {
            // TODO: Remove sprites array and implement a pooling system.

            string path;
            if (variation == "") {
                path = GamePaths.FurnitureSprite(id.ToString());
            }
            else {
                path = GamePaths.FurnitureSprite(id.ToString(), variation);
            }

            Sprite spr = Resources.Load<Sprite>(path);

            if (spr == null) {
                Debug.LogError("Can't find a sprite for tile with ID: " + id.ToString() + "and Variation: " + variation);
            }

            return spr;
        }

        public int GetLength() {
            return sprites.Length;
        }

    }
	
}