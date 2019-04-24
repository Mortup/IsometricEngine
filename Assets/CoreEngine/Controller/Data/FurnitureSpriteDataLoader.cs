using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using com.gStudios.isometric.controller.config;
using com.gStudios.isometric.controller.data.structs;

namespace com.gStudios.isometric.controller.data {

    public class FurnitureSpriteDataLoader {

        List<IFurnitureSprite> sprites;

        public FurnitureSpriteDataLoader() {
            sprites = new List<IFurnitureSprite>();

            Sprite[] resLoadedSprites = Resources.LoadAll<Sprite>(GamePaths.FurnitureSprites);
            List<List<Sprite>> groupedSprites = resLoadedSprites
                .GroupBy(x => x.name.Split('_')[0])
                .Select(grp => grp.ToList())
                .OrderBy(group => group.First().name.Split('_')[0], new TrailingNumberComparer())
                .ToList();

            foreach (List<Sprite> sprGroup in groupedSprites) {
                Sprite[] currentSpritePack = sprGroup.ToArray();

                if (currentSpritePack.Length == 1) {
                    sprites.Add(new FurnitureSingleSprite(currentSpritePack[0]));
                }
                else {
                    sprites.Add(new DefaultFurnitureSprite(currentSpritePack));
                }
            }
        }

        public IFurnitureSprite GetDataById(int id) {
            // TODO: Remove sprites array and implement a pooling system.
            return sprites[id];
        }

        public int GetLength() {
            return sprites.Count;
        }

    }
	
}