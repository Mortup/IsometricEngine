using com.gStudios.isometric.model.world.tile;

namespace com.gStudios.isometric.model.world.furniture {

	public interface IFurnitureObserver {

        void NotifyFurnitureTypeChanged(ITile tile);

	}
	
}