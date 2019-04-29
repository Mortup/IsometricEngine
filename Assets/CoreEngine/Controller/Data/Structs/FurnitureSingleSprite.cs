using UnityEngine;

using com.gStudios.isometric.model.world.furniture;

namespace com.gStudios.isometric.controller.data.structs {

    public class FurnitureSingleSprite : IFurnitureSprite {

        Sprite sprite;

        public FurnitureSingleSprite(Sprite spr) {
            sprite = spr;
        }

        public Sprite GetSprite() {
            return sprite;
        }

        public Sprite GetSprite(IFurniture furniture) {
            return sprite;
        }

        public Sprite GetThumbnail() {
            return sprite;
        }
    }

}
