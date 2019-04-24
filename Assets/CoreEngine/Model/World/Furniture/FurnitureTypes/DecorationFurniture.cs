using com.gStudios.isometric.model.world.tile;

namespace com.gStudios.isometric.model.world.furniture {

    public class DecorationFurniture : BaseFurniture {

        public DecorationFurniture (int index, Level level, ITile parent) : base(index, level, parent) {
        }

        public override int GetSpriteIndex() {
            return this.index;
        }

    }
}