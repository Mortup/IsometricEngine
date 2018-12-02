﻿using System.Collections.Generic;

using UnityEngine;

using com.gStudios.isometric.controller;
using com.gStudios.isometric.controller.config;
using com.gStudios.isometric.controller.isometricTransform;

using com.gStudios.isometric.model.characters;
using com.gStudios.isometric.model.world;

using com.gStudios.sokoban.model.saving;
using com.gStudios.sokoban.controller.characters;
using com.gStudios.isometric.model.world.tile;

namespace com.gStudios.sokoban.controller {

    public class SokobanController : MonoBehaviour, ILevelController {

        [SerializeField] private GameObject winScreen;
        [SerializeField] private GameObject lastWinScreen;

        private CoreLevelController coreLevelController;
        private Level level;

        public static int currentLevelIndex = 0;

        private GameObject playerController;
        private SokobanCharMovement cc;
        private SokobanCharSprites sprCC;

        public void Init(CoreLevelController clc) {
            coreLevelController = clc;

            playerController = new GameObject("Player");
            cc = playerController.AddComponent<SokobanCharMovement>();
            sprCC = playerController.AddComponent<SokobanCharSprites>();
 
            LoadNextLevel();
       }

        public void OnLevelInit(Level level) {
            this.level = level;

            List<ICharacter> chars = level.GetCharacters();

            if (chars.Count > 1)
                Debug.LogError("Sokoban levels cannot have more than one character.");

            cc.Init(chars[0], level, sprCC);
            sprCC.Init(chars[0], "Player");

            Vector2Int centerCoords = new Vector2Int(level.Width/2, level.Height/2);
            Vector2 centerPos = TileTransformer.CoordToWorld(centerCoords);
            centerPos.x += Settings.TILE_WIDTH_HALF;

            Camera mainCam = Camera.main;
            mainCam.transform.position = new Vector3(centerPos.x, centerPos.y, mainCam.transform.position.z);
        }

        public void Update() {
            if (HasWon() && winScreen.activeSelf == false) {
                SokoPlayerPrefs.CompleteLevel(currentLevelIndex - 1);

                if (currentLevelIndex == SokobanLevelSerializer.LevelsCount()) {
                    lastWinScreen.SetActive(true);
                }
                else {
                    winScreen.SetActive(true);
                }
                
            }

        }

        public void MovePlayer(string direction) {
            cc.Move(direction);
        }

        public void UndoMovement() {
            cc.Undo();
        }

        public void RestartLevel() {
            LoadCurrentLevel();
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

        public void LoadNextLevel() {
            currentLevelIndex++;
            LoadCurrentLevel();
            Debug.Log("Loading level " + currentLevelIndex.ToString() + "...");
        }

        private void LoadCurrentLevel() {
            coreLevelController.LoadLevel(new SokobanLevelSerializer("level" + currentLevelIndex.ToString()));
        }

    }

}