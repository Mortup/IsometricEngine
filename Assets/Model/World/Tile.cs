using System.Collections;
using System.Collections.Generic;

using com.gStudios.isometric.model.items;

namespace com.gStudios.isometric.model.world {

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

		public int X {
			get {
				return x;
			}
		}

		int y;

		public int Y {
			get {
				return y;
			}
		}

		public Tile(Level level, int x, int y) {
			this.level = level;
			this.x = x;
			this.y = y;
		}

	}

}