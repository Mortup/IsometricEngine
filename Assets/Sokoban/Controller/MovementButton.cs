using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MovementButton : Button {

    [SerializeField] private UnityEvent onPressed;

    public override void OnPointerDown(PointerEventData eventData) {
        base.OnPointerDown(eventData);

        onPressed.Invoke();
    }


}
