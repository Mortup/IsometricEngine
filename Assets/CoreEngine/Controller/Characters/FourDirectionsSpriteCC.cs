using UnityEngine;

using com.gStudios.isometric.model.characters;
using com.gStudios.isometric.controller.spriteObservers;

namespace com.gStudios.isometric.controller.characters {

	public class FourDirectionsSpriteCC : MonoBehaviour {

        ICharacter character;
        SpriteRenderer sr;

        Sprite up, down, left, right;

        public void Awake() {
            sr = gameObject.AddComponent<SpriteRenderer>();
        }

        public void Init(ICharacter character, string spritesName) {
            this.character = character;

            sr.sortingLayerName = "Tiles";

            string basePath = "Sprites/Characters/" + spritesName + "/";
            up = Resources.Load<Sprite>(basePath + "up");
            down = Resources.Load<Sprite>(basePath + "down");
            left = Resources.Load<Sprite>(basePath + "left");
            right = Resources.Load<Sprite>(basePath + "right");
            sr.sprite = right;
        }

        private void Update() {
            sr.sortingOrder = SortingOrders.TileOrder(character.X, character.Y, TileSubLayer.Character);

            if (Input.GetKeyDown(KeyCode.RightArrow))
                sr.sprite = right;
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
                sr.sprite = left;
            else if (Input.GetKeyDown(KeyCode.UpArrow))
                sr.sprite = up;
            else if (Input.GetKeyDown(KeyCode.DownArrow))
                sr.sprite = down;
        }

    }
	
}