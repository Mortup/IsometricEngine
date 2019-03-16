using com.gStudios.isometric.model.world.tile;

namespace com.gStudios.isometric.model.world.furniture {

    public class DecorationFurniture : BaseFurniture {

        public DecorationFurniture (Level level, ITile parent) : base(level, parent) {
        }

        public override int GetSpriteIndex() {
            return 2;
        }

    }
}