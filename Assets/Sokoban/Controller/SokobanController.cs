using System.Collections.Generic;

using UnityEngine;

using com.gStudios.isometric.controller;
using com.gStudios.isometric.controller.characters;

using com.gStudios.isometric.model.characters;
using com.gStudios.isometric.model.world;

using com.gStudios.sokoban.model.saving;
using com.gStudios.isometric.model.world.tile;

namespace com.gStudios.sokoban.controller {

    public class SokobanController : MonoBehaviour, ILevelController {

        private CoreLevelController coreLevelController;
        private Level level;

        private int currentLevelIndex = 1;

        private GameObject playerController;
        private SimpleMovementCC cc;
        private FourDirectionsSpriteCC sprCC;

        public void Init(CoreLevelController clc) {
            coreLevelController = clc;

            playerController = new GameObject("Player");
            cc = playerController.AddComponent<SimpleMovementCC>();
            sprCC = playerController.AddComponent<FourDirectionsSpriteCC>();
 
            LoadNextLevel();
       }

        public void OnLevelInit(Level level) {
            this.level = level;

            List<ICharacter> chars = level.GetCharacters();

            if (chars.Count > 1)
                Debug.LogError("Sokoban levels cannot have more than one character.");
            
            cc.Init(chars[0]);
            sprCC.Init(chars[0], "Player");
        }

        public void Update() {
            if (HasWon()) {
                Debug.Log("You Win!");
                LoadNextLevel();
            }

        }

        private bool HasWon() {
            bool hasWon = true;

            for (int x = 0; x < level.Width; x++) {
                for (int y = 0; y < level.Height; y++) {
                    ITile currentTile = level.GetTileAt(x, y);
                    if (currentTile.Type == 2) {
                        if (currentTile.GetPlacedFurniture().GetTag() != "Box") {
                            hasWon = false;
                        }
                    }
                }
            }

            return hasWon;

        }

        private void LoadNextLevel() {
            coreLevelController.LoadLevel(new SokobanLevelSerializer("level" + currentLevelIndex.ToString()));
            currentLevelIndex++;
        }

    }

}