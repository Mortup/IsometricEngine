using UnityEngine;

using com.gStudios.isometric.model.world.wall;

namespace com.gStudios.isometric.controller.data.structs {

	public class WallSprite {

        Sprite[] sprites;

        public WallSprite(Sprite[] sprites) {
            this.sprites = sprites;
        }
		
        public Sprite GetSprite(IWall wall) {
            if (wall.Z == 0)
                return sprites[0];

            return sprites[1];
        }
	}

}