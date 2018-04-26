using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using com.gStudios.isometric.controller.data;
using com.gStudios.isometric.controller.cursor;
using com.gStudios.isometric.controller.loaders;
using com.gStudios.isometric.model.data.structures;

namespace com.gStudios.isometric.controller.ui {

	public class RightPanel : MonoBehaviour {

		[SerializeField] LevelController levelController;
		[SerializeField] GameObject buttonPrefab;

		CursorController cursorController;
		TileSpriteManager tileSpriteManager;
		List<GameObject> childs;

		void Awake() {
			childs = new List<GameObject>();

			cursorController = levelController.GetCursorController ();
			tileSpriteManager = levelController.GetTileSpriteManager ();
		}

		public void ShowFloorButtons() {
			RemoveChilds ();

			List<FloorData> floorDatas = DataLoader.GetFloors ();
			foreach (FloorData fd in floorDatas) {
				GameObject button = GameObject.Instantiate (buttonPrefab);
				button.transform.SetParent (transform);

				SelectionButton sb = button.GetComponent<SelectionButton> ();
				sb.Init (fd, tileSpriteManager.GetSprite(fd), cursorController);
			}
		}

		public void RemoveChilds() {
			while (childs.Count > 0) {
				Destroy (childs [0]);
			}
		}
		
	}

}