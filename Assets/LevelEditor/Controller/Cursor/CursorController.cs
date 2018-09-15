using UnityEngine;
using UnityEngine.EventSystems;

using com.gStudios.isometric.model.world;
using com.gStudios.isometric.model.world.commands;

using com.gStudios.isometric.controller;
using com.gStudios.isometric.controller.config;

using com.gStudios.utils.structs;

using com.gStudios.levelEditor.controller.cursor.modes;

namespace com.gStudios.levelEditor.controller.cursor {

	/// <summary>
	/// This class takes care of the user click actions and the
	/// instantiation of cursors.
	/// </summary>
	public class CursorController : MonoBehaviour, ILevelController {

		/// <summary>
		/// A reference to the current level.
		/// </summary>
		Level level;

		modes.ICursorMode currentMode;

		const int mouseButton = 0;

		DropoutStack<CursorCommand> inverseCommands;

        public void Init(CoreLevelController clc) { }

		/// <summary>
		/// Should be called right after instantiation.
		/// </summary>
		/// <param name="level">The level to interact with.</param>
		public void OnLevelInit(Level level) {
			this.level = level;
			SimplePool.Preload (Resources.Load<GameObject> (GamePaths.CursorPrefab));
			inverseCommands = new DropoutStack<CursorCommand> (Settings.MaxCursorUndoStackSize);

			SetMode ("buildFloor");
		}

		public void SetMode(string mode) {
			if (currentMode != null)
				currentMode.Deactivate ();

			if (mode == "buildFloor") {
				currentMode = new BuildMode (level);
			}
			else if (mode == "paintFloor") {
				currentMode = new FloorPaintMode (level);
			}
            else if (mode == "buildWalls") {
                currentMode = new WallBuildMode(level);
            }
            else if (mode == "paintWalls") {
                currentMode = new WallPaintMode(level);
            }
			else {
				Debug.LogError ("Trying to set an unkown mode " + mode);
			}

			currentMode.Activate ();
		}

		public void SetIndex(int index) {
			if (currentMode != null)
				currentMode.SetIndex (index);
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