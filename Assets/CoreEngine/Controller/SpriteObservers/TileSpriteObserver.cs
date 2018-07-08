using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using com.gStudios.isometric.controller;
using com.gStudios.isometric.controller.isometricTransform;
using com.gStudios.isometric.controller.data;

using com.gStudios.isometric.model.world;
using com.gStudios.isometric.model.world.tile;

namespace com.gStudios.isometric.controller.spriteObservers {

	public class TileSpriteObserver : ITileObserver {

		GameObject tileHolder;

		Dictionary<ITile, GameObject> gameobjects;

		public TileSpriteObserver() {
			tileHolder = new GameObject ("Tiles");

			gameobjects = new Dictionary<ITile, GameObject> ();
		}

		GameObject CreateSprite(ITile tile) {
			GameObject tile_go = new GameObject ();
			tile_go.name = "Tile [" + tile.X.ToString () + "," + tile.Y.ToString () + "]";
			tile_go.transform.position = (Vector3)TileTransformer.CoordToWorld (tile.X, tile.Y);
			tile_go.transform.SetParent (tileHolder.transform, true);

			SpriteRenderer sr = tile_go.AddComponent<SpriteRenderer> ();
			sr.sortingLayerName = "Floor";
			sr.sortingOrder = GetSortingOrder(tile.X, tile.Y, FloorSubLayer.FloorTile);
			UpdateSprite (tile, tile_go);

			tile.Subscribe (this);
			gameobjects.Add (tile, tile_go);
			return tile_go;
		}

		public void NotifyTileTypeChanged(ITile tile) {
			UpdateSprite (tile, gameobjects [tile]);
		}

		public void UpdateSprite(ITile tile, GameObject tile_go) {
			SpriteRenderer sr = tile_go.GetComponent<SpriteRenderer> ();				

			sr.sprite = DataManager.tileSpriteData.GetDataById(tile.Type);
		}

		public void RemoveTiles() {

			foreach(KeyValuePair<ITile, GameObject> entry in gameobjects)
			{
				GameObject.Destroy (entry.Value);
			}
			gameobjects = new Dictionary<ITile, GameObject> ();
		}

		public void BindLevel(Level level) {
			for (int x = 0; x < level.Width; x++) {
				for (int y = 0; y < level.Height; y++) {
					CreateSprite (level.GetTileAt(x,y));
				}
			}
		}

		public static int GetSortingOrder(int x, int y, FloorSubLayer layer) {
            return ((x + y) * 10) + (int) layer;
		}
	}

}