using com.gStudios.isometric.model.characters;

namespace com.gStudios.isometric.model.world.furniture {

    public abstract class EmptyCallBacksFurniture : IFurniture {

        // For sprite management
        public abstract int GetSpriteIndex();
        public abstract string GetSpriteVariation();

        public abstract bool IsFurniture();
        public abstract bool IsWalkable(WalkInfo walkInfo);
        public abstract void Move(int xOffset, int yOffset);

        public virtual void OnStandOver(WalkInfo walkInfo) {

        }
    }

}