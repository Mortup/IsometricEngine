using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using com.gStudios.isometric.model.world;
using com.gStudios.isometric.model.world.commands;

namespace com.gStudios.isometric.controller.cursor {
	public class WallsMode : AbstractCursorMode {

		public WallsMode(Level level) : base (level) {}

		public override CursorCommand OnClick(Vector2 mousePosition) {
			Debug.Log ("Clicking on Walls Mode");
			return null;
		}

	}
}
