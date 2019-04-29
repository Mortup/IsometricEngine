using com.gStudios.isometric.model.characters;
using com.gStudios.isometric.model.world.orientation;

namespace com.gStudios.isometric.model.world.furniture {

	public class NullFurniture : EmptyCallBacksFurniture {

        public override string GetTag() {
            return "NullFurniture";
        }

        public override int GetSpriteIndex() {
            return 0;
        }

        public override string GetSpriteVariation() {
            return "";
        }

        public override bool IsEmpty() {
            return false;
        }

        public override bool IsWalkable(WalkInfo walkInfo) {
            return true;
        }

        public override void Move(int xOffset, int yOffset) {
            throw new System.NotImplementedException();
        }

        public override Orientation GetOrientation() {
            return Orientation.North;
        }
    }
	
}