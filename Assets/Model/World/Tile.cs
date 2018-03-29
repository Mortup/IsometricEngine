using System.Collections;
using System.Collections.Generic;
using System;
using System.Diagnostics;

using com.gStudios.isometric.model.items;

namespace com.gStudios.isometric.model.world {

	public class Tile {

		public enum TileType { Empty, Floor };

		Level level;
		int x;
		int y;
		TileType type = TileType.Empty;

		PlacedFurniture placedFurniture;

		private List<ITileObserver> observers;

		public TileType Type {
			get {
				return type;
			}
			set {
				foreach (ITileObserver tileObserver in observers) {
					tileObserver.NotifyTileTypeChanged (this);
				}

				type = value;
			}
		}

		public int X {
			get {
				return x;
			}
		}

		public int Y {
			get {
				return y;
			}
		}

		public Tile(Level level, int x, int y) {
			this.level = level;
			this.x = x;
			this.y = y;

			observers = new List<ITileObserver> ();
		}

		public void Subscribe(ITileObserver observer) {
			if (observers.Contains (observer))
				UnityEngine.Debug.LogError ("Trying to add an observer more than once.");

			observers.Add (observer);
		}

	}

}