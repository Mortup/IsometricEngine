using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;

using com.gStudios.isometric.controller.data;
using com.gStudios.isometric.controller.cursor;
using com.gStudios.isometric.controller.spriteObservers;

using com.gStudios.isometric.model.saving;
using com.gStudios.isometric.model.world;

namespace com.gStudios.isometric.controller {

	public class LevelController : MonoBehaviour {

		[SerializeField] int levelWidth;
		[SerializeField] int levelHeight;

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

			levelSerializer = new LevelSerializer ();
			LoadLevel ();
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

			if (Input.GetKeyDown(KeyCode.P)) {
				wallSpriteObserver.SetClipping (true);
			}
			if (Input.GetKeyDown(KeyCode.O)) {
				wallSpriteObserver.SetClipping (false);
			}
		}

		void LoadLevel() {
			tileSpriteObserver.RemoveTiles ();
			wallSpriteObserver.RemoveWalls ();

			Level level;
			if (levelSerializer.ExistsSavedLevel ()) {
				level = levelSerializer.LoadLevel ();
			}
			else {
				level = new Level(levelWidth, levelHeight);
			}

			cursorController.Init (level);

			tileSpriteObserver.BindLevel (level);
			wallSpriteObserver.BindLevel (level);
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