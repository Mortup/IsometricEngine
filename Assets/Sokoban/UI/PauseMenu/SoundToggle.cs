using UnityEngine;
using UnityEngine.UI;

public class SoundToggle : MonoBehaviour {

    private Toggle toggle;

    private void Awake() {
        toggle = GetComponent<Toggle>();
    }

    void Start () {
        toggle.isOn = SoundChooser.soundOn;
	}

    private void Update() {
        bool last = SoundChooser.soundOn;

        SoundChooser.soundOn = toggle.isOn;

        if (last != SoundChooser.soundOn) {
            if (SoundChooser.soundOn) {
                PlayerPrefs.SetInt("SO", 1);
            }
            else {
                PlayerPrefs.SetInt("SO", 0);
            }
        }
    }

}
