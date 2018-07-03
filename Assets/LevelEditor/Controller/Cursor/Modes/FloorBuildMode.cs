﻿using UnityEngine;

using com.gStudios.isometric.model.world;
using com.gStudios.isometric.model.world.tile;
using com.gStudios.isometric.model.world.commands;

using com.gStudios.isometric.controller;
using com.gStudios.isometric.controller.data;

namespace com.gStudios.levelEditor.controller.cursor.modes {

	public class BuildMode : DraggableTileMode {

		public BuildMode(Level level) : base(level) {
			defaultSprite = DataManager.cursorSpriteData.tileBuildSprite;
			invertedSprite = DataManager.cursorSpriteData.tileRemoveSprite;
			onTileSprite = DataManager.cursorSpriteData.tileBuildOverTileSprite;
			invertedOnEmptySprite = DataManager.cursorSpriteData.tileRemoveOnEmptySprite;
		}

        protected override CursorCommand GetActionCommand(Vector2 mousePosition) {
            int selectedIndex = Input.GetButton("InverseFunction") ? TileIndex.EmptyTileIndex : TileIndex.NewTileIndex;
            Vector2Int endCoords = IsometricTransformer.ScreenToCoord(mousePosition);

            return new BuildAreaCmd(level, dragStartCoords.x, endCoords.x, dragStartCoords.y, endCoords.y, selectedIndex);
        }
    }

}