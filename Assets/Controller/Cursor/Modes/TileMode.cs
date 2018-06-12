using com.gStudios.isometric.model.world;

namespace com.gStudios.isometric.controller.cursor.modes {

	public abstract class TileMode : DefaultMode{

        public TileMode(Level level) : base(level) {
            mainCursorSr.sortingLayerName = "Floor";
        }

    }

}