using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using com.gStudios.isometric.controller.data;
using com.gStudios.isometric.model.world;

namespace com.gStudios.isometric.controller.spriteObservers {

	public class WallSpriteObserver : IWallObserver {

		GameObject wallHolder;

		Dictionary<Wall, GameObject> gameobjects;

		public WallSpriteObserver() {
			wallHolder = new GameObject ("Walls");

			gameobjects = new Dictionary<Wall, GameObject> ();
		}

		public GameObject CreateSprite(Wall wall) {
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

		public void NotifyWallTypeChanged(Wall wall) {
			UpdateSprite (wall, gameobjects [wall]);
		}

		public void UpdateSprite(Wall wall, GameObject wall_go) {
			SpriteRenderer sr = wall_go.GetComponent<SpriteRenderer> ();

			if (wall.Type >= DataManager.wallSpriteData.GetData().Length)
				Debug.LogError ("Can't find a sprite for wall with ID: " + wall.Type.ToString ());

			sr.sprite = DataManager.wallSpriteData.GetDataById(wall.Type);
		}

		public void RemoveWalls() {

			foreach(KeyValuePair<Wall, GameObject> entry in gameobjects)
			{
				GameObject.Destroy (entry.Value);
			}
			gameobjects = new Dictionary<Wall, GameObject> ();
		}

		public static int GetSortingOrder(int x, int y, int z) {
			return TileSpriteObserver.GetSortingOrder (x, y) + 1;
		}
	}

}