using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using com.gStudios.isometric.controller;
using com.gStudios.isometric.model.world;

namespace com.gStudios {

	public class TileSpriteManager : ITileObserver {

		Dictionary<Tile, GameObject> gameobjects;

		public TileSpriteManager() {
			gameobjects = new Dictionary<Tile, GameObject> ();
		}

		public GameObject CreateSprite(Tile tile) {
			GameObject tile_go = new GameObject ();
			tile_go.name = "Tile [" + tile.X.ToString () + "," + tile.Y.ToString () + "]";
			tile_go.transform.position = (Vector3)IsometricTransformer.CoordToScreen (tile.X, tile.Y);

			SpriteRenderer sr = tile_go.AddComponent<SpriteRenderer> ();
			sr.sortingOrder = tile.X + tile.Y;
			UpdateSprite (tile, tile_go);

			tile.Subscribe (this);
			gameobjects.Add (tile, tile_go);
			return tile_go;
		}

		public void NotifyTileTypeChanged(Tile tile) {
			UpdateSprite (tile, gameobjects [tile]);
		}

		public void UpdateSprite(Tile tile, GameObject tile_go) {
			SpriteRenderer sr = tile_go.GetComponent<SpriteRenderer> ();

			if (tile.Type == Tile.TileType.Empty)
				sr.sprite = null;
			else {
				sr.sprite = Resources.Load<Sprite> ("Sprites/Tiles/Default");
			}
		}

	}

}