using System.Collections.Generic;
using UnityEngine;

using com.gStudios.isometric.model.world.wall;

namespace com.gStudios.isometric.controller.data.structs {

	public class DefaultWallSprite : IWallSprite {

        const int numberOfSprites = 256;    // Number of sprites on each folder

        Sprite[] sprites;

        public DefaultWallSprite(Sprite[] sprites) {
            if (sprites.Length != numberOfSprites)
                Debug.LogError(string.Format("Couldn't load sprite. Excpected {0} images but received {1}.",
                    numberOfSprites.ToString(),
                    sprites.Length.ToString()));

            this.sprites = sprites;
        }
		
        public Sprite GetSprite(IWall wall, bool isCropped) {
            List<bool> conditions = new List<bool>();

            if (wall.Z == 0) {
                conditions.Add(wall.GetNeighbor(-1, 0, 0).Type != WallIndex.EmptyWallIndex);
                conditions.Add(wall.GetNeighbor(0, 0, 1).Type != WallIndex.EmptyWallIndex);
                conditions.Add(wall.GetNeighbor(0, -1, 1).Type != WallIndex.EmptyWallIndex);
                conditions.Add(wall.GetNeighbor(1, 0, 0).Type != WallIndex.EmptyWallIndex);
                conditions.Add(wall.GetNeighbor(1, 0, 1).Type != WallIndex.EmptyWallIndex);
                conditions.Add(wall.GetNeighbor(1, -1, 1).Type != WallIndex.EmptyWallIndex);
            }
            else {
                conditions.Add(wall.GetNeighbor(0, -1, 1).Type != WallIndex.EmptyWallIndex);
                conditions.Add(wall.GetNeighbor(-1, 0, 0).Type != WallIndex.EmptyWallIndex);
                conditions.Add(wall.GetNeighbor(0, 0, 0).Type != WallIndex.EmptyWallIndex);
                conditions.Add(wall.GetNeighbor(0, 1, 1).Type != WallIndex.EmptyWallIndex);
                conditions.Add(wall.GetNeighbor(-1, 1, 0).Type != WallIndex.EmptyWallIndex);
                conditions.Add(wall.GetNeighbor(0, 1, 0).Type != WallIndex.EmptyWallIndex);
            }

            conditions.Add(wall.Z == 1);
            conditions.Add(isCropped);

            int index = 0;
            for (int i = 0; i < conditions.Count; i++) {
                if (conditions[i])
                    index += Mathf.RoundToInt(Mathf.Pow(2, i));
            }
            return sprites[index];
        }
	}

}