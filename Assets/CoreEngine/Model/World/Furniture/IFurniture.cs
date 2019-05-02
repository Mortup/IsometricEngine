using com.gStudios.isometric.model.characters;
using com.gStudios.isometric.model.world.orientation;

namespace com.gStudios.isometric.model.world.furniture {

	public interface IFurniture {

        string GetTag();

        int GetIndex();

        Orientation GetOrientation();

        string GetSpriteVariation();

        bool IsWalkable(WalkInfo walkInfo);

        bool IsEmpty();

        void Move(int xOffset, int yOffset);

        // Callbacks

        void OnStandOver(WalkInfo walkInfo);
	}

}