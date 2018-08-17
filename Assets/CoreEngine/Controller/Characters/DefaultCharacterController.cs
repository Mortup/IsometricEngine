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
            char_go.transform.position = TileTransformer.CoordToWorld(character.X, character.Y);
            //char_go.transform.SetParent(tileHolder.transform, true);

            SpriteRenderer sr = char_go.AddComponent<SpriteRenderer>();
            sr.sortingLayerName = "Tiles";
            sr.sprite = Resources.Load<Sprite>("Sprites/Untitled");
            sr.sortingOrder = WallSpriteObserver.GetSortingOrder(character.X, character.Y, 0, TileSubLayer.Character);

            charGo = char_go;
            //tile.Subscribe(this);
        }

        public void Update() {
            Debug.Log("Controller being updated...");
            Debug.Log(initializated);
        }

    }

}