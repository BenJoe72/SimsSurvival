using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CurrentCharacter_Data))]
public class Manager_Click : MonoBehaviour
{
    public Camera _MainCamera;
    public LayerMask _ClickLayer;

    [Header("Events")]
    public Vector2Event _ClickScreenPositionEvent;
    public Vector3Event _HoverPositionEvent;
    public InteractionEvent _ClickInteractionEvent;
    
    private CurrentCharacter_Data _currentCharacter;

    private int fingerID = -1;
    private void Awake()
    {
#if !UNITY_EDITOR
        fingerID = 0; 
#endif
    }

    private void Start()
    {
        _currentCharacter = GetComponent<CurrentCharacter_Data>();
    }

    private bool _hoverRequested = false;

    public void RequestHover()
    {
        _hoverRequested = true;
    }

    public void Hover(Vector2 screenPos)
    {
        if (!_hoverRequested)
            return;

        Ray ray = _MainCamera.ScreenPointToRay(screenPos);
        RaycastHit hit;

        if (!EventSystem.current.IsPointerOverGameObject(fingerID) && Physics.Raycast(ray, out hit, float.MaxValue, _ClickLayer))
        {
            _HoverPositionEvent?.Invoke(hit.point);
            _hoverRequested = false;
        }
    }

    public void Clicked(Vector2 screenPos)
    {
        Ray ray = _MainCamera.ScreenPointToRay(screenPos);
        RaycastHit hit;

        if (!EventSystem.current.IsPointerOverGameObject(fingerID) && Physics.Raycast(ray, out hit, float.MaxValue, _ClickLayer))
        {
            Interactable interactable = hit.transform.GetComponent<Interactable>();

            if (interactable != null)
            {
                _ClickScreenPositionEvent?.Invoke(screenPos);

                Vector3 position = hit.point;
                if (interactable._InteractionPoint != null)
                    position = interactable._InteractionPoint.position;

                _ClickInteractionEvent?.Invoke(new Interaction(_currentCharacter.character, interactable, position));
            }
        }
    }
}
