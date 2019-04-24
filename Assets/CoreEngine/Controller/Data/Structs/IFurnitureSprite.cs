using UnityEngine;

using com.gStudios.isometric.model.world.furniture;

namespace com.gStudios.isometric.controller.data.structs {

    public interface IFurnitureSprite {

        Sprite GetSprite(IFurniture furniture);
        Sprite GetThumbnail();

    }

}