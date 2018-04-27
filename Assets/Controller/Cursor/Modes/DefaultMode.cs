using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using com.gStudios.isometric.model.world;
using com.gStudios.isometric.model.world.commands;
using com.gStudios.isometric.controller.loaders;

using com.gStudios.isometric.controller.config;

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
			cursorSr.sprite = Resources.Load<Sprite> (Paths.CursorSprite ("Default"));
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

			if (level.IsInBounds(coords.x, coords.y)) {
				cursorSr.enabled = true;
				cursorSr.sortingOrder = TileSpriteObserver.GetSortingOrder (coords.x, coords.y) + sortingOrderOffset;
				cursorGo.transform.position = IsometricTransformer.CoordToWorld (coords);

				cursorSr.sprite = GetCursorSprite (coords);
			}
			else {
				cursorSr.enabled = false;
			}
		}

		public virtual void SetIndex(int index) {
			this.index = index;
		}

		protected Sprite GetCursorSprite(Vector2Int coords) {
			Tile tile = level.GetTileAt (coords.x, coords.y);
			if (tile == null)
				return null;

			if (invertedSprite != null && Input.GetButton("InverseFunction")) {
				if (invertedOnEmptySprite != null && tile.Type == Tile.EmptyTileIndex) {
					return invertedOnEmptySprite;
				}
				else if (invertedOnTileSprite != null && tile.Type != Tile.EmptyTileIndex) {
					return invertedOnTileSprite;
				}
				else {
					return invertedSprite;
				}
			}
			else {
				if (onEmptySprite != null && tile.Type == Tile.EmptyTileIndex) {
					return onEmptySprite;
				}
				else if (onTileSprite != null && tile.Type != Tile.EmptyTileIndex) {
					return onTileSprite;
				}
				else {
					return defaultSprite;
				}
			}
		}

	}

}