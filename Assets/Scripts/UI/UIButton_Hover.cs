using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIButton_Hover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public UnityEvent OnHover;
    public UnityEvent OnHoverOff;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        OnHover?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnHoverOff?.Invoke();
    }
}
