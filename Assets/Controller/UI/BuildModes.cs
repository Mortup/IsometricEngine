﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.gStudios.isometric.controller.ui {

	public class BuildModes : MonoBehaviour {

		[SerializeField] GameObject rightPanel;

		void Update() {
			if (Input.GetKeyDown(KeyCode.L)) {
				ResetUI ();
			}
		}

		void ResetUI() {
			rightPanel.SetActive (false);
		}
	}

}