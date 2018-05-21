using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using com.gStudios.isometric.model.world;
using com.gStudios.isometric.model.world.tile;
using com.gStudios.isometric.model.world.commands;

using com.gStudios.utils;

using com.gStudios.isometric.controller.data;
using com.gStudios.isometric.controller.config;

namespace com.gStudios.isometric.controller.cursor.modes {

	public class BuildMode : DraggableMode {

		public BuildMode(Level level) : base(level) {
			defaultSprite = DataManager.cursorSpriteData.buildSprite;
			invertedSprite = DataManager.cursorSpriteData.buildInverseSprite;
			onTileSprite = DataManager.cursorSpriteData.buildOverTileSprite;
			invertedOnEmptySprite = DataManager.cursorSpriteData.buildInvertedOnEmptySprite;
			cursorSr.sortingLayerName = "Tiles";
		}
//
		public override CursorCommand ClickEnd (Vector2 mousePosition) {
			if (!isDragging)
				return NullCommand.instance;

			int selectedIndex = Input.GetButton ("InverseFunction") ? TileIndex.EmptyTileIndex : TileIndex.NewTileIndex;

			Vector2Int startCoords = IsometricTransformer.ScreenToCoord (mouseStartPosition);
			Vector2Int endCoords = IsometricTransformer.ScreenToCoord (mousePosition);

			isDragging = false;
			return new BuildAreaCmd (level, startCoords.x, endCoords.x, startCoords.y, endCoords.y, selectedIndex);
		}
		
	}

}