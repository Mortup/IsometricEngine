using System.Collections.Generic;
using UnityEngine;

using com.gStudios.isometric.controller.data;
using com.gStudios.isometric.controller.isometricTransform;

using com.gStudios.isometric.model.world;
using com.gStudios.isometric.model.world.wall;
using com.gStudios.isometric.model.world.tile;

namespace com.gStudios.isometric.controller.spriteObservers {

	public class WallSpriteObserver : ITileObserver, IWallObserver, IOrientationObserver {

        Level level;
		GameObject wallHolder;
        DeferredCaller deferredCaller;

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

            deferredCaller = GameObject.FindObjectOfType<DeferredCaller>();
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

			gameobjects.Add (wall, wall_go);

            UpdateSprite(wall);
            return wall_go;
		}

        public void NotifyTileTypeChanged(ITile tile) {
            deferredCaller.CallLimitedToOncePerFrame(UpdateAllSprites);
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

            level.SubscribeToTileChanges(this);
            level.SubscribeToWallChanges(this);
		}

        public void NotifyOrientationChanged(Orientation previousOrientation, Orientation newOrientation) {
            UpdateAllSprites();
        }

        private bool GetClippingForWall(IWall wall) {
            if(wall.Type == WallIndex.Empty) {
                return true;
            }
            if (currentClipping == ClippingMode.FULL_CLIPPING) {
                return true;
            }
            if (currentClipping == ClippingMode.NO_CLIP) {
                return false;
            }
            Vector2Int baseTileCoords = WallTransformer.GetRotatedWallBase(new Vector3Int(wall.X, wall.Y, wall.Z));
            Vector3Int rotatedWallCoords = WallTransformer.RotateInsideTile(new Vector3Int(wall.X, wall.Y, wall.Z));
            ITile baseTile = level.GetTileAt(baseTileCoords.x, baseTileCoords.y);
            List<ITile> tiles = new List<ITile>();

            if (rotatedWallCoords.z == 1) {

                //The tiles checked for wall clipping,
                Vector2Int rotatedOffset1 = TileTransformer.InverseRotateCoord(new Vector2Int(-1, 0));
                Vector2Int rotatedOffSet2 = TileTransformer.InverseRotateCoord(new Vector2Int(-1, -1));
                Vector2Int rotatedOffSet3 = TileTransformer.InverseRotateCoord(new Vector2Int(-2, -1));
                Vector2Int rotatedOffset4 = TileTransformer.InverseRotateCoord(new Vector2Int(-2, -2));
                Vector2Int rotatedOffset5 = TileTransformer.InverseRotateCoord(new Vector2Int(-3, -2));
                Vector2Int rotatedOffSet6 = TileTransformer.InverseRotateCoord(new Vector2Int(-3, -3));
                Vector2Int rotatedOffSet7 = TileTransformer.InverseRotateCoord(new Vector2Int(-4, 3));



                tiles.Add(level.GetTileAt(baseTile.X + rotatedOffset1.x, baseTile.Y + rotatedOffset1.y));
                tiles.Add(level.GetTileAt(baseTile.X + rotatedOffSet2.x, baseTile.Y + rotatedOffSet2.y));
                tiles.Add(level.GetTileAt(baseTile.X + rotatedOffSet3.x, baseTile.Y + rotatedOffSet3.y));
                tiles.Add(level.GetTileAt(baseTile.X + rotatedOffset4.x, baseTile.Y + rotatedOffset4.y));
                tiles.Add(level.GetTileAt(baseTile.X + rotatedOffset5.x, baseTile.Y + rotatedOffset5.y));
                tiles.Add(level.GetTileAt(baseTile.X + rotatedOffSet6.x, baseTile.Y + rotatedOffSet6.y));
                tiles.Add(level.GetTileAt(baseTile.X + rotatedOffSet7.x, baseTile.Y + rotatedOffSet7.y));
            }
            else {
                Vector2Int rotatedOffset1 = TileTransformer.InverseRotateCoord(new Vector2Int(0, -1));
                Vector2Int rotatedOffSet2 = TileTransformer.InverseRotateCoord(new Vector2Int(-1, -1));
                Vector2Int rotatedOffSet3 = TileTransformer.InverseRotateCoord(new Vector2Int(-1, -2));
                Vector2Int rotatedOffset4 = TileTransformer.InverseRotateCoord(new Vector2Int(-2, -2));
                Vector2Int rotatedOffset5 = TileTransformer.InverseRotateCoord(new Vector2Int(-2, -3));
                Vector2Int rotatedOffSet6 = TileTransformer.InverseRotateCoord(new Vector2Int(-3, -3));
                Vector2Int rotatedOffSet7 = TileTransformer.InverseRotateCoord(new Vector2Int(-3, -4));

                tiles.Add(level.GetTileAt(baseTile.X + rotatedOffset1.x, baseTile.Y + rotatedOffset1.y));
                tiles.Add(level.GetTileAt(baseTile.X + rotatedOffSet2.x, baseTile.Y + rotatedOffSet2.y));
                tiles.Add(level.GetTileAt(baseTile.X + rotatedOffSet3.x, baseTile.Y + rotatedOffSet3.y));
                tiles.Add(level.GetTileAt(baseTile.X + rotatedOffset4.x, baseTile.Y + rotatedOffset4.y));
                tiles.Add(level.GetTileAt(baseTile.X + rotatedOffset5.x, baseTile.Y + rotatedOffset5.y));
                tiles.Add(level.GetTileAt(baseTile.X + rotatedOffSet6.x, baseTile.Y + rotatedOffSet6.y));
                tiles.Add(level.GetTileAt(baseTile.X + rotatedOffSet7.x, baseTile.Y + rotatedOffSet7.y));

            }
            foreach (ITile tile in tiles)
            {

                if (tile.Type != TileIndex.Empty)
                {
                    return true;
                }
            }
            return false;
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