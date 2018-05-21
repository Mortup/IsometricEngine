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
		WallSpriteObserver wallSpriteObserver;

		LevelSerializer levelSerializer;

		void Start () {
			DataManager.Init ();

			cursorController = GetComponent<CursorController> ();
			tileSpriteObserver = new TileSpriteObserver ();
			wallSpriteObserver = new WallSpriteObserver ();

			levelSerializer = new LevelSerializer (tileSpriteObserver, wallSpriteObserver);
			LoadLevel ();

			cursorController.Init (level);
		}
		
		void Update () {
			if (Input.GetKeyDown(KeyCode.R)) {
				level.RandomizeWalls ();
			}

			if (Input.GetKeyDown(KeyCode.S)) {
				levelSerializer.SaveLevel (level);
			}

			if (Input.GetKeyDown(KeyCode.L)) {
				LoadLevel ();
			}
		}

		void LoadLevel() {
			tileSpriteObserver.RemoveTiles ();
			wallSpriteObserver.RemoveWalls ();

			level = levelSerializer.LoadLevel ();

			cursorController.Init (level);
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