using com.gStudios.isometric.model.characters;

namespace com.gStudios.isometric.model.world.furniture {

	public interface IFurniture {

        int GetSpriteIndex();

        string GetSpriteVariation();

        bool IsWalkable(WalkInfo walkInfo);

        bool IsFurniture();

        void Move(int xOffset, int yOffset);

        // Callbacks

        void OnStandOver(WalkInfo walkInfo);
	}

}