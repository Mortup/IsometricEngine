using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;

using com.gStudios.isometric.controller.data;
using com.gStudios.isometric.controller.cursor;
using com.gStudios.isometric.controller.spriteObservers;
using com.gStudios.isometric.controller.saving;
using com.gStudios.isometric.model.world;

namespace com.gStudios.isometric.controller {

	public class LevelController : MonoBehaviour {

		Level level;
		CursorController cursorController;
		TileSpriteObserver tileSpriteObserver;
		LevelSerializer levelSerializer;

		void Start () {
			DataManager.Init ();

			tileSpriteObserver = new TileSpriteObserver ();

			levelSerializer = new LevelSerializer (tileSpriteObserver);
			level = levelSerializer.LoadLevel ();

			cursorController = GetComponent<CursorController> ();
			cursorController.Init (level);
		}
		
		void Update () {
			if (Input.GetKeyDown(KeyCode.L)) {
				level.RandomizeTiles ();
			}

			if (Input.GetKeyDown(KeyCode.S)) {
				levelSerializer.SaveLevel (level);
			}
		}

		// CONTROLLER GETTERS
		public CursorController GetCursorController() {
			return cursorController;
		}

		public TileSpriteObserver GetTileSpriteManager() {
			return tileSpriteObserver;
		}
		// END CONTROLLER GETTERS
	}

}