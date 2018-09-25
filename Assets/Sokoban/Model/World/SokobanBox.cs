using com.gStudios.isometric.model.characters;
using com.gStudios.isometric.model.world;
using com.gStudios.isometric.model.world.furniture;
using com.gStudios.isometric.model.world.tile;

namespace com.gStudios.sokoban.model.world {

    public class SokobanBox : BaseFurniture {

        public SokobanBox(Level level, ITile parent) : base(level, parent) {
            UpdateVariation();
        }

        public override int GetSpriteIndex() {
            return 1;
        }

        public override bool IsWalkable(WalkInfo walkInfo) {
            ITile boxDestinationTile = level.GetTileAt(parent.X + walkInfo.xDirection, parent.Y + walkInfo.yDirection);

            if (boxDestinationTile.IsEmpty())
                return false;
            if (boxDestinationTile.HasFurniture())
                return false;

            return true;
        }

        public override void OnStandOver(WalkInfo walkInfo) {

            Move(walkInfo.xDirection, walkInfo.yDirection);
            UpdateVariation();
        }

        private void UpdateVariation() {
            if (parent.Type == 2)
                spriteVariation = "placed";
            else
                spriteVariation = "";
        }
    }

}