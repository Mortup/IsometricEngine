using com.gStudios.isometric.model.characters;
using com.gStudios.isometric.model.world.furniture;

namespace com.gStudios.isometric.model.world.tile {

    public class NullTile : ITile {

        int x;
        int y;

        public NullTile(int x, int y) {
            this.x = x;
            this.y = y;
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