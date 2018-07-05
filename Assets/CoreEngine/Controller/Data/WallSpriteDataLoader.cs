using System.IO;
using System.Linq;
using System.Collections.Generic;

using UnityEngine;

using com.gStudios.isometric.controller.config;
using com.gStudios.isometric.controller.data.structs;
using com.gStudios.isometric.model.world.wall;

namespace com.gStudios.isometric.controller.data {

	public class WallSpriteDataLoader {

        List<IWallSprite> sprites;

		public WallSpriteDataLoader() {
            sprites = new List<IWallSprite>();

            string wallSpritesFullPath = Path.Combine(GamePaths.ResourcesBase, GamePaths.WallSprites);
            string[] directories = Directory.GetDirectories(wallSpritesFullPath);
            foreach (string folder in directories.OrderBy(x => x, new TrailingNumberComparer())) {
                Sprite[] currentSpritePack = Resources.LoadAll<Sprite>(folder.Remove(0, GamePaths.ResourcesBase.Length + 1));

                if (sprites.Count == WallIndex.EmptyWallIndex)
                    sprites.Add(new EmptySprite(currentSpritePack[0]));
                else
                    sprites.Add(new DefaultWallSprite(currentSpritePack));
            }
		}

		public IWallSprite GetDataById(int id) {
			return sprites[id];
		}

        public int GetLength() {
            return sprites.Count;
        }
		
	}

}