using UnityEngine;

namespace com.gStudios.levelEditor.controller.ui {

	public class BuildModes : MonoBehaviour {

		[SerializeField] GameObject rightPanel;

		void Update() {
            // Reset UI when loading a level.
			if (Input.GetKeyDown(KeyCode.L)) {
				ResetUI ();
			}
		}

		void ResetUI() {
			rightPanel.SetActive (false);
		}
	}

}