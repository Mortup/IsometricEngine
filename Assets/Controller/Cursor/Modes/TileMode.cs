using UnityEngine;

using com.gStudios.isometric.controller.spriteObservers;
using com.gStudios.isometric.model.world;

namespace com.gStudios.isometric.controller.cursor.modes {

	public abstract class TileMode : DefaultMode{

        public TileMode(Level level) : base(level) {
            mainCursorSr.sortingLayerName = "Floor";
        }

        public override void UpdateCursors(Vector2 mousePosition) {
            Vector2Int coords = IsometricTransformer.ScreenToCoord(mousePosition);

            if (level.IsTileInBounds(coords.x, coords.y)) {
                mainCursorSr.enabled = true;
                mainCursorSr.sortingOrder = TileSpriteObserver.GetSortingOrder(coords.x, coords.y) + mainCursorSortingOrderOffset; // Only for tiles?
                mainCursorGo.transform.position = IsometricTransformer.CoordToWorld(coords); // Only for tiles

                mainCursorSr.sprite = GetCursorSprite(coords); // Only for tiles
            }
            else {
                mainCursorSr.enabled = false;
            }
        }
    }

}