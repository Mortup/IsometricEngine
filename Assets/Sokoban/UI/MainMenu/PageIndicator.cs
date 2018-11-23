using UnityEngine;
using UnityEngine.UI;

public class PageIndicator : MonoBehaviour {

    [SerializeField] GameObject dotPrefab;
    [SerializeField] Color selectedDotColor;

    GameObject[] dots;

    public void Init(int nPages) {
        dots = new GameObject[nPages];

        for (int i = 0; i < nPages; i++) {
            dots[i] = Instantiate(dotPrefab, transform);
        }
    }

    public void SetActiveDot(int n) {
        for (int i = 0; i < dots.Length; i++) {
            Image img = dots[i].GetComponent<Image>();
            if (i == n) {
                img.color = selectedDotColor;
            }
            else {
                img.color = Color.white;
            }
        }
    }

	
}
