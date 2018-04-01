using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using com.gStudios.isometric.model.world;
using com.gStudios.isometric.model.world.commands;

using com.gStudios.isometric.controller.config;

namespace com.gStudios.isometric.controller.cursor {
	public class BuildMode : AbstractCursorMode {

		public BuildMode(Level level) : base(level) {
		}

		public override CursorCommand ClickEnd(Vector2 mousePosition) {
			Vector2Int pressedCoords = IsometricTransformer.ScreenToCoord (mousePosition);

			if (Input.GetButton ("InverseFunction")) {
				return new BuildTileCmd (level, pressedCoords.x, pressedCoords.y, Tile.EmptyTileIndex);
			}
			return new BuildTileCmd (level, pressedCoords.x, pressedCoords.y, Tile.NewTileIndex);
		}
	}
}
