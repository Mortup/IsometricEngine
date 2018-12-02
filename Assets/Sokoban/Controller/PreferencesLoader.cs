using UnityEngine;

public class PreferencesLoader : MonoBehaviour {

    private void Awake() {
        int horInv = PlayerPrefs.GetInt("HI", 0);
        ButtonsPosition.horizontalInverted = horInv > 0;

        int soundOn = PlayerPrefs.GetInt("SO", 0);
        SoundChooser.soundOn = soundOn > 0;
    }
}
