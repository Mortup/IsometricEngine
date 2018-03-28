using System.Collections;
using System.Collections.Generic;

using com.gStudios.isometric.items;

namespace com.gStudios.isometric.world {

	public class Tile {

		public enum TileType { Empty, Floor };

		TileType type = TileType.Empty;

		public TileType Type {
			get {
				return type;
			}
			set {
				type = value;
			}
		}

		PlacedFurniture placedFurniture;

		Level level;
		int x;
		int y;

		public Tile(Level level, int x, int y) {
			this.level = level;
			this.x = x;
			this.y = y;
		}

	}

}