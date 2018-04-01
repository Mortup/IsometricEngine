using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using com.gStudios.isometric.model.world;
using com.gStudios.isometric.controller;

namespace com.gStudios.isometric.controller.cursor {

	public class CursorSprite : MonoBehaviour {

		bool initialized = false;

		Level level;
		SpriteRenderer sr;

		Sprite defaultSprite;
		Sprite inverseSprite;

		bool followMouse = false;
		bool sortWithTiles = false;

		public bool showOnTiles = true;
		public bool showOnEmpty = true;
		public bool showOnTilesWhenInverted = true;
		public bool showOnEmptyWhenInverted = true;

		void Awake() {
			sr = GetComponent<SpriteRenderer> ();

			defaultSprite = sr.sprite;
		}

		public void Init(Level level) {
			this.level = level;

			initialized = true;
		}

		void Update() {
			if (!initialized)
				return;

			Vector2Int coords = IsometricTransformer.ScreenToCoord (Input.mousePosition);

			// Follow mouse?
			if (followMouse) {
				transform.position = IsometricTransformer.CoordToWorld (coords.x, coords.y);
			}

			// Sort with tiles?
			if (sortWithTiles) {
				sr.sortingOrder = TileSpriteManager.GetSortingOrder(coords.x, coords.y) + 1;
			}

			// Is in bounds?
			if (!level.IsInBounds (coords.x, coords.y)) {
				sr.sprite = null;
				return;
			}
			
			int tileIndex = level.GetTileAt (coords.x, coords.y).Type;
			if (Input.GetButton("InverseFunction") && inverseSprite != null) {
				sr.sprite = ((showOnTilesWhenInverted && tileIndex != Tile.EmptyTileIndex) || (showOnEmptyWhenInverted && tileIndex == Tile.EmptyTileIndex)) ? inverseSprite : null;

			}
			else {
				sr.sprite = ((showOnTiles && tileIndex != Tile.EmptyTileIndex) || (showOnEmpty && tileIndex == Tile.EmptyTileIndex)) ? defaultSprite : null;
			}


		}

		public void SetSprite(Sprite sprite) {
			defaultSprite = sprite;
		}

		public void SetInverseSprite(Sprite sprite) {
			inverseSprite = sprite;
		}

		public void FollowMouse() {
			followMouse = true;
		}

		public void SortWithTiles() {
			sortWithTiles = true;

			sr.sortingLayerName = "Tiles";
		}

	}

}