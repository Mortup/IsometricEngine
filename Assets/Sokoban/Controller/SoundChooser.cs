using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundChooser : MonoBehaviour {

    public static bool soundOn = true;

    [SerializeField] AudioClip movement;

    AudioSource source;

    private void Awake() {
        source = GetComponent<AudioSource>();
    }

    public void Play(string soundName) {
        if (!soundOn)
            return;

        source.pitch = Random.Range(0.98f, 1.02f);

        switch(soundName) {
            case "movement":
                source.clip = movement;
                source.Play();
                break;
            default:
                Debug.LogError("Unkown sound to play");
                break;
        }
    }
    
}
