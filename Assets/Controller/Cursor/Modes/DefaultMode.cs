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

    /// <summary>
    /// Base abstract class for all cursor modes.
    /// Provides basic functionality.
    /// </summary>
	public abstract class DefaultMode : CursorMode {

		protected int mainCursorSortingOrderOffset = 2;

		protected Level level;
		protected int index = -1;

		protected GameObject mainCursorGo;
		protected SpriteRenderer mainCursorSr;

		// Cursor sprites
		protected Sprite defaultSprite;
		protected Sprite onTileSprite;
		protected Sprite onEmptySprite;
		protected Sprite invertedSprite;
		protected Sprite invertedOnTileSprite;
		protected Sprite invertedOnEmptySprite;

		public DefaultMode(Level level) {
			this.level = level;

			mainCursorGo = new GameObject ("Main Cursor");
			mainCursorSr = mainCursorGo.AddComponent<SpriteRenderer> ();
			mainCursorSr.sprite = DataManager.cursorSpriteData.defaultSprite;
			mainCursorSr.sortingLayerName = "Debug";
		}

		public virtual void Activate() {
			mainCursorGo.SetActive (true);
		}
		public virtual void Deactivate() {
            GameObject.Destroy(mainCursorGo);
		}

		public virtual void ClickStart (Vector2 mousePosition) {}
		public abstract CursorCommand ClickEnd (Vector2 mousePosition);

		public virtual void UpdateCursors(Vector2 mousePosition) {
			Vector2Int coords = IsometricTransformer.ScreenToCoord (mousePosition);

			if (level.IsTileInBounds(coords.x, coords.y)) {
				mainCursorSr.enabled = true;
				mainCursorSr.sortingOrder = TileSpriteObserver.GetSortingOrder (coords.x, coords.y) + mainCursorSortingOrderOffset; // Only for tiles?
				mainCursorGo.transform.position = IsometricTransformer.CoordToWorld (coords); // Only for tiles

				mainCursorSr.sprite = GetCursorSprite (coords); // Only for tiles
			}
			else {
				mainCursorSr.enabled = false;
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