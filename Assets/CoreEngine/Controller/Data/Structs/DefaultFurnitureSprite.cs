using UnityEngine;

using com.gStudios.isometric.model.world.furniture;

namespace com.gStudios.isometric.controller.data.structs {

    public class DefaultFurnitureSprite : IFurnitureSprite {

        Sprite[] sprites;

        public DefaultFurnitureSprite(Sprite[] spritePack) {
            //if (spritePack.Length != 4)
            sprites = spritePack;
        }

        public Sprite GetSprite(IFurniture furniture) {
            return sprites[0];
        }

        public Sprite GetThumbnail() {
            return sprites[0];
        }
    }

}