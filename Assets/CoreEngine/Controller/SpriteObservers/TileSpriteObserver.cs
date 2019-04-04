using System.Collections.Generic;

using UnityEngine;

using com.gStudios.isometric.controller.isometricTransform;
using com.gStudios.isometric.controller.data;

using com.gStudios.isometric.model.world;
using com.gStudios.isometric.model.world.tile;

namespace com.gStudios.isometric.controller.spriteObservers {

	public class TileSpriteObserver : ITileObserver, IOrientationObserver {

		GameObject tileHolder;

		Dictionary<ITile, GameObject> gameobjects;

		public TileSpriteObserver() {
			tileHolder = new GameObject ("Tiles");

			gameobjects = new Dictionary<ITile, GameObject> ();

            OrientationManager.RegisterObserver(this);
		}

        public void StopObserving() {
            OrientationManager.UnregisterObserver(this);
        }

		GameObject CreateSprite(ITile tile) {
			GameObject tile_go = new GameObject ();
			tile_go.name = "Tile [" + tile.X.ToString () + "," + tile.Y.ToString () + "]";
			tile_go.transform.SetParent (tileHolder.transform, true);

			SpriteRenderer sr = tile_go.AddComponent<SpriteRenderer> ();
			sr.sortingLayerName = "Floor";

            gameobjects.Add(tile, tile_go);
            UpdateSprite(tile);

            return tile_go;
		}

		public void NotifyTileTypeChanged(ITile tile) {
			UpdateSprite (tile);
		}

        public void NotifyOrientationChanged(Orientation previousOrientation, Orientation newOrientation) {
            UpdateAllSprites();
        }

		void UpdateSprite(ITile tile) {
            if (gameobjects.ContainsKey(tile) == false) {
                Debug.LogError("Trying to update a tile without a gameobject created.");
                return;
            }

            GameObject tile_go = gameobjects[tile];

            tile_go.transform.position = (Vector3)TileTransformer.CoordToWorld(tile.X, tile.Y);
            
            SpriteRenderer sr = tile_go.GetComponent<SpriteRenderer>();
			sr.sprite = DataManager.tileSpriteData.GetDataById(tile.Type);
            sr.sortingOrder = SortingOrders.FloorOrder(tile.X, tile.Y, FloorSubLayer.FloorTile);
        }

        void UpdateAllSprites() {
            foreach (KeyValuePair<ITile, GameObject> entry in gameobjects) {
                UpdateSprite(entry.Key);
            }
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

            level.SubscribeToTileChanges(this);
		}
	}

}