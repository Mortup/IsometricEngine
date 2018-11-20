using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using com.gStudios.sokoban.controller;

public class LevelSelectionButton : MonoBehaviour {

    [SerializeField] private Text text;

    private int index;
    private Button btn;

    public void Init(int index) {
        this.index = index;

        text.text = index.ToString();
        btn = GetComponent<Button>();
        btn.onClick.AddListener(OnButtonPressed);
    }

    public void OnButtonPressed() {
        if (Input.touchCount > 0 && Mathf.Abs(Input.GetTouch(0).deltaPosition.x) < 3f) {
            TransitionManager tm = FindObjectOfType<TransitionManager>();
            SokobanController.currentLevelIndex = index - 1;
            tm.Transition(LoadLevel);            
        }
    }

    public void LoadLevel() {
        SceneManager.LoadScene("GameScene");
    }
    
}
