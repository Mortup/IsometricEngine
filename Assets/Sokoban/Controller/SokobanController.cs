using System.Collections.Generic;

using UnityEngine;

using com.gStudios.isometric.controller;
using com.gStudios.isometric.controller.characters;

using com.gStudios.isometric.model.characters;
using com.gStudios.isometric.model.world;

using com.gStudios.sokoban.model.saving;

namespace com.gStudios.sokoban.controller {

    public class SokobanController : MonoBehaviour, ILevelController {

        private CoreLevelController coreLevelController;

        public void Init(CoreLevelController clc) {
            coreLevelController = clc;

            coreLevelController.LoadLevel(new SokobanLevelSerializer("level2"));
        }

        public void OnLevelInit(Level level) {
            List<ICharacter> chars = level.GetCharacters();

            if (chars.Count > 1)
                Debug.LogError("Sokoban levels cannot have more than one character.");

            GameObject playerController = new GameObject("So what is this?");
            SimpleMovementCC cc = playerController.AddComponent<SimpleMovementCC>();
            cc.Init(chars[0]);
            FourDirectionsSpriteCC sprCC = playerController.AddComponent<FourDirectionsSpriteCC>();
            sprCC.Init(chars[0], "Player");
        }

    }

}