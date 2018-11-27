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
        SoundChooser.soundOn = toggle.isOn;
    }

}
