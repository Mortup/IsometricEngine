using UnityEngine;

using com.gStudios.isometric.controller.isometricTransform;
using com.gStudios.isometric.controller.spriteObservers;

using com.gStudios.isometric.model.characters;

using com.gStudios.isometric.model.world.tile; // Delete: unused

namespace com.gStudios.isometric.controller.characters {

	public class DefaultCharacterController : MonoBehaviour {

        private float xOffset = 0.05f;
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
            transform.position = TileTransformer.CoordToWorld(character.x + rotatedOffset.x, character.y + rotatedOffset.y);
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            sr.sortingOrder = SortingOrders.TileOrder(character.roundedX, character.roundedY, TileSubLayer.Character);
        }

        public void Update() {
            if (!initializated)
                Debug.LogError("Character controllers must be initializated inmediately!");

            float speed = 5f;
            if (Input.GetKey(KeyCode.UpArrow)) {
                character.Walk(-Time.deltaTime * speed, -Time.deltaTime * speed);
            }
            if (Input.GetKey(KeyCode.DownArrow)) {
                character.Walk(Time.deltaTime * speed, Time.deltaTime * speed);
            }
            if (Input.GetKey(KeyCode.LeftArrow)) {
                character.Walk(Time.deltaTime * speed, -Time.deltaTime * speed);
            }
            if (Input.GetKey(KeyCode.RightArrow)) {
                character.Walk(-Time.deltaTime * speed, Time.deltaTime * speed);
            }

            UpdateSprite();
        }

        private void PaintCurrentTile() {
            int roundedX = character.roundedX;
            int roundedY = character.roundedY;

            for (int i = roundedX - 1; i <= roundedX + 1; i++) {
                for (int j = roundedY - 1; j <= roundedY + 1; j++) {
                    character.Level.GetTileAt(i, j).Type = 1;
                }
            }

            int northX = Mathf.RoundToInt(character.x - character.width);
            if (northX != roundedX) {
                character.Level.GetTileAt(northX, roundedY).Type = 4;
            }
            int southX = Mathf.RoundToInt(character.x + character.width);
            if (southX != roundedX) {
                character.Level.GetTileAt(southX, roundedY).Type = 4;
            }

            int westY = Mathf.RoundToInt(character.y - character.height);
            if (westY != roundedY) {
                character.Level.GetTileAt(roundedX, westY).Type = 4;
            }

            int eastY = Mathf.RoundToInt(character.y + character.height);
            if (eastY != roundedY) {
                character.Level.GetTileAt(roundedX, eastY).Type = 4;
            }

            character.Level.GetTileAt(roundedX, roundedY).Type = 3;

        }

    }

}