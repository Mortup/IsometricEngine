using UnityEngine;

using com.gStudios.isometric.model.world;
using com.gStudios.isometric.model.world.tile;
using com.gStudios.isometric.model.world.commands;

using com.gStudios.isometric.controller;
using com.gStudios.isometric.controller.data;
using com.gStudios.isometric.controller.isometricTransform;

namespace com.gStudios.levelEditor.controller.cursor.modes {

	public class FloorPaintMode : DraggableTileMode {

		public FloorPaintMode(Level level) : base(level) {
			defaultSprite = DataManager.cursorSpriteData.defaultSprite;
			onEmptySprite = DataManager.cursorSpriteData.emptySprite;
			invertedSprite = DataManager.tileSpriteData.GetDataById (TileIndex.New);
			invertedOnEmptySprite = DataManager.cursorSpriteData.emptySprite;
			SetIndex (TileIndex.New + 1);
		}

		public override void SetIndex (int index)
		{
			base.SetIndex (index);

			defaultSprite = DataManager.tileSpriteData.GetDataById(index);
		}

        protected override IWorldCommand GetActionCommand(Vector2 mousePosition) {
			int selectedIndex = Input.GetButton ("InverseFunction") ? TileIndex.New : index;
			Vector2Int endCoords = TileTransformer.ScreenToCoord (mousePosition);

			return new PaintAreaCmd (level, dragStartCoords.x, endCoords.x, dragStartCoords.y, endCoords.y, selectedIndex);
		}
    }

}