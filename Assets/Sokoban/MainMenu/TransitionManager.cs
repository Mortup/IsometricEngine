using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TransitionManager : MonoBehaviour {

    [SerializeField] private Image transitionImage;
    [SerializeField] private float transitionTime;

    public void Transition() {
        StartCoroutine(FadeOut());
    }

    public IEnumerator FadeOut() {

        transitionImage.gameObject.SetActive(true);
        float elapsedTime = 0f;

        while (elapsedTime < transitionTime) {
            elapsedTime += Time.deltaTime;

            Color originalColor = transitionImage.color;
            transitionImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.InverseLerp(0f, transitionTime, elapsedTime));

            yield return null;
        }

        transitionImage.color = new Color(transitionImage.color.r, transitionImage.color.g, transitionImage.color.b, 1f);
        yield return null;

    }
	
}
