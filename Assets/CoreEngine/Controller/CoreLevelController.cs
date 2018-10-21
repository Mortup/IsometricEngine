using UnityEngine;

using com.gStudios.isometric.controller.data;
using com.gStudios.isometric.controller.isometricTransform;
using com.gStudios.isometric.controller.spriteObservers;

using com.gStudios.isometric.model.saving;
using com.gStudios.isometric.model.world;

namespace com.gStudios.isometric.controller {

	public class CoreLevelController : MonoBehaviour {

		[SerializeField] int levelWidth;
		[SerializeField] int levelHeight;
        [SerializeField] MonoBehaviour[] customControllers;

        [SerializeField] bool debugRandomizeLevel;
        [SerializeField] bool debugLoadLevel;

		Level level;

		TileSpriteObserver tileSpriteObserver;
		WallSpriteObserver wallSpriteObserver;
        FurnitureSpriteObserver furnitureSpriteObserver;

		void Start () {
			DataManager.Init ();

			tileSpriteObserver = new TileSpriteObserver ();
			wallSpriteObserver = new WallSpriteObserver ();
            furnitureSpriteObserver = new FurnitureSpriteObserver();

			if (debugLoadLevel) {
                LoadLevel(new DefaultLevelSerializer());
            }

            if (debugRandomizeLevel) {
                level.RandomizeTiles();
                level.RandomizeWalls();
            }

            foreach (ILevelController levelController in customControllers) {
                levelController.Init(this);
            }
        }
		
		void Update () {
			if (Input.GetKeyDown(KeyCode.R)) {
				level.RandomizeWalls ();
			}

			if (Input.GetKeyDown(KeyCode.P)) {
				wallSpriteObserver.SetClipping (true);
			}
			if (Input.GetKeyDown(KeyCode.O)) {
				wallSpriteObserver.SetClipping (false);
			}

            if (Input.GetKeyDown(KeyCode.E)) {
                OrientationManager.RotateClockwise();
            }
            if (Input.GetKeyDown(KeyCode.Q)) {
                OrientationManager.RotateCounterClockwise();
            }
		}

		public void LoadLevel(ILevelSerializer levelSerializer) {
			tileSpriteObserver.RemoveTiles ();
			wallSpriteObserver.RemoveWalls ();
            furnitureSpriteObserver.RemoveFurniture();

            level = levelSerializer.LoadLevel();

            foreach (ILevelController levelController in customControllers) {
                levelController.OnLevelInit(level);
            }

			tileSpriteObserver.BindLevel (level);
			wallSpriteObserver.BindLevel (level);
            furnitureSpriteObserver.BindLevel(level);
		}

		// CONTROLLER GETTERS
		public TileSpriteObserver GetTileSpriteManager() {
			return tileSpriteObserver;
		}
		// END CONTROLLER GETTERS
	}

}