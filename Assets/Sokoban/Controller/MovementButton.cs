using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MovementButton : MonoBehaviour, IPointerDownHandler {

    [SerializeField] private UnityEvent onPressedDown;

    public void OnPointerDown(PointerEventData eventData) {
        onPressedDown.Invoke();
    }
}
