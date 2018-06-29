using com.gStudios.isometric.model.world.wall;
using UnityEngine;

namespace com.gStudios.isometric.controller.data.structs {

	public class EmptySprite : IWallSprite {

        Sprite sprite;

	    public EmptySprite(Sprite sprite) {
            this.sprite = sprite;
        }

        public Sprite GetSprite(IWall wall, bool isCropped) {
            return sprite;
        }

        public Sprite GetThumbnail() {
            return sprite;
        }
    }

}