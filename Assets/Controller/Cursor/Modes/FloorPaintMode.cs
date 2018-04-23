using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using com.gStudios.isometric.model.world;
using com.gStudios.isometric.model.world.commands;
using com.gStudios.isometric.controller.config;

using com.gStudios.isometric.controller.ui;

namespace com.gStudios.isometric.controller.cursor.modes {

	public class FloorPaintMode : DraggableMode {

		public FloorPaintMode(Level level) : base(level) {
			defaultSprite = Resources.Load<Sprite> (Paths.CursorSprite("Default"));
			onEmptySprite = Resources.Load<Sprite> (Paths.CursorSprite("Empty"));
			invertedSprite = Resources.Load<Sprite> (Paths.TileSprite (Tile.NewTileIndex));
			invertedOnEmptySprite = Resources.Load<Sprite> (Paths.CursorSprite("Empty"));
			cursorSr.sortingLayerName = "Tiles";
			SetIndex (Tile.NewTileIndex + 1);
		}

		public override void SetIndex (int index)
		{
			base.SetIndex (index);

			defaultSprite = Resources.Load<Sprite> (Paths.TileSprite(index));
		}

		public override CursorCommand ClickEnd (Vector2 mousePosition) {
			if (!isDragging)
				return NullCommand.instance;

			int selectedIndex = Input.GetButton ("InverseFunction") ? Tile.NewTileIndex : index;

			Vector2Int startCoords = IsometricTransformer.ScreenToCoord (mouseStartPosition);
			Vector2Int endCoords = IsometricTransformer.ScreenToCoord (mousePosition);

			isDragging = false;
			return new PaintAreaCmd (level, startCoords.x, endCoords.x, startCoords.y, endCoords.y, selectedIndex);
		}
	}

}