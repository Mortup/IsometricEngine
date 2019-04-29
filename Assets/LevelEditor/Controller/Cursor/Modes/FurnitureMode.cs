using UnityEngine;
using com.gStudios.isometric.controller.isometricTransform;
using com.gStudios.isometric.controller.spriteObservers;
using com.gStudios.isometric.controller.data;
using com.gStudios.isometric.controller.data.structs;

using com.gStudios.isometric.model.world;
using com.gStudios.isometric.model.world.commands;
using com.gStudios.isometric.model.world.furniture;
using com.gStudios.isometric.model.world.tile;
using com.gStudios.isometric.model.world.orientation;

namespace com.gStudios.levelEditor.controller.cursor.modes {

    public class FurnitureMode : DefaultMode {

        public FurnitureMode(Level level) :base(level) {
            mainCursorSr.sortingLayerName = "Tiles";
            index = 1;
        }

        protected override IWorldCommand GetActionCommand(Vector2 mousePosition) {
            Vector2Int tilePos = TileTransformer.ScreenToCoord(mousePosition);
            ITile tile = level.GetTileAt(tilePos.x, tilePos.y);

            if (Input.GetButton("InverseFunction"))
                return new RemoveFurnitureCommand(level, tilePos.x, tilePos.y);

            IFurniture furniture = new DecorationFurniture(index, level, tile);
            return new PlaceFurnitureCommand(level, tilePos.x, tilePos.y, furniture);
        }

        public override void UpdateCursors(Vector2 mousePosition) {
            Vector2Int tilePos = TileTransformer.ScreenToCoord(mousePosition);

            if (level.IsTileInBounds(tilePos.x, tilePos.y)) {
                mainCursorSr.enabled = true;
                mainCursorSr.sortingOrder = SortingOrders.TileOrder(tilePos.x, tilePos.y, TileSubLayer.Furniture);

                IFurnitureSprite sprite = DataManager.furnitureSpriteData.GetDataById(index);
                mainCursorSr.sprite = sprite.GetSprite();

                mainCursorGo.transform.position = TileTransformer.CoordToWorld(tilePos);
            }
            else {
                mainCursorSr.enabled = false;
            }
        }

        public override void Rotate(RotationDirection rotation) {
            base.Rotate(rotation);

            Debug.Log("Rotandox");
        }
    }
}
