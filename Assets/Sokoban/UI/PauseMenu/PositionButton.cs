using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionButton : MonoBehaviour {

	public void SetInvertedControls(bool val) {
        ButtonsPosition.horizontalInverted = val;

        if (val) {
            PlayerPrefs.SetInt("HI", 1);
        }
        else {
            PlayerPrefs.SetInt("HI", 0);
        }
    }
}
