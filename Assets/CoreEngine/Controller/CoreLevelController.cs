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
        [SerializeField] bool debugLoadSavedLevel;

		Level level;

		TileSpriteObserver tileSpriteObserver;
		WallSpriteObserver wallSpriteObserver;
        FurnitureSpriteObserver furnitureSpriteObserver;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void OnBeforeSceneLoadRuntimeMethod() {
            DataManager.Init();
        }

        void Start () {

			tileSpriteObserver = new TileSpriteObserver ();
			wallSpriteObserver = new WallSpriteObserver ();
            furnitureSpriteObserver = new FurnitureSpriteObserver();

            LoadLevel(new DefaultLevelSerializer());

            if (debugRandomizeLevel) {
                level.RandomizeTiles();
                level.RandomizeWalls();
            }

            foreach (ILevelController levelController in customControllers) {
                levelController.Init(this);
            }
        }
		
		void Update () {
					}

		public void LoadLevel(ILevelSerializer levelSerializer) {
			tileSpriteObserver.RemoveTiles ();
			wallSpriteObserver.RemoveWalls ();
            furnitureSpriteObserver.RemoveFurniture();

            if (debugLoadSavedLevel) {
                level = levelSerializer.LoadLevel();
            }
            else {
                level = new Level(levelWidth, levelHeight);
            }

            foreach (ILevelController levelController in customControllers) {
                levelController.OnLevelInit(level);
            }

			tileSpriteObserver.BindLevel (level);
			wallSpriteObserver.BindLevel (level);
            furnitureSpriteObserver.BindLevel(level);
		}

        private void OnApplicationQuit() {
            tileSpriteObserver.StopObserving();
            wallSpriteObserver.StopObserving();
            furnitureSpriteObserver.StopObserving();
        }

        // CONTROLLER GETTERS
        public TileSpriteObserver GetTileSpriteObserver() {
			return tileSpriteObserver;
		}

        public WallSpriteObserver GetWallSpriteObserver() {
            return wallSpriteObserver;
        }
		// END CONTROLLER GETTERS
	}

}