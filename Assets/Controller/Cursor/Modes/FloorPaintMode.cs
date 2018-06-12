using UnityEngine;

using com.gStudios.isometric.model.world;
using com.gStudios.isometric.model.world.tile;
using com.gStudios.isometric.model.world.commands;

using com.gStudios.isometric.controller.data;

namespace com.gStudios.isometric.controller.cursor.modes {

	public class FloorPaintMode : DraggableTileMode {

		public FloorPaintMode(Level level) : base(level) {
			defaultSprite = DataManager.cursorSpriteData.defaultSprite;
			onEmptySprite = DataManager.cursorSpriteData.emptySprite;
			invertedSprite = DataManager.tileSpriteData.GetDataById (TileIndex.NewTileIndex);
			invertedOnEmptySprite = DataManager.cursorSpriteData.emptySprite;
			SetIndex (TileIndex.NewTileIndex + 1);
		}

		public override void SetIndex (int index)
		{
			base.SetIndex (index);

			defaultSprite = DataManager.tileSpriteData.GetDataById(index);
		}

        protected override CursorCommand GetActionCommand(Vector2 mousePosition) {
			int selectedIndex = Input.GetButton ("InverseFunction") ? TileIndex.NewTileIndex : index;
			Vector2Int endCoords = IsometricTransformer.ScreenToCoord (mousePosition);

			return new PaintAreaCmd (level, dragStartCoords.x, endCoords.x, dragStartCoords.y, endCoords.y, selectedIndex);
		}
    }

}