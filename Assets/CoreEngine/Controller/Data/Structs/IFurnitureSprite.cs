using UnityEngine;

using com.gStudios.isometric.model.world.furniture;
using com.gStudios.isometric.model.world.orientation;

namespace com.gStudios.isometric.controller.data.structs {

    public interface IFurnitureSprite {

        Sprite GetSprite(IFurniture furniture);
        Sprite GetSprite(Orientation furnitureOrientation);
        Sprite GetThumbnail();

    }

}