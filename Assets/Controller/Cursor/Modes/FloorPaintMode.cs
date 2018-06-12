using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using com.gStudios.isometric.model.world;
using com.gStudios.isometric.model.world.tile;
using com.gStudios.isometric.model.world.commands;

using com.gStudios.isometric.controller.config;
using com.gStudios.isometric.controller.data;
using com.gStudios.isometric.controller.ui;

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

		public override CursorCommand ClickEnd (Vector2 mousePosition) {
			if (!isDragging)
				return NullCommand.instance;

			int selectedIndex = Input.GetButton ("InverseFunction") ? TileIndex.NewTileIndex : index;

			Vector2Int endCoords = IsometricTransformer.ScreenToCoord (mousePosition);

			isDragging = false;
			return new PaintAreaCmd (level, dragStartCoords.x, endCoords.x, dragStartCoords.y, endCoords.y, selectedIndex);
		}
	}

}