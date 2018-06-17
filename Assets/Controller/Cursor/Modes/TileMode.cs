﻿using UnityEngine;

using com.gStudios.isometric.controller.spriteObservers;

using com.gStudios.isometric.model.world;
using com.gStudios.isometric.model.world.commands;
using com.gStudios.isometric.model.world.tile;

namespace com.gStudios.isometric.controller.cursor.modes {

	public abstract class TileMode : DefaultMode{

        protected Sprite defaultSprite;
        protected Sprite onTileSprite;
        protected Sprite onEmptySprite;
        protected Sprite invertedSprite;
        protected Sprite invertedOnTileSprite;
        protected Sprite invertedOnEmptySprite;

        public TileMode(Level level) : base(level) {
            mainCursorSr.sortingLayerName = "Floor";
        }

        public override void UpdateCursors(Vector2 mousePosition) {
            Vector2Int coords = IsometricTransformer.ScreenToCoord(mousePosition);

            if (level.IsTileInBounds(coords.x, coords.y)) {
                mainCursorSr.enabled = true;
                mainCursorSr.sortingOrder = TileSpriteObserver.GetSortingOrder(coords.x, coords.y, FloorSubLayer.Cursor);
                mainCursorGo.transform.position = IsometricTransformer.CoordToWorld(coords);

                mainCursorSr.sprite = GetCursorSprite(coords);
            }
            else {
                mainCursorSr.enabled = false;
            }

            return;
        }

        protected Sprite GetCursorSprite(Vector2Int coords) {
            ITile tile = level.GetTileAt(coords.x, coords.y);
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