using com.gStudios.isometric.model.world;
using com.gStudios.isometric.model.world.tile;
using com.gStudios.isometric.model.world.wall;

namespace com.gStudios.isometric.model.saving {

	public interface ILevelSerializer {

        Level LoadLevel();
        void SaveLevel(Level level, ITile[,] tiles, IWall[,,] walls);
        bool ExistsSavedLevel();


    }
	
}