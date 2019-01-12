using com.gStudios.isometric.controller.isometricTransform;
using com.gStudios.isometric.controller.spriteObservers;
using com.gStudios.isometric.model.world;
using com.gStudios.isometric.model.world.commands;

using UnityEngine;

namespace com.gStudios.levelEditor.controller.cursor.modes {

    public class FurnitureMode : DefaultMode {

        public FurnitureMode(Level level) :base(level) {
            mainCursorSr.sortingLayerName = "Tiles";
        }

        protected override IWorldCommand GetActionCommand(Vector2 mousePosition) {
            Vector2Int tilePos = TileTransformer.ScreenToCoord(mousePosition);
            level.GetTileAt(tilePos.x, tilePos.y);

            return NullCommand.instance;
        }

        public override void UpdateCursors(Vector2 mousePosition) {
            Vector2Int tilePos = TileTransformer.ScreenToCoord(mousePosition);

            if (level.IsTileInBounds(tilePos.x, tilePos.y)) {
                mainCursorSr.enabled = true;
                mainCursorSr.sortingOrder = SortingOrders.TileOrder(tilePos.x, tilePos.y, TileSubLayer.Furniture);
                mainCursorSr.sprite = Resources.Load<Sprite>("Sprites/Furniture/2");

                mainCursorGo.transform.position = TileTransformer.CoordToWorld(tilePos);
            }
            else {
                mainCursorSr.enabled = false;
            }
        }
    }
}
