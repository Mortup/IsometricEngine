using UnityEngine;

using com.gStudios.isometric.controller.isometricTransform;

using com.gStudios.isometric.model.characters;
using com.gStudios.isometric.model.world;

namespace com.gStudios.isometric.controller.characters {

	public class SimpleMovementCC : MonoBehaviour {

        private bool initializated = false;

        protected Level level;
        protected ICharacter character;

        public virtual void Init(ICharacter character, Level level) {
            this.character = character;
            this.level = level;

            initializated = true;

            UpdatePosition();
        }

        public void UpdatePosition() {
            gameObject.transform.position = TileTransformer.CoordToWorld(character.X, character.Y);
        }

        public virtual void Update() {
            if (!initializated)
                Debug.LogError("Character controllers must be initializated inmediately!");

            if (Input.GetKeyDown(KeyCode.UpArrow)) {
                character.Walk(-1, 0);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow)) {
                character.Walk(1, 0);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                character.Walk(0, -1);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow)) {
                character.Walk(0, 1);
            }

            UpdatePosition();
        }

    }

}