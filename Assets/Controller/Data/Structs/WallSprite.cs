using System.Collections.Generic;
using UnityEngine;

using com.gStudios.isometric.model.world.wall;

namespace com.gStudios.isometric.controller.data.structs {

	public class WallSprite {

        Sprite[] sprites;

        public WallSprite(Sprite[] sprites) {
            this.sprites = sprites;
        }
		
        public Sprite GetSprite(IWall wall) {
            // TODO: Create a new class to check this, so I can check the # of sprites on the constructor too.
            if (wall.Type == WallIndex.EmptyWallIndex)
                return sprites[0];

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
                conditions.Add(wall.GetNeighbor(-1, 1, 0).Type != WallIndex.EmptyWallIndex);
                conditions.Add(wall.GetNeighbor(0, 1, 1).Type != WallIndex.EmptyWallIndex);
                conditions.Add(wall.GetNeighbor(0, 1, 0).Type != WallIndex.EmptyWallIndex);
            }

            conditions.Add(wall.Z == 1);

            int index = 0;
            for (int i = 0; i < conditions.Count; i++) {
                if (conditions[i])
                    index += Mathf.RoundToInt(Mathf.Pow(2, i));
            }
            return sprites[index];
        }
	}

}