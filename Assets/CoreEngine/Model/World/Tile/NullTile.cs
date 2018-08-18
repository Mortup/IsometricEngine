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

        public bool IsEmpty() {
            return true;
        }

        public bool IsWalkable() {
            return false;
        }

        public void Subscribe(ITileObserver observer) {
            throw new System.NotImplementedException();
        }
    }

}