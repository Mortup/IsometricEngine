using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using com.gStudios.isometric.model.world;
using com.gStudios.isometric.model.world.commands;

using com.gStudios.isometric.controller.config;

namespace com.gStudios.isometric.controller.cursor {
	public class BuildMode : AbstractCursorMode {

		bool isDragging = false;
		Vector2Int dragStartCoords;

		public BuildMode(Level level) : base(level) {
			CursorSprite cursorSprite = cursorGameobject.GetComponent<CursorSprite> ();
			cursorSprite.SetSprite (Resources.Load<Sprite> (Paths.CursorSprite ("BuildCursor")));
			cursorSprite.SetInverseSprite (Resources.Load<Sprite> (Paths.CursorSprite ("BuildInverseCursor")));

			cursorSprite.SortWithTiles ();
			cursorSprite.FollowMouse ();
			cursorSprite.showOnEmptyWhenInverted = false;
			cursorSprite.showOnTiles = false;
		}

		public override void ClickStart (Vector2 mousePosition)
		{
			base.ClickStart (mousePosition);

			isDragging = true;
			dragStartCoords = IsometricTransformer.ScreenToCoord (mousePosition);
		}

		public override CursorCommand ClickEnd(Vector2 mousePosition) {

			Vector2Int releasedCoords = IsometricTransformer.ScreenToCoord (mousePosition);
			int index = Input.GetButton ("InverseFunction") ? Tile.EmptyTileIndex : Tile.NewTileIndex;
			
			if (!isDragging){
				return NullCommand.instance;
			}

			isDragging = false;
			return new BuildAreaCmd(level, dragStartCoords.x, releasedCoords.x, dragStartCoords.y, releasedCoords.y, index);
		}
	}
}
