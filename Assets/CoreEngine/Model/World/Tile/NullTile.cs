using com.gStudios.isometric.model.characters;
using com.gStudios.isometric.model.world.furniture;

namespace com.gStudios.isometric.model.world.tile {

    public class NullTile : ITile {
        public int X {
            get {
                throw new System.NotImplementedException();
            }
        }

        public int Y {
            get {
                throw new System.NotImplementedException();
            }
        }

        public int Type {
            get {
                return TileIndex.Empty;
            }
            set {

            }
        }

        public IFurniture GetPlacedFurniture() {
            return new NullFurniture();
        }

        public bool IsEmpty() {
            return true;
        }

        public bool IsWalkable(WalkInfo walkInfo) {
            return false;
        }

        public bool HasFurniture() {
            return false;
        }

        public void PlaceFurniture(IFurniture furniture) {
            return;
        }

        public void RemoveFurniture() {
            return;
        }

        public void Subscribe(ITileObserver observer) {
            throw new System.NotImplementedException();
        }

        public void SubscribeToFurniture(IFurnitureObserver furnitureObserver) {
            throw new System.NotImplementedException();
        }

        public void NotifyFurnitureVariationChanged() {
            throw new System.NotImplementedException();
        }

        public void OnStandOver(WalkInfo walkInfo) {
            return;
        }
    }

}