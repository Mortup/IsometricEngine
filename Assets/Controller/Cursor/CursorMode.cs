using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using com.gStudios.isometric.model.world.commands;

namespace com.gStudios.isometric.controller.cursor {

	/// <summary>
	/// Defines a behaviour for the cursor in-game.
	/// </summary>
	public interface CursorMode {

		void ClickStart (Vector2 mousePosition);

		CursorCommand ClickEnd(Vector2 mousePosition);

		void SetSelectedIndex (int index);

		void DestroyCursors();

	}

}