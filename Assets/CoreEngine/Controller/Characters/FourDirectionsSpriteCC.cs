using UnityEngine;

using com.gStudios.isometric.model.characters;
using com.gStudios.isometric.controller.spriteObservers;

namespace com.gStudios.isometric.controller.characters {

	public class FourDirectionsSpriteCC : MonoBehaviour, ICharacterObserver {

        ICharacter character;
        SpriteRenderer sr;

        Sprite up, down, left, right;

        public void Awake() {
            sr = gameObject.AddComponent<SpriteRenderer>();
        }

        public void Init(ICharacter character, string spritesName) {
            this.character = character;
            character.Subscribe(this);

            sr.sortingLayerName = "Tiles";

            string basePath = "Sprites/Characters/" + spritesName + "/";
            up = Resources.Load<Sprite>(basePath + "up");
            down = Resources.Load<Sprite>(basePath + "down");
            left = Resources.Load<Sprite>(basePath + "left"); 
            right = Resources.Load<Sprite>(basePath + "right");
            sr.sprite = right;
        }

        public void OnCharMove(int xOffset, int yOffset, bool succesful) {
            if (xOffset > 0) {
                sr.sprite = down;
            }            
            else if(xOffset < 0) {
                sr.sprite = up;
            }
            else if(yOffset > 0) {
                sr.sprite = right;
            }
            else if (yOffset < 0) {
                sr.sprite = left;
            }
        }

        private void Update() {
            sr.sortingOrder = SortingOrders.TileOrder(character.X, character.Y, TileSubLayer.Character);

        }

        

    }
	
}