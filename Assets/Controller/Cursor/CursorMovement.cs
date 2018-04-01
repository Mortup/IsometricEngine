using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using com.gStudios.isometric.controller;

namespace com.gStudios.isometric.controller.cursor {

	public class CursorMovement : MonoBehaviour {

		void Update() {
			Vector2Int coords = IsometricTransformer.ScreenToCoord (Input.mousePosition);
			transform.position = IsometricTransformer.CoordToWorld (coords.x, coords.y);
		}
		
	}

}