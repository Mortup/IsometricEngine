using UnityEngine;

using com.gStudios.isometric.controller.isometricTransform;
using com.gStudios.isometric.controller.spriteObservers;

using com.gStudios.isometric.model.characters;

namespace com.gStudios.isometric.controller.characters {

	public class DefaultCharacterController : MonoBehaviour {

        private float xOffset = 0.15f;
        private float yOffset = 0.35f;

        private bool initializated = false;

        ICharacter character;

        public void Init(ICharacter character) {
            this.character = character;

            CreateSprite();

            initializated = true;
        }

        public void CreateSprite() {
            SpriteRenderer sr = gameObject.AddComponent<SpriteRenderer>();
            sr.sortingLayerName = "Tiles";
            sr.sprite = Resources.Load<Sprite>("Sprites/Untitled");

            UpdateSprite();
        }

        public void UpdateSprite() {
            Vector2 rotatedOffset = TileTransformer.InverseRotateCoord(new Vector2(xOffset, yOffset));
            transform.position = TileTransformer.CoordToWorld(character.X + rotatedOffset.x, character.Y + rotatedOffset.y);
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            sr.sortingOrder = SortingOrders.TileOrder(character.roundedX, character.roundedY, TileSubLayer.Character);
        }

        public void Update() {
            if (!initializated)
                Debug.LogError("Character controllers must be initializated inmediately!");

            if (Input.GetKey(KeyCode.UpArrow)) {
                character.Walk(-Time.deltaTime, 0);
            }
            if (Input.GetKey(KeyCode.DownArrow)) {
                character.Walk(Time.deltaTime, 0);
            }
            if (Input.GetKey(KeyCode.LeftArrow)) {
                character.Walk(0, -Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.RightArrow)) {
                character.Walk(0, Time.deltaTime);
            }

            UpdateSprite();

            PaintCurrentTile();
        }

        int previousX = 0;
        int previousY = 0;

        private void PaintCurrentTile() {
            int roundedX = character.roundedX;
            int roundedY = character.roundedY;

            character.Level.GetTileAt(roundedX, roundedY).Type = 3;
            if (previousX != roundedX || previousY != roundedY)
                character.Level.GetTileAt(previousX, previousY).Type = 1;

            previousX = roundedX;
            previousY = roundedY;
        }

    }

}