using com.gStudios.isometric.model.characters;

namespace com.gStudios.isometric.model.world.furniture {

	public class NullFurniture : EmptyCallBacksFurniture {
	
        public override int GetSpriteIndex() {
            return 0;
        }

        public override string GetSpriteVariation() {
            return "";
        }

        public override bool IsFurniture() {
            return false;
        }

        public override bool IsWalkable(WalkInfo walkInfo) {
            return true;
        }

        public override void Move(int xOffset, int yOffset) {
            throw new System.NotImplementedException();
        }
    }
	
}