using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using com.gStudios.isometric.model.world;
using com.gStudios.isometric.model.world.tile;
using com.gStudios.isometric.model.world.commands;
using com.gStudios.isometric.controller.spriteObservers;

using com.gStudios.isometric.controller.config;
using com.gStudios.isometric.controller.data;

namespace com.gStudios.isometric.controller.cursor.modes {

	public abstract class DefaultMode : CursorMode {

		const int sortingOrderOffset = 2;

		protected Level level;
		protected int index = -1;

		protected GameObject cursorGo;
		protected SpriteRenderer cursorSr;

		// Cursor sprites
		protected Sprite defaultSprite;
		protected Sprite onTileSprite;
		protected Sprite onEmptySprite;
		protected Sprite invertedSprite;
		protected Sprite invertedOnTileSprite;
		protected Sprite invertedOnEmptySprite;

		public DefaultMode(Level level) {
			this.level = level;

			cursorGo = new GameObject ("Cursor");
			cursorSr = cursorGo.AddComponent<SpriteRenderer> ();
			cursorSr.sprite = DataManager.cursorSpriteData.defaultSprite;
			cursorSr.sortingLayerName = "Debug";
		}

		public virtual void Activate() {
			cursorGo.SetActive (true);
		}
		public virtual void Deactivate() {
			cursorGo.SetActive (false);
		}

		public virtual void ClickStart (Vector2 mousePosition) {}
		public abstract CursorCommand ClickEnd (Vector2 mousePosition);

		public virtual void UpdateCursors(Vector2 mousePosition) {
			Vector2Int coords = IsometricTransformer.ScreenToCoord (mousePosition);

			if (level.IsTileInBounds(coords.x, coords.y)) {
				cursorSr.enabled = true;
				cursorSr.sortingOrder = TileSpriteObserver.GetSortingOrder (coords.x, coords.y) + sortingOrderOffset; // Only for tiles?
				cursorGo.transform.position = IsometricTransformer.CoordToWorld (coords); // Only for tiles

				cursorSr.sprite = GetCursorSprite (coords); // Only for tiles
			}
			else {
				cursorSr.enabled = false;
			}
		}

		public virtual void SetIndex(int index) {
			this.index = index;
		}

		protected Sprite GetCursorSprite(Vector2Int coords) {
			ITile tile = level.GetTileAt (coords.x, coords.y);
			if (tile == null)
				return null;

			if (invertedSprite != null && Input.GetButton("InverseFunction")) {
				if (invertedOnEmptySprite != null && tile.Type == TileIndex.EmptyTileIndex) {
					return invertedOnEmptySprite;
				}
				else if (invertedOnTileSprite != null && tile.Type != TileIndex.EmptyTileIndex) {
					return invertedOnTileSprite;
				}
				else {
					return invertedSprite;
				}
			}
			else {
				if (onEmptySprite != null && tile.Type == TileIndex.EmptyTileIndex) {
					return onEmptySprite;
				}
				else if (onTileSprite != null && tile.Type != TileIndex.EmptyTileIndex) {
					return onTileSprite;
				}
				else {
					return defaultSprite;
				}
			}
		}

	}

}