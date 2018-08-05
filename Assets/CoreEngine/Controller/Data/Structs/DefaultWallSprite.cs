using UnityEngine;

using com.gStudios.isometric.controller.isometricTransform;
using com.gStudios.isometric.controller.spriteCreators;
using com.gStudios.isometric.model.world.wall;

namespace com.gStudios.isometric.controller.data.structs {

	public class DefaultWallSprite : IWallSprite {

        const int numberOfSprites = 2;    // Number of sprites on each folder

        Sprite[] sprites;

        public DefaultWallSprite(Sprite[] sprites) {
            if (sprites.Length != numberOfSprites)
                Debug.LogError(string.Format("Couldn't load sprite. Excpected {0} images but received {1}.",
                    numberOfSprites.ToString(),
                    sprites.Length.ToString()));

            this.sprites = sprites;
        }
		
        public Sprite GetSprite(IWall wall, bool isCropped) {
            Vector3Int coords = new Vector3Int(wall.X, wall.Y, wall.Z);
            Vector3Int rotatedCoords = WallTransformer.InverseRotateCoord(coords);

            return WallCreator.DrawSpriteBorders(
                sprites[rotatedCoords.z],
                rotatedCoords.z,
                new InmediateWallNeighbors(wall, OrientationManager.currentOrientation),
                isCropped);
        }

        public Sprite GetThumbnail() {
            return sprites[0];
        }

        private bool HasNeighborShortcut(IWall wall, Vector3Int coords) {
            return wall.GetNeighbor(coords.x, coords.y, coords.z).Type != WallIndex.Empty;
        }
    }

}