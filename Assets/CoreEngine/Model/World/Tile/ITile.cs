using com.gStudios.isometric.model.characters;
using com.gStudios.isometric.model.world.furniture;

namespace com.gStudios.isometric.model.world.tile {

	public interface ITile {

		int X {get;}
		int Y {get;}

		int Type {get; set;}

		void Subscribe (ITileObserver observer);

        void SubscribeToFurniture(IFurnitureObserver furnitureObserver);

        void NotifyFurnitureVariationChanged();

        bool IsEmpty();

        bool HasFurniture();

        bool IsWalkable(WalkInfo walkInfo);

        IFurniture GetPlacedFurniture();

        void PlaceFurniture(IFurniture furniture);

        void RemoveFurniture();

        // Callbacks

        void OnStandOver(WalkInfo walkInfo);
		
	}

}