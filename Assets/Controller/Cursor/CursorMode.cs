using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using com.gStudios.isometric.model.world.commands;

namespace com.gStudios.isometric.controller.cursor {

	/// <summary>
	/// Defines a behaviour for the cursor in-game.
	/// </summary>
	public interface CursorMode {

		CursorCommand OnClick(Vector2 mousePosition);

		void SetSelectedIndex (int index);
		
	}

}