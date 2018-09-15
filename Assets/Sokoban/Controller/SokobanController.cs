using UnityEngine;

using com.gStudios.isometric.controller;
using com.gStudios.isometric.model.world;
using com.gStudios.sokoban.model.saving;

namespace com.gStudios.sokoban.controller {

    public class SokobanController : MonoBehaviour, ILevelController {

        private CoreLevelController coreLevelController;

        public void Init(CoreLevelController clc) {
            coreLevelController = clc;

            coreLevelController.LoadLevel(new SokobanLevelSerializer("level3"));
        }

        public void OnLevelInit(Level level) {
            
        }

    }

}