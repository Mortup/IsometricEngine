using UnityEngine;
using UnityEngine.UI;

using com.gStudios.sokoban.model.saving;

public class ButtonsContainer : MonoBehaviour {

    [SerializeField] private GameObject containerPrefab;
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private int buttonsPerScreen;
    [SerializeField] private PageIndicator pageIndicator;
    [SerializeField] private Color completedColor;
    [SerializeField] private int levelUnlockMargin;

    private float minSlide = 5;

    private Vector3 startingPosition;
    private Vector3 containerStartDragPos;
    private Vector3 startDragPos;

    private GameObject[] containers;
    private float containerWidth;
    private int currentAnchorIndex = 0;

    private int nButtons;

    void Awake () {
        nButtons = SokobanLevelSerializer.LevelsCount();

        int nContainers = 1 + (nButtons / buttonsPerScreen);
        containers = new GameObject[nContainers];

        for (int i = 0; i < nContainers; i++) {
            containers[i] = Instantiate(containerPrefab, transform);
            
            if (i != 0) {
                RectTransform prevContainerTrans = containers[i - 1].GetComponent<RectTransform>();
                containers[i].transform.position = prevContainerTrans.position + new Vector3(containerWidth, 0f, 0f);
            }
            else {
                RectTransform rt = containers[i].GetComponent<RectTransform>();
                containerWidth = rt.rect.width * rt.lossyScale.x;
            }
        }

        for (int i = 0; i < nButtons; i++) {
            int containerIndex = i / buttonsPerScreen;

            GameObject button = Instantiate(buttonPrefab, containers[containerIndex].transform);
            LevelSelectionButton lsb = button.GetComponent<LevelSelectionButton>();
            lsb.Init(i + 1);
            
            if (i > SokoPlayerPrefs.MaxCompletedLevel() + levelUnlockMargin) {
                Button btnComponent = button.GetComponent<Button>();
                btnComponent.interactable = false;
            }
            if (SokoPlayerPrefs.IsCompleted(i)) {
                Image img = button.GetComponent<Image>();
                img.color = completedColor;
            }
        }

        pageIndicator.Init(nContainers);
        pageIndicator.SetActiveDot(currentAnchorIndex);

        startingPosition = transform.position;
        containerStartDragPos = startingPosition;        
    }

    private void Update() {
        if (Input.touchCount > 0) {
            if (Input.GetTouch(0).phase == TouchPhase.Began) {
                startDragPos = Input.GetTouch(0).position;
                containerStartDragPos = transform.position;
            }

            if (Input.GetTouch(0).phase == TouchPhase.Moved) {
                Vector3 currentPos;
                currentPos = Input.GetTouch(0).position;
                Vector3 touchDelta = currentPos - startDragPos;
                touchDelta.y = 0;
                touchDelta.z = 0;
                transform.position = containerStartDragPos + touchDelta;
            }           
            
            if (Input.GetTouch(0).phase == TouchPhase.Ended) {
                Vector2 lastMovement = Input.GetTouch(0).deltaPosition;

                if (lastMovement.x > minSlide) {
                    currentAnchorIndex--;
                }
                else if (lastMovement.x < -minSlide) {
                    currentAnchorIndex++;
                }
                currentAnchorIndex = Mathf.Clamp(currentAnchorIndex, 0, containers.Length - 1);
            }
        }
        else {
            float xAnchor = startingPosition.x - currentAnchorIndex * containerWidth;
            pageIndicator.SetActiveDot(currentAnchorIndex);

            Vector3 posAnchor = new Vector3(xAnchor, startingPosition.y, startingPosition.z);
            transform.position = Vector3.Lerp(transform.position, posAnchor, 0.15f);
        }
    }
}
