using System;
using System.Collections.Generic;
using UnityEngine;

using com.gStudios.levelEditor.controller.cursor;
using com.gStudios.isometric.controller.data;
using com.gStudios.isometric.controller.data.structs;

using com.gStudios.isometric.model.data.structures;

namespace com.gStudios.levelEditor.controller.ui {

	public class RightPanel : MonoBehaviour {

		[SerializeField] CursorController cursorController;
        [SerializeField] GameObject buttonPrefab;

		
		List<GameObject> childs;

		void Awake() {
			childs = new List<GameObject>();
        }

		public void ShowFloorButtons() {
			RemoveChilds ();

            for (int i = 2; i < DataManager.tileSpriteData.GetLength(); i++) {
                Sprite sprite = DataManager.tileSpriteData.GetDataById(i);

                GameObject button = GameObject.Instantiate(buttonPrefab);
                childs.Add(button);
                button.transform.SetParent(transform);

                GenericSelectionButton gsb = button.AddComponent<GenericSelectionButton>();
                gsb.Init(cursorController, i, sprite);
            }
		}

        public void ShowWallButtons() {
            RemoveChilds();

            for (int i = 1; i < DataManager.wallSpriteData.GetLength(); i++) {
                IWallSprite wallSprite = DataManager.wallSpriteData.GetDataById(i);

                GameObject button = GameObject.Instantiate(buttonPrefab);
                childs.Add(button);
                button.transform.SetParent(transform);

                GenericSelectionButton gsb = button.AddComponent<GenericSelectionButton>();
                gsb.Init(cursorController, i, wallSprite.GetThumbnail());
            }
        }

		public void RemoveChilds() {
			while (childs.Count > 0) {
				Destroy (childs [0]);
				childs.Remove (childs [0]);
			}
		}
		
	}

}