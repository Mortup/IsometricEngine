﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using com.gStudios.isometric.model.world;
using com.gStudios.isometric.model.world.commands;

using com.gStudios.isometric.controller.cursor.modes;
using com.gStudios.isometric.controller.config;

using com.gStudios.utils.structs;

namespace com.gStudios.isometric.controller.cursor {

	/// <summary>
	/// This class takes care of the user click actions and the
	/// instantiation of cursors.
	/// </summary>
	public class CursorController : MonoBehaviour {

		/// <summary>
		/// A reference to the current level.
		/// </summary>
		Level level;

		modes.CursorMode currentMode;

		const int mouseButton = 0;

		DropoutStack<CursorCommand> inverseCommands;

		/// <summary>
		/// Should be called right after instantiation.
		/// </summary>
		/// <param name="level">The level to interact with.</param>
		public void Init(Level level) {
			this.level = level;
			SimplePool.Preload (Resources.Load<GameObject> (Paths.CursorPrefab));
			inverseCommands = new DropoutStack<CursorCommand> (Settings.MaxCursorUndoStackSize);

			SetMode ("build");
		}

		public void SetMode(string mode) {
			if (currentMode != null)
				currentMode.Deactivate ();

			if (mode == "build") {
				currentMode = new BuildMode (level);
			}
			else if (mode == "floor") {
				currentMode = new FloorPaintMode (level);
			}
			else {
				Debug.LogError ("Trying to set an unkown mode " + mode);
			}

			currentMode.Activate ();
		}

		public void Update() {
			if (Input.GetMouseButtonDown (mouseButton))
				ClickStart ();
			if (Input.GetMouseButtonUp (mouseButton))
				ClickEnd ();
			if (Input.GetButtonDown ("Undo"))
				Undo ();

			currentMode.UpdateCursors (Input.mousePosition);
		}

		public void ClickStart() {
			if (EventSystem.current.IsPointerOverGameObject ())
				return;
			
			currentMode.ClickStart (Input.mousePosition);
		}

		public void ClickEnd() {
			CursorCommand cmd = currentMode.ClickEnd (Input.mousePosition);
			CursorCommand invCmd = cmd.Excecute ();
			inverseCommands.Push (invCmd);
		}

		void Undo() {
			inverseCommands.Pop ().Excecute ();
		}
	}

}