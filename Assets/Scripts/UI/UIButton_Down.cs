using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIButton_Down : MonoBehaviour, IPointerDownHandler
{
    public UnityEvent OnClick;

    public void OnPointerDown(PointerEventData eventData)
    {
        OnClick?.Invoke();
    }
}
