using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace com.gStudios.isometric.controller.camera {

	public class CameraDrag : MonoBehaviour {

		Camera cam;

		bool isDragging;
		Vector2 lastMousePosition;

		int dragButton = 1;

		void Awake() {
			cam = gameObject.GetComponent<Camera> ();
		}

		void Update() {
			if (isDragging) {
				cam.transform.position += (Vector3) (lastMousePosition - (Vector2)(cam.ScreenToWorldPoint (Input.mousePosition)));

				if (!Input.GetMouseButton(dragButton)) {
					isDragging = false;
				}
			}
			else {
				if (Input.GetMouseButtonDown(dragButton) && !EventSystem.current.IsPointerOverGameObject()) {
					isDragging = true;
					lastMousePosition = (Vector2)(cam.ScreenToWorldPoint(Input.mousePosition));
				}
			}
		}
		
	}

}