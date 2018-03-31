using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using com.gStudios.isometric.model.world;
using com.gStudios.isometric.model.world.commands;

namespace com.gStudios.isometric.controller.cursor {
	public class FloorMode : AbstractCursorMode {

		public FloorMode(Level level) : base(level) {}

		public override CursorCommand OnClick(Vector2 mousePosition) {
			Debug.Log ("Clicking on Floor Mode");
			return null;
		}

	}
}
