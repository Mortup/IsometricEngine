using UnityEngine;

public class ButtonsPosition : MonoBehaviour {

    public static bool horizontalInverted = false;

    [SerializeField] private GameObject[] regularControls;
    [SerializeField] private GameObject[] invertedControls;

    public void Start() {
        UpdateControlsPosition();
    }

    public void UpdateControlsPosition() {
        foreach (GameObject go in regularControls) {
            go.SetActive(!horizontalInverted);
        }

        foreach (GameObject go in invertedControls) {
            go.SetActive(horizontalInverted);
        }
    }

}
