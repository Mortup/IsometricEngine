﻿using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;

using com.gStudios.utils.structs;
using com.gStudios.isometric.model.world;
using com.gStudios.isometric.model.world.commands;

namespace com.gStudios.isometric.controller.cursor {

	public class CursorController : MonoBehaviour {

		bool initialized = false;

		Level level;
		CursorMode currentMode;

		DropoutStack<CursorCommand> cmdStack;

		private int maxUndoStack = 20;

		public void Init(Level level) {
			this.level = level;

			cmdStack = new DropoutStack<CursorCommand> (maxUndoStack);
			SetMode ("build");

			initialized = true;
		}

		public void Update() {
			if (!initialized)
				return;

			if (Input.GetMouseButtonDown (0) && !EventSystem.current.IsPointerOverGameObject ())
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
			CursorCommand cmd = currentMode.OnClick (Input.mousePosition);
			CursorCommand inverse = cmd.Excecute ();

			if (inverse != null)
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