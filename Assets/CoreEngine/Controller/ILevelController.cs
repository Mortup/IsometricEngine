using com.gStudios.isometric.model.world;

namespace com.gStudios.isometric.controller {

	public interface ILevelController {

        void Init(CoreLevelController clc);

        void OnLevelInit(Level level);
		
	}

}