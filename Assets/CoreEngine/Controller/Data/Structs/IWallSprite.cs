using UnityEngine;

using com.gStudios.isometric.model.world.wall;

namespace com.gStudios.isometric.controller.data.structs {

	public interface IWallSprite {

        Sprite GetSprite(IWall wall, bool isCropped);
        Sprite GetThumbnail();

	}

}