using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.gStudios.isometric.controller.camera {

	public class PixelPerfectCamera : MonoBehaviour {

		Camera cam;

		const int PPU = 64;

		void Awake() {
			cam = GetComponent<Camera> ();
		}

		void Start() {
			cam.orthographicSize = Screen.height / PPU * 0.5f;
		}
		
	}

}