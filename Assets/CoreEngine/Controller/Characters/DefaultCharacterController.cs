using UnityEngine;

using com.gStudios.isometric.controller.isometricTransform;
using com.gStudios.isometric.controller.spriteObservers;

using com.gStudios.isometric.model.characters;

namespace com.gStudios.isometric.controller.characters {

	public class DefaultCharacterController : MonoBehaviour {

        private bool initializated = false;

        ICharacter character;
        GameObject charGo;

        public void Init(ICharacter character) {
            this.character = character;

            CreateSprite();

            initializated = true;
        }

        public void CreateSprite() {
            GameObject char_go = new GameObject();
            char_go.name = "Character";
            //char_go.transform.SetParent(tileHolder.transform, true);

            SpriteRenderer sr = char_go.AddComponent<SpriteRenderer>();
            sr.sortingLayerName = "Tiles";
            sr.sprite = Resources.Load<Sprite>("Sprites/Untitled");

            charGo = char_go;
            //tile.Subscribe(this);

            UpdateSprite();
        }

        public void UpdateSprite() {
            charGo.transform.position = TileTransformer.CoordToWorld(character.X, character.Y);
            SpriteRenderer sr = charGo.GetComponent<SpriteRenderer>();
            sr.sortingOrder = SortingOrders.GetSortingOrder(character.X, character.Y, 0, TileSubLayer.Character);
        }

        public void Update() {
            if (!initializated)
                Debug.LogError("Character controllers must be initializated inmediately!");

            if (Input.GetKeyDown(KeyCode.UpArrow)) {
                character.Walk(1, 0);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow)) {
                character.Walk(-1, 0);
            }

            UpdateSprite();
        }

    }

}