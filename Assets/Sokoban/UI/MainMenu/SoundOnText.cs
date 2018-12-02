using UnityEngine;
using UnityEngine.UI;

public class SoundOnText : MonoBehaviour {

    private Text text;
	
	void Awake () {
        text = GetComponent<Text>();
	}
	
	void Update () {
		if (SoundChooser.soundOn) {
            text.text = "Sound is on";
        }
        else {
            text.text = "Sound is off";
        }
	}
}
