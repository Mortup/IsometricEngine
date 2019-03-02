using System.Collections.Generic;
using UnityEngine;

using com.gStudios.isometric.controller.data;
using com.gStudios.isometric.controller.isometricTransform;

using com.gStudios.isometric.model.world;
using com.gStudios.isometric.model.world.wall;

namespace com.gStudios.isometric.controller.spriteObservers {

	public class WallSpriteObserver : IWallObserver, IOrientationObserver {

        Level level;
		GameObject wallHolder;

		Dictionary<IWall, GameObject> gameobjects;

        public enum ClippingMode {
            NO_CLIP = 1,
            FULL_CLIPPING = 2,
            FRONT_CLIPPING = 3
        }

        private ClippingMode currentClipping = ClippingMode.FULL_CLIPPING;

		public WallSpriteObserver() {
			wallHolder = new GameObject ("Walls");

			gameobjects = new Dictionary<IWall, GameObject> ();

            OrientationManager.RegisterObserver(this);

		}

        ~WallSpriteObserver() {
            OrientationManager.UnregisterObserver(this);
        }

        GameObject CreateSprite(IWall wall) {
            GameObject wall_go = new GameObject {
                name = "Wall [" + wall.X.ToString() + "," + wall.Y.ToString() + "," + wall.Z.ToString() + "]"
            };
			
			wall_go.transform.SetParent (wallHolder.transform, true);

			SpriteRenderer sr = wall_go.AddComponent<SpriteRenderer> ();
			sr.sortingLayerName = "Tiles";

			wall.Subscribe (this);
			gameobjects.Add (wall, wall_go);

            UpdateSprite(wall);
            return wall_go;
		}

		public void NotifyWallTypeChanged(IWall wall) {
            // Update wall
			UpdateSprite (wall);

            // Update neighbors
            UpdateSprite(wall.GetNeighbor(0, 0, 0));
            UpdateSprite(wall.GetNeighbor(0, 0, 1));
            UpdateSprite(wall.GetNeighbor(0, 1, 0));
            UpdateSprite(wall.GetNeighbor(0, 1, 1));
            UpdateSprite(wall.GetNeighbor(0, -1, 1));
            UpdateSprite(wall.GetNeighbor(1, 0, 0));
            UpdateSprite(wall.GetNeighbor(1, 0, 1));
            UpdateSprite(wall.GetNeighbor(1, -1, 1));
            UpdateSprite(wall.GetNeighbor(-1, 0, 0));
            UpdateSprite(wall.GetNeighbor(-1, 1, 0));
        }

        void UpdateSprite(IWall wall) {
            if (gameobjects.ContainsKey(wall) == false)
                return;

            GameObject wall_go = gameobjects[wall];

            wall_go.transform.position = WallTransformer.CoordToWorld(wall.X, wall.Y, wall.Z);

            SpriteRenderer sr = wall_go.GetComponent<SpriteRenderer> ();

			sr.sprite = DataManager.wallSpriteData.GetDataById(wall.Type).GetSprite(wall, GetClippingForWall(wall));
            sr.sortingOrder = SortingOrders.WallOrder(wall.X, wall.Y, wall.Z, TileSubLayer.Wall);

            //sr.color = Random.ColorHSV(0, 1, 0, 1, 0.4f, 1);
        }

		void UpdateAllSprites() {
			foreach(KeyValuePair<IWall, GameObject> entry in gameobjects)
			{
				UpdateSprite (entry.Key);
			}
		}

		public void RemoveWalls() {

			foreach(KeyValuePair<IWall, GameObject> entry in gameobjects)
			{
				GameObject.Destroy (entry.Value);
			}
			gameobjects = new Dictionary<IWall, GameObject> ();
		}

		public void BindLevel(Level level) {
            this.level = level;
			
			for (int x = 0; x < level.Width+1; x++) {
				for (int y = 0; y < level.Height+1; y++) {
					CreateSprite(level.GetWallAt(x,y,0));
					CreateSprite(level.GetWallAt(x,y,1));
				}
			}

		}

        public void NotifyOrientationChanged(Orientation previousOrientation, Orientation newOrientation) {
            UpdateAllSprites();
        }

        private bool GetClippingForWall(IWall wall) {
            Vector3Int rotatedPos = WallTransformer.InverseRotateInsideTile(new Vector3Int(wall.X, wall.Y, wall.Z));

            if (wall.IsEmpty() == false) {
                Vector2Int offset = TileTransformer.RotateCoord(new Vector2Int(0, 0));
                level.GetTileAt(rotatedPos.x, rotatedPos.y).Type = 0;
                level.GetTileAt(wall.X, wall.Y).Type = 1;
            }

            return true;
        }

        public ClippingMode CurrentClipping {
            get {
                return currentClipping;
            }
            set {
                ClippingMode lastMode = currentClipping;
                currentClipping = value;

                if (lastMode != currentClipping) {
                    UpdateAllSprites();
                }
            }
        }
    }

}