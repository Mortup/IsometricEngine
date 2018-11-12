using UnityEngine;
using UnityEngine.UI;

public class ButtonsContainer : MonoBehaviour {

    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private int nButtons;

    private Vector3 startingPosition;
    private Vector3 startDragPos;

    void Awake () {

        for (int i = 0; i < nButtons; i++) {
            GameObject button = Instantiate(buttonPrefab, transform);
            LevelSelectionButton lsb = button.GetComponent<LevelSelectionButton>();
            lsb.Init(i + 1);
        }

        startingPosition = transform.position;
		
	}

    private void Update() {
        if (Input.touchCount > 0) {
            if (Input.GetTouch(0).phase == TouchPhase.Began) {
                startDragPos = Input.GetTouch(0).position;
            }

            if (Input.GetTouch(0).phase == TouchPhase.Moved) {
                Vector3 currentPos;
                currentPos = Input.GetTouch(0).position;
                Vector3 touchDelta = currentPos - startDragPos;
                Debug.Log(touchDelta);
                touchDelta.y = 0;
                touchDelta.z = 0;
                transform.position = startingPosition + touchDelta;
            }            
        }
        else {
            transform.position = Vector3.Lerp(transform.position, startingPosition, 0.15f);
        }
    }
}
