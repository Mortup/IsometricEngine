using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScripts : MonoBehaviour {

	public void OnPressPlay() {
        SceneManager.LoadScene("GameScene");
    }

}
