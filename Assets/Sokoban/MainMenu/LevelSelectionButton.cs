using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class LevelSelectionButton : MonoBehaviour {

    [SerializeField] private Text text;

    private int index;
    private Button btn;

    private Vector3 startDragPos;
    private float currentXDelta;

    public void Init(int index) {
        this.index = index;

        text.text = index.ToString();
        btn = GetComponent<Button>();
        btn.onClick.AddListener(OnButtonPressed);
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
                currentXDelta = touchDelta.x;
            }
        }
    }

    public void OnButtonPressed() {
        Debug.Log("Apretaito");
        if (Mathf.Abs(currentXDelta) < 10f) {
            TransitionManager tm = FindObjectOfType<TransitionManager>();
            Debug.Log(tm);
            tm.Transition(LoadLevel);            
        }
    }

    public void LoadLevel() {
        SceneManager.LoadScene("GameScene");
    }
    
}
