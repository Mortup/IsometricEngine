using UnityEngine;

using com.gStudios.isometric.controller.isometricTransform;
using com.gStudios.isometric.controller.spriteObservers;
using com.gStudios.isometric.model.saving;
using com.gStudios.isometric.model.world;

namespace com.gStudios.isometric.controller {
    public class DebugController : MonoBehaviour, ILevelController
    {
        CoreLevelController coreLevelController;
        Level level;

        public void Init(CoreLevelController clc) {
            coreLevelController = clc;
        }

        public void OnLevelInit(Level level) {
            this.level = level;
        }

        void Update() {
            // Wall Randomization
            if (Input.GetKeyDown(KeyCode.R)) {
				level.RandomizeWalls ();
			}

            // Wall Clipping
			if (Input.GetKeyDown(KeyCode.P)) {
				coreLevelController.GetWallSpriteObserver().CurrentClipping = WallSpriteObserver.ClippingMode.FULL_CLIPPING;
			}
			if (Input.GetKeyDown(KeyCode.O)) {
				coreLevelController.GetWallSpriteObserver().CurrentClipping = WallSpriteObserver.ClippingMode.NO_CLIP;
			}
			if (Input.GetKeyDown(KeyCode.I)) {
				coreLevelController.GetWallSpriteObserver().CurrentClipping = WallSpriteObserver.ClippingMode.FRONT_CLIPPING;
			}

            // Orientation Rotation
            if (Input.GetKeyDown(KeyCode.E)) {
                OrientationManager.RotateClockwise();
            }
            if (Input.GetKeyDown(KeyCode.Q)) {
                OrientationManager.RotateCounterClockwise();
            }

            // Debug character
            if (Input.GetKeyDown(KeyCode.N)) {
                model.characters.Character characterM = new model.characters.Character(level, 6, 6);

                GameObject character = new GameObject("Character");
                characters.DefaultCharacterController dcc = character.AddComponent<characters.DefaultCharacterController>();
                dcc.Init(characterM);
            }

            // Debug save
            if (Input.GetKeyDown(KeyCode.S)) {
                LevelSerializer ls = new LevelSerializer();
                level.Save(new DefaultLevelSerializer());
            }
        }
    }
}
