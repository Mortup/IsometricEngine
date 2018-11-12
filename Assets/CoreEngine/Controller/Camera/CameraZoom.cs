using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.gStudios.isometric.controller.camera {

	public class CameraZoom : MonoBehaviour {

		Camera cam;

		void Awake() {
			cam = GetComponent<Camera> ();
		}

		void Start() {
			cam.orthographicSize = 12f;
		}

		void Update() {
			float newSize = cam.orthographicSize * Mathf.Pow(2, -Input.mouseScrollDelta.y);
			newSize = Mathf.Max (1f, newSize);
			newSize = Mathf.Min (16f, newSize);

			cam.orthographicSize = newSize;
		}
	}

}