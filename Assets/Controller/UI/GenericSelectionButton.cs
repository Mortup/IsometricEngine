using UnityEngine;
using UnityEngine.UI;

using com.gStudios.isometric.controller.cursor;

namespace com.gStudios.isometric.controller.ui {

	public class GenericSelectionButton : MonoBehaviour {

        CursorController cursorController;
        int index;

        public void Init(CursorController cursorController, int index, Sprite sprite) {
            this.cursorController = cursorController;
            this.index = index;

            gameObject.name = "Generic Selection Button";

            Button btn = GetComponent<Button>();
            btn.onClick.AddListener(OnClick);

            Image img = GetComponent<Image>();
            img.sprite = sprite;
        }

        void OnClick() {
            cursorController.SetIndex(index);
        }

    }

}