using System.Collections.Generic;

using com.gStudios.isometric.model.characters;
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
        private List<IFurnitureObserver> furnitureObservers;

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
            furnitureObservers = new List<IFurnitureObserver>();
		}

        public bool IsEmpty() {
            return Type == TileIndex.Empty;
        }

        public bool HasFurniture() {
            return placedFurniture.IsFurniture();
        }

        public bool IsWalkable(WalkInfo walkInfo) {
            return (Type != TileIndex.Empty) && placedFurniture.IsWalkable(walkInfo);
        }

		public void Subscribe(ITileObserver observer) {
			if (observers.Contains (observer))
				UnityEngine.Debug.LogError ("Trying to add an observer more than once.");

			observers.Add (observer);
		}

        public void SubscribeToFurniture(IFurnitureObserver furnitureObserver) {
            if (furnitureObservers.Contains(furnitureObserver))
                UnityEngine.Debug.LogError("Trying to add an observer more than once.");

            furnitureObservers.Add(furnitureObserver);
        }

        public IFurniture GetPlacedFurniture() {
            return placedFurniture;
        }

        public void PlaceFurniture(IFurniture furniture) {
            placedFurniture = furniture;

            NotifyFurnitureObservers();
        }

        public void RemoveFurniture() {
            PlaceFurniture(new NullFurniture());
        }

        public void OnStandOver(WalkInfo walkInfo) {
            placedFurniture.OnStandOver(walkInfo);
        }

        public void NotifyFurnitureVariationChanged() {
            NotifyFurnitureObservers();
        }

        private void NotifyFurnitureObservers() {
            foreach (IFurnitureObserver furnitureObserver in furnitureObservers) {
                furnitureObserver.NotifyFurnitureTypeChanged(this);
            }
        }
    }

}