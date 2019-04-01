using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using com.gStudios.isometric.model.world.tile;

namespace com.gStudios.isometric.model.world.generation {

	/// <summary>
	/// Creates tiles for empty levels.
	/// </summary>
	public static class TileGenerator {

		/// <summary>
		/// Generates an 2D array of ITile with empty tiles.
		/// </summary>
		/// <param name="levelWidth">Level width.</param>
		/// <param name="levelHeight">Level height.</param>
		public static ITile[,] Generate(Level level, int levelWidth, int levelHeight) {
			ITile[,] tiles = new ITile[levelWidth,levelHeight];

			for (int x = 0; x < levelWidth; x++) {
				for (int y = 0; y < levelHeight; y++) {
                    ITile tile = new RegularTile(x, y);
                    tile.Subscribe(level);
                    tile.SubscribeToFurniture(level);

                    tiles[x, y] = tile;
				}
			}

			return tiles;
		}
		
	}

}