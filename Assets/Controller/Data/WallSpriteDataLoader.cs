using System.IO;
using System.Linq;
using System.Collections.Generic;

using UnityEngine;

using com.gStudios.isometric.controller.config;
using com.gStudios.isometric.controller.data.structs;

namespace com.gStudios.isometric.controller.data {

	public class WallSpriteDataLoader {

        List<WallSprite> sprites;

		public WallSpriteDataLoader() {
            sprites = new List<WallSprite>();

            string wallSpritesFullPath = Path.Combine(GamePaths.ResourcesBase, GamePaths.WallSprites);
            string[] directories = Directory.GetDirectories(wallSpritesFullPath);
            foreach (string folder in directories.OrderBy(x => x, new TrailingNumberComparer())) {
                Sprite[] currentSpritePack = Resources.LoadAll<Sprite>(folder.Remove(0, GamePaths.ResourcesBase.Length + 1));
                sprites.Add(new WallSprite(currentSpritePack));
            }
		}

		public WallSprite GetDataById(int id) {
			return sprites[id];
		}
		
	}

}