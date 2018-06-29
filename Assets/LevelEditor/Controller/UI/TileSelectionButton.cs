using UnityEngine;
using UnityEngine.UI;

using com.gStudios.isometric.model.data.structures;

using com.gStudios.levelEditor.controller.cursor;

namespace com.gStudios.levelEditor.controller.ui {

	public class TileSelectionButton : MonoBehaviour {

		CursorController cursorController;
		TileData floorData;

		public void Init(TileData floorData, Sprite sprite, CursorController cursorController) {
			this.cursorController = cursorController;
			this.floorData = floorData;

			gameObject.name = floorData.name + " Selection Button";

			Button btn = GetComponent<Button> ();
			btn.onClick.AddListener (OnClick);

			Image img = GetComponent<Image> ();
			img.sprite = sprite;
		}

		void OnClick() {
			cursorController.SetIndex (floorData.id);
		}
		
	}

}