using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using com.gStudios.isometric.model.world.wall;

namespace com.gStudios.isometric.model.world.generation {

	public static class WallGenerator {

		public static IWall[,,] Generate(int levelWidth, int levelHeight) {
			IWall[,,] walls = new IWall[levelWidth+1, levelHeight+1, 2];

			for (int x = 0; x < levelWidth+1; x++) {
				for (int y = 0; y < levelHeight+1; y++) {

					if (x == levelWidth) {
						walls [x, y, 0] = new NullWall (x, y, 0);
					}
					if (y == levelHeight) {
						walls [x, y, 1] = new NullWall (x, y, 1);
					}

					if (walls [x, y, 0] == null)
						walls [x, y, 0] = new RegularWall (x,y,0);

					if (walls [x, y, 1] == null)
						walls [x, y, 1] = new RegularWall (x,y,1);

					if (x == 0 || x == levelWidth)
						walls [x, y, 1].Type = WallIndex.NewWallIndex;
					if (y == 0 || y == levelHeight)
						walls [x, y, 0].Type = WallIndex.NewWallIndex;
				}
			}

			return walls;
		}
		
	}

}