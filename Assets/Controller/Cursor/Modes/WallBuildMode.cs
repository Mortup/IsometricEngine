using UnityEngine;

using com.gStudios.isometric.controller.data;
using com.gStudios.isometric.controller.spriteObservers;

using com.gStudios.isometric.model.world;
using com.gStudios.isometric.model.world.commands;

namespace com.gStudios.isometric.controller.cursor.modes {

	public class WallBuildMode : DefaultMode {

        public WallBuildMode(Level level) : base(level) {
            mainCursorSr.sortingLayerName = "Tiles";

            defaultSprite = DataManager.cursorSpriteData.wallMainSprite;
            Debug.Log(DataManager.cursorSpriteData.wallMainSprite);
        }

        protected override CursorCommand GetActionCommand(Vector2 mousePosition) {
            throw new System.NotImplementedException();
        }

        public override void UpdateCursors(Vector2 mousePosition) {
            Vector2Int coords = IsometricTransformer.ScreenToCoord(mousePosition);

            if (level.IsTileInBounds(coords.x, coords.y)) {
                mainCursorSr.enabled = true;
                mainCursorSr.sortingOrder = TileSpriteObserver.GetSortingOrder(coords.x, coords.y) + mainCursorSortingOrderOffset; // Only for tiles?

                Vector3 pos = Camera.main.ScreenToWorldPoint(mousePosition);
                mainCursorGo.transform.position = new Vector3(pos.x, pos.y, 0f);

                mainCursorSr.sprite = GetCursorSprite(coords); // Only for tiles
            }
            else {
                mainCursorSr.enabled = false;
            }
        }
    }

}