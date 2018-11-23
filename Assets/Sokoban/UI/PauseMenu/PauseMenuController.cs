using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour {

    [SerializeField] private GameObject pauseMenu;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            pauseMenu.SetActive(!pauseMenu.activeSelf);
        }
    }

    public void ResumeGame() {
        pauseMenu.SetActive(false);
    }

    public void GoToMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }
}
