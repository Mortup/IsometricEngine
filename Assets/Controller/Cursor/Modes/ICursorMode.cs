using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using com.gStudios.isometric.model.world.commands;

namespace com.gStudios.isometric.controller.cursor.modes {

	/// <summary>
	/// The state of the cursor. Determines which actions will
	/// be excecuted when cursor's input is received.
	/// </summary>
	public interface ICursorMode {

		/// <summary>
		/// Should be called when the cursor mode is enabled.
		/// </summary>
		void Activate ();

		/// <summary>
		/// Should be called when the cursor mode is disabled.
		/// </summary>
		void Deactivate();

		/// <summary>
		/// Called when a mouse's click starts.
		/// </summary>
		/// <param name="mousePosition">Mouse position.</param>
		void ClickStart (Vector2 mousePosition);

		/// <summary>
		/// Called when the click ends.
		/// </summary>
		/// <param name="mousePosition">Mouse position.</param>
		CursorCommand ClickEnd (Vector2 mousePosition);

		/// <summary>
		/// Updates the cursors.
		/// </summary>
		/// <param name="mousePosition">Mouse position.</param>
		void UpdateCursors (Vector2 mousePosition);

		/// <summary>
		/// Sets the index the mode will be using.
		/// This can be used to know which color to paint a
		/// tile, or a wall, or which item to place.
		/// </summary>
		/// <param name="index">Index.</param>
		void SetIndex(int index);
	}

}