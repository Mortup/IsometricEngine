using UnityEngine;
using UnityEngine.UI;

public class PositionButton : MonoBehaviour {

    [SerializeField] private bool isLeft;
    private Button btn;

    public void Awake() {
        btn = GetComponent<Button>();
    }

    public void Update() {
        btn.interactable = isLeft != ButtonsPosition.horizontalInverted;
    }

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
