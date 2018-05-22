using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using com.gStudios.isometric.controller.data;
using com.gStudios.isometric.model.world;
using com.gStudios.isometric.model.world.wall;

namespace com.gStudios.isometric.controller.spriteObservers {

	public class WallSpriteObserver : IWallObserver {

		GameObject wallHolder;

		Dictionary<IWall, GameObject> gameobjects;

		int currentClipping = 1;

		public WallSpriteObserver() {
			wallHolder = new GameObject ("Walls");

			gameobjects = new Dictionary<IWall, GameObject> ();
		}

		public GameObject CreateSprite(IWall wall) {
			GameObject wall_go = new GameObject ();
			wall_go.name = "Wall [" + wall.X.ToString () + "," + wall.Y.ToString () + "," + wall.Z.ToString () + "]";
			wall_go.transform.position = (Vector3)IsometricTransformer.WallPosToWorld(wall.X, wall.Y, wall.Z);
			wall_go.transform.SetParent (wallHolder.transform, true);

			SpriteRenderer sr = wall_go.AddComponent<SpriteRenderer> ();
			sr.sortingLayerName = "Tiles";
			sr.sortingOrder = GetSortingOrder(wall.X, wall.Y, wall.Z);
			UpdateSprite (wall, wall_go);

			wall.Subscribe (this);
			gameobjects.Add (wall, wall_go);
			return wall_go;
		}

		public void NotifyWallTypeChanged(IWall wall) {
			UpdateSprite (wall, gameobjects [wall]);
		}

		public void UpdateSprite(IWall wall, GameObject wall_go) {
			SpriteRenderer sr = wall_go.GetComponent<SpriteRenderer> ();

			sr.sprite = DataManager.wallSpriteData.GetDataById(wall.Type, wall.Z, currentClipping);
		}

		public void UpdateAllSprites() {
			foreach(KeyValuePair<IWall, GameObject> entry in gameobjects)
			{
				UpdateSprite (entry.Key, entry.Value);
			}
		}

		public void SetClipping(int clipping) {
			int lastClipping = currentClipping;
			currentClipping = clipping;

			if (lastClipping != currentClipping)
				UpdateAllSprites ();
		}

		public void RemoveWalls() {

			foreach(KeyValuePair<IWall, GameObject> entry in gameobjects)
			{
				GameObject.Destroy (entry.Value);
			}
			gameobjects = new Dictionary<IWall, GameObject> ();
		}

		public static int GetSortingOrder(int x, int y, int z) {
			return TileSpriteObserver.GetSortingOrder (x, y) + 1;
		}
	}

}