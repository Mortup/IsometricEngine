using com.gStudios.isometric.model.world;
using com.gStudios.isometric.model.world.furniture;
using com.gStudios.isometric.model.world.tile;

namespace com.gStudios.sokoban.model.world {

    public class SokobanWall : BaseFurniture {

        public SokobanWall(Level level, ITile parent) : base(level, parent) {
        }

        public override int GetSpriteIndex() {
            return 2;
        }

    }

}