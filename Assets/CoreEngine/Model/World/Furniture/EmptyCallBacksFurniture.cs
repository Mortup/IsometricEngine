using com.gStudios.isometric.model.characters;
using com.gStudios.isometric.model.world.orientation;

namespace com.gStudios.isometric.model.world.furniture {

    public abstract class EmptyCallBacksFurniture : IFurniture {

        // For sprite management
        public abstract string GetTag();

        public abstract int GetIndex();
        public abstract string GetSpriteVariation();

        public abstract bool IsEmpty();
        public abstract bool IsWalkable(WalkInfo walkInfo);
        public abstract void Move(int xOffset, int yOffset);

        public virtual void OnStandOver(WalkInfo walkInfo) {

        }

        public abstract Orientation GetOrientation();
    }

}