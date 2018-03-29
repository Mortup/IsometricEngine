using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;

using com.gStudios.isometric.model.world;

namespace com.gStudios.isometric.controller {

	public class LevelController : MonoBehaviour {

		public Sprite floorSprite;

		Level level;
		TileSpriteManager tileSpriteManager;

		void Start () {
			tileSpriteManager = new TileSpriteManager ();

			level = new Level (50,50);
			level.RandomizeTiles ();

			// Create a GameObject for each of out tiles.
			for (int x = 0; x < level.Width; x++) {
				for (int y = 0; y < level.Height; y++) {
					tileSpriteManager.CreateSprite (level.GetTileAt(x,y));
				}
			}
		}
		
		void Update () {
			if (Input.GetKeyDown(KeyCode.L)) {
				level.RandomizeTiles ();
			}
		}

		void OnTileTypeChanged(Tile tile_data, GameObject tile_go) {
			if (tile_data.Type == Tile.TileType.Floor) {
				tile_go.GetComponent<SpriteRenderer> ().sprite = floorSprite;
			}
			else {
				tile_go.GetComponent<SpriteRenderer> ().sprite = null;
			}

			// TODO: Add a unrecognized tile type error.
		}
	}

}