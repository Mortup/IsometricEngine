﻿using System.Collections;
using System.Collections.Generic;
using System;
using System.Diagnostics;

using com.gStudios.isometric.model.world.furniture;

namespace com.gStudios.isometric.model.world.tile {

	public class RegularTile : ITile {

		public const int EmptyTileIndex = 0;
		public const int NewTileIndex = 1;

		int x;
		int y;
		int type = EmptyTileIndex;

		IFurniture placedFurniture;

		private List<ITileObserver> observers;

		public int Type {
			get {
				return type;
			}
			set {
				type = value;

				foreach (ITileObserver tileObserver in observers) {
					tileObserver.NotifyTileTypeChanged (this);
				}
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

		public RegularTile(int x, int y) {
			this.x = x;
			this.y = y;

            placedFurniture = new NullFurniture();

			observers = new List<ITileObserver> ();
		}

        public bool IsEmpty() {
            return Type == TileIndex.Empty;
        }

        public bool IsWalkable() {
            return Type != TileIndex.Empty;
        }

		public void Subscribe(ITileObserver observer) {
			if (observers.Contains (observer))
				UnityEngine.Debug.LogError ("Trying to add an observer more than once.");

			observers.Add (observer);
		}

	}

}