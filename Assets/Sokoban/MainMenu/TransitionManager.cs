using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionManager : MonoBehaviour {

    [SerializeField] private Image transitionImage;
    [SerializeField] private float fadeOutTime;
    [SerializeField] private float fadeInTime;
    [SerializeField] private GameObject startingScreen;

    private GameObject currentScreen;
    private Stack<GameObject> previousScreens;

    public void Awake() {
        currentScreen = startingScreen;
        previousScreens = new Stack<GameObject>();
    }

    public void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (previousScreens.Count == 0) {
                Debug.Log("Quiting");
                Application.Quit();
            }

            UndoTransition();
        }
    }

    public void Transition(GameObject newScreen) {
        StartCoroutine(FadeOut(newScreen, true));
    }

    public void UndoTransition() {
        StartCoroutine(FadeOut(previousScreens.Pop(), false));
    }

    public IEnumerator FadeOut(GameObject newScreen, bool addToTransitionStack) {

        transitionImage.gameObject.SetActive(true);
        float elapsedTime = 0f;

        while (elapsedTime < fadeOutTime) {
            elapsedTime += Time.deltaTime;

            Color originalColor = transitionImage.color;
            transitionImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.InverseLerp(0f, fadeOutTime, elapsedTime));

            yield return null;
        }

        transitionImage.color = new Color(transitionImage.color.r, transitionImage.color.g, transitionImage.color.b, 1f);

        currentScreen.SetActive(false);
        newScreen.SetActive(true);

        if (addToTransitionStack)
            previousScreens.Push(currentScreen);

        currentScreen = newScreen;

        StartCoroutine(FadeIn());
        yield return null;
    }

    public IEnumerator FadeIn() {

        float elapsedTime = 0f;

        while (elapsedTime < fadeInTime) {
            elapsedTime += Time.deltaTime;

            Color originalColor = transitionImage.color;
            transitionImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.InverseLerp(fadeInTime, 0f, elapsedTime));

            yield return null;
        }

        transitionImage.color = new Color(transitionImage.color.r, transitionImage.color.g, transitionImage.color.b, 0f);

        transitionImage.gameObject.SetActive(false);
        yield return null;
    }
	
}
