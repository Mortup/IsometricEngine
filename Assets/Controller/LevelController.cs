using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;

using com.gStudios.isometric.controller.data;
using com.gStudios.isometric.controller.cursor;
using com.gStudios.isometric.controller.spriteObservers;
using com.gStudios.isometric.model.world;

namespace com.gStudios.isometric.controller {

	public class LevelController : MonoBehaviour {

		Level level;
		CursorController cursorController;
		TileSpriteObserver tileSpriteManager;

		void Start () {
			TileDataLoader.Init ();
			tileSpriteManager = new TileSpriteObserver ();

			level = new Level (50,50);
			level.RandomizeTiles ();

			// Create a GameObject for each of out tiles.
			for (int x = 0; x < level.Width; x++) {
				for (int y = 0; y < level.Height; y++) {
					tileSpriteManager.CreateSprite (level.GetTileAt(x,y));
				}
			}

			cursorController = GetComponent<CursorController> ();
			cursorController.Init (level);
		}
		
		void Update () {
			if (Input.GetKeyDown(KeyCode.L)) {
				level.RandomizeTiles ();
			}
		}

		// CONTROLLER GETTERS
		public CursorController GetCursorController() {
			return cursorController;
		}

		public TileSpriteObserver GetTileSpriteManager() {
			return tileSpriteManager;
		}
		// END CONTROLLER GETTERS
	}

}