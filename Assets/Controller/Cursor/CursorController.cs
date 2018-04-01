using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;

using com.gStudios.utils.structs;
using com.gStudios.isometric.model.world;
using com.gStudios.isometric.model.world.commands;

using com.gStudios.isometric.controller.config;

namespace com.gStudios.isometric.controller.cursor {

	public class CursorController : MonoBehaviour {

		bool initialized = false;

		Level level;
		CursorMode _currentMode;

		public CursorMode currentMode {
			get {
				return _currentMode;
			}
			set {
				if (_currentMode != null)
					_currentMode.DestroyCursors ();
				
				_currentMode = value;
			}
		}

		DropoutStack<CursorCommand> cmdStack;

		public void Init(Level level) {
			this.level = level;

			cmdStack = new DropoutStack<CursorCommand> (Settings.MaxCursorUndoStackSize);
			SetMode ("build");

			initialized = true;
		}

		public void Update() {
			if (!initialized)
				return;

			if (Input.GetMouseButtonDown (0) && !EventSystem.current.IsPointerOverGameObject ())
				currentMode.ClickStart (Input.mousePosition);

			if (Input.GetMouseButtonUp(0))
				ExcecuteClick ();

			if (Input.GetButtonDown("Undo")) {
				Undo ();
			}
		}

		public void SetMode(string newMode) {

			switch(newMode) {
			case "build":
				currentMode = new BuildMode (level);
				break;
			case "floor":
				currentMode = new FloorMode (level);
				break;
			case "walls":
				currentMode = new WallsMode (level);
				break;
			case "furniture":
				currentMode = new FurnitureMode (level);
				break;
			default:
				Debug.LogError ("Cannot find mode " + newMode);
				break;
			}

		}

		private void ExcecuteClick() {
			CursorCommand cmd = currentMode.ClickEnd (Input.mousePosition);

			CursorCommand inverse = cmd.Excecute ();

			if (inverse != NullCommand.instance)
				cmdStack.Push (inverse);
		}

		private void Undo() {
			if (cmdStack.Count > 0) {
				CursorCommand cmd = cmdStack.Pop ();
				cmd.Excecute ();
			}
		}
	}

}