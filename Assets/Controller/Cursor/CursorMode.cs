using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using com.gStudios.isometric.model.world.commands;

namespace com.gStudios.isometric.controller.cursor {

	/// <summary>
	/// Defines a behaviour for the cursor in-game.
	/// </summary>
	public interface CursorMode {

		/// <summary>
		/// Should be called when the user starts a mouse click.
		/// </summary>
		/// <param name="mousePosition">Mouse position.</param>
		void ClickStart (Vector2 mousePosition);

		/// <summary>
		/// Should be called when the user finishes a mouse click.
		/// </summary>
		/// <returns>The end.</returns>
		/// <param name="mousePosition">Mouse position.</param>
		CursorCommand ClickEnd(Vector2 mousePosition);

		/// <summary>
		/// Called when the mode is activated.
		/// </summary>
		void TurnOn();

		/// <summary>
		/// Should be called when the mode is deactivated.
		/// </summary>
		void TurnOff();

		/// <summary>
		/// Updates the cursors on scene.
		/// </summary>
		void UpdateCursors();

		/// <summary>
		/// Sets the index of the object the mode will be editing.
		/// </summary>
		/// <param name="index">Index.</param>
		void SetSelectedIndex (int index);
	}

}