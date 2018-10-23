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

            Sprite[] resLoadedSprites = Resources.LoadAll<Sprite>(GamePaths.WallSprites);
            List<List<Sprite>> groupedSprites = resLoadedSprites
                .GroupBy(x => x.name.Split('_')[1])
                .Select(grp => grp.ToList())
                .OrderBy(group => group.First().name.Split('_')[1], new TrailingNumberComparer())
                .ToList();
            
            foreach (List<Sprite> sprGroup in groupedSprites) {
                Sprite[] currentSpritePack = sprGroup.ToArray();

                if (sprites.Count == WallIndex.Empty)
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