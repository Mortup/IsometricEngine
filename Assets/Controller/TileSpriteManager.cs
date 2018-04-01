using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using com.gStudios.isometric.controller;
using com.gStudios.isometric.model.world;

using com.gStudios.isometric.controller.config;

namespace com.gStudios {

	public class TileSpriteManager : ITileObserver {

		GameObject tileHolder;

		Dictionary<Tile, GameObject> gameobjects;

		Sprite defaultSprite = Resources.Load<Sprite> (Paths.TileSprite("Default"));

		public TileSpriteManager() {
			tileHolder = new GameObject ("Tiles");

			gameobjects = new Dictionary<Tile, GameObject> ();
		}

		public GameObject CreateSprite(Tile tile) {
			GameObject tile_go = new GameObject ();
			tile_go.name = "Tile [" + tile.X.ToString () + "," + tile.Y.ToString () + "]";
			tile_go.transform.position = (Vector3)IsometricTransformer.CoordToWorld (tile.X, tile.Y);
			tile_go.transform.SetParent (tileHolder.transform, true);

			SpriteRenderer sr = tile_go.AddComponent<SpriteRenderer> ();
			sr.sortingLayerName = "Tiles";
			sr.sortingOrder = GetSortingOrder(tile.X, tile.Y);
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

			if (tile.Type == 0)
				sr.sprite = null;
			else {
				sr.sprite = defaultSprite;
			}
		}

		public static int GetSortingOrder(int x, int y) {
			return (x + y) * 10;
		}
	}

}