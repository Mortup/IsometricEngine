using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using com.gStudios.isometric.model.world;
using com.gStudios.isometric.model.world.commands;

namespace com.gStudios.isometric.controller.cursor {

	public abstract class AbstractCursorMode : CursorMode {

		protected Level level;
		protected int selectedIndex;

		public AbstractCursorMode(Level level) {
			this.level = level;
			selectedIndex = -1;
		}

		public abstract CursorCommand OnClick (Vector2 mousePosition);

		public void SetSelectedIndex(int index) {
			if (index == -1) {
				Debug.LogError ("Cannot set selected index to negative values.");
				return;
			}

			selectedIndex = index;
		}
		
	}

}