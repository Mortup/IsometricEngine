using UnityEngine;

using com.gStudios.isometric.model.world.furniture;
using com.gStudios.isometric.model.world.orientation;

namespace com.gStudios.isometric.controller.data.structs {

    public class FurnitureSingleSprite : IFurnitureSprite {

        Sprite sprite;

        public FurnitureSingleSprite(Sprite spr) {
            sprite = spr;
        }

        public Sprite GetSprite(Orientation orientation) {
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
