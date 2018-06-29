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

			List<TileData> floorDatas = DataManager.tileData.GetData ();
			foreach (TileData fd in floorDatas) {
				GameObject button = GameObject.Instantiate (buttonPrefab);
				childs.Add (button);
				button.transform.SetParent (transform);

				TileSelectionButton sb = button.AddComponent<TileSelectionButton> ();
				sb.Init (fd, DataManager.tileSpriteData.GetDataById(fd.id), cursorController);
			}
		}

        public void ShowWallButtons() {
            RemoveChilds();

            int i = 1;
            while(true) {
                try {
                    IWallSprite wallSprite = DataManager.wallSpriteData.GetDataById(i);

                    GameObject button = GameObject.Instantiate(buttonPrefab);
                    childs.Add(button);
                    button.transform.SetParent(transform);

                    GenericSelectionButton gsb = button.AddComponent<GenericSelectionButton>();
                    gsb.Init(cursorController, i, wallSprite.GetThumbnail());
                }
                catch (ArgumentOutOfRangeException e) when (e.ParamName == "index") {
                    break;
                }
                
                i++;
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