using UnityEngine;

using com.gStudios.isometric.controller.characters;
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

        [SerializeField] bool debugLoadLevel;

		Level level;

        LevelSerializer levelSerializer;

		TileSpriteObserver tileSpriteObserver;
		WallSpriteObserver wallSpriteObserver;

		void Start () {
			DataManager.Init ();

			tileSpriteObserver = new TileSpriteObserver ();
			wallSpriteObserver = new WallSpriteObserver ();

			levelSerializer = new LevelSerializer ();
			LoadLevel ();
		}
		
		void Update () {
			if (Input.GetKeyDown(KeyCode.R)) {
				level.RandomizeWalls ();
			}

			if (Input.GetKeyDown(KeyCode.S)) {
                level.Save(levelSerializer);
			}

			if (Input.GetKeyDown(KeyCode.L)) {
				LoadLevel ();
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

            if (Input.GetKeyDown(KeyCode.A)) {
                model.characters.Character character = new model.characters.Character(Random.Range(0, level.Width), Random.Range(0, level.Height));
                level.AddCharacter(character);
                Debug.Log("Agregando personaje");
                GameObject cont = new GameObject();
                DefaultCharacterController cc = cont.AddComponent<DefaultCharacterController>();
                cc.Init(character);
            }
		}

		void LoadLevel() {
			tileSpriteObserver.RemoveTiles ();
			wallSpriteObserver.RemoveWalls ();

            if (debugLoadLevel && levelSerializer.ExistsSavedLevel()) {
                level = levelSerializer.LoadLevel();
            }
            else {
                level = new Level(levelWidth, levelHeight);
            }

            foreach (ILevelController levelController in customControllers) {
                levelController.Init(level);
            }

			tileSpriteObserver.BindLevel (level);
			wallSpriteObserver.BindLevel (level);
		}

		// CONTROLLER GETTERS
		public TileSpriteObserver GetTileSpriteManager() {
			return tileSpriteObserver;
		}
		// END CONTROLLER GETTERS
	}

}