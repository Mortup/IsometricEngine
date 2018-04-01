using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using com.gStudios.isometric.model.world;
using com.gStudios.isometric.model.world.commands;

using com.gStudios.isometric.controller.config;

namespace com.gStudios.isometric.controller.cursor {

	public abstract class AbstractCursorMode : CursorMode {

		protected Level level;
		protected int selectedIndex;
		protected GameObject cursorGameobject;

		public AbstractCursorMode(Level level) {
			this.level = level;
			selectedIndex = -1;

			cursorGameobject = GameObject.Instantiate( Resources.Load<GameObject> (Paths.CursorPrefab) );
			cursorGameobject.GetComponent<CursorSprite> ().Init (level);
		}

		public abstract CursorCommand ClickEnd (Vector2 mousePosition);

		public virtual void ClickStart (Vector2 mousePosition) {}

		public void SetSelectedIndex(int index) {
			if (index == -1) {
				Debug.LogError ("Cannot set selected index to negative values.");
				return;
			}

			selectedIndex = index;
		}

		public void DestroyCursors() {
			GameObject.Destroy (cursorGameobject);
		}
		
	}

}