using UnityEngine;
using UnityEngine.UI;

using com.gStudios.levelEditor.controller.cursor;

namespace com.gStudios.levelEditor.controller.ui {

	public class GenericSelectionButton : MonoBehaviour {

        CursorController cursorController;
        int index;

        bool isInitialized = false;

        public void Init(CursorController cursorController, int index, Sprite sprite) {
            this.cursorController = cursorController;
            this.index = index;

            gameObject.name = "Generic Selection Button";

            Button btn = GetComponent<Button>();
            btn.onClick.AddListener(OnClick);

            Image img = GetComponent<Image>();
            img.sprite = sprite;

            isInitialized = true;
        }

        void OnClick() {
            if (!isInitialized)
                Debug.LogError("Clicking on an uninitialized button.");

            cursorController.SetIndex(index);
        }

    }

}