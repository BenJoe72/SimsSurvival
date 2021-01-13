using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CurrentCharacter_Data))]
public class Manager_Click : MonoBehaviour
{
    public Camera _MainCamera;
    public LayerMask _ClickLayer;
    public LayerMask _GroundLayer;
    public LayerMask _CharacterLayer;

    [Header("Events")]
    public Vector2Event _ClickScreenPositionEvent;
    public Vector3Event _HoverPositionEvent;
    public InteractionEvent _ClickInteractionEvent;
    public InteractableEvent _ClickInteractableEvent;
    public CharacterEvent _ClickCharacterEvent;
    public CharacterEvent _RightClickCharacterEvent;


    public Vector3Event _House_ClickDownEvent;
    public Vector3Event _House_ClickHoldEvent;
    public Vector3Event _House_ClickUpEvent;

    private CurrentCharacter_Data _currentCharacter;

    // TODO: do something with this later to not be hard-coded 2 states
    private bool _isEditing;
    public void EnableEdit()
    {
        _isEditing = true;
    }
    public void DisableEdit()
    {
        _isEditing = false;
    }


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

        if (!EventSystem.current.IsPointerOverGameObject(fingerID) && Physics.Raycast(ray, out hit, float.MaxValue, _isEditing ? _GroundLayer : _ClickLayer))
        {
            _HoverPositionEvent?.Invoke(hit.point);
            _hoverRequested = false;
        }
    }

    public void RightClicked(Vector2 screenPos)
    {
        Ray ray = _MainCamera.ScreenPointToRay(screenPos);
        RaycastHit hit;

        if (!EventSystem.current.IsPointerOverGameObject(fingerID))
        {
            if (Physics.Raycast(ray, out hit, float.MaxValue, _CharacterLayer))
            {
                CharacterScript character = hit.transform.GetComponent<CharacterScript>();

                if (character != null)
                {
                    _RightClickCharacterEvent?.Invoke(character);
                    return;
                }
            }
        }
    }

    public void Clicked(Vector2 screenPos)
    {
        Ray ray = _MainCamera.ScreenPointToRay(screenPos);
        RaycastHit hit;

        if (!EventSystem.current.IsPointerOverGameObject(fingerID))
        {
            if (Physics.Raycast(ray, out hit, float.MaxValue, _CharacterLayer))
            {
                CharacterScript character = hit.transform.GetComponent<CharacterScript>();

                if (character != null)
                {
                    _ClickCharacterEvent?.Invoke(character);
                    return;
                }
            }

            if (Physics.Raycast(ray, out hit, float.MaxValue, _ClickLayer + _GroundLayer))
            {
                Interactable interactable = hit.transform.GetComponent<Interactable>();

                if (interactable != null)
                {
                    _ClickScreenPositionEvent?.Invoke(screenPos);

                    Vector3 position = hit.point;
                    if (interactable.InteractionPoint != null)
                        position = interactable.InteractionPoint.position;

                    if (!_isEditing)
                    {
                        _ClickInteractionEvent?.Invoke(new Interaction(_currentCharacter.character, interactable, position, _currentCharacter.character.Reesources));
                    }
                    else
                    {
                        _ClickInteractableEvent?.Invoke(interactable);
                    }
                }
            }
        }
    }

    // TODO: refactor, very much, please, for the love of ...

    public void ButtonDown(Vector2 screenPos)
    {
        Ray ray = _MainCamera.ScreenPointToRay(screenPos);
        RaycastHit hit;

        if (!EventSystem.current.IsPointerOverGameObject(fingerID))
        {
            if (Physics.Raycast(ray, out hit, float.MaxValue, _GroundLayer))
            {
                Interactable interactable = hit.transform.GetComponent<Interactable>();

                if (interactable != null)
                {
                    _House_ClickDownEvent?.Invoke(hit.point);
                }
            }
        }
    }

    public void ButtonUp(Vector2 screenPos)
    {
        Ray ray = _MainCamera.ScreenPointToRay(screenPos);
        RaycastHit hit;

        if (!EventSystem.current.IsPointerOverGameObject(fingerID))
        {
            if (Physics.Raycast(ray, out hit, float.MaxValue, _GroundLayer))
            {
                Interactable interactable = hit.transform.GetComponent<Interactable>();

                if (interactable != null)
                {
                    _House_ClickUpEvent?.Invoke(hit.point);
                }
            }
        }
    }

    public void ButtonHold(Vector2 screenPos)
    {
        Ray ray = _MainCamera.ScreenPointToRay(screenPos);
        RaycastHit hit;

        if (!EventSystem.current.IsPointerOverGameObject(fingerID))
        {
            if (Physics.Raycast(ray, out hit, float.MaxValue, _GroundLayer))
            {
                Interactable interactable = hit.transform.GetComponent<Interactable>();

                if (interactable != null)
                {
                    _House_ClickHoldEvent?.Invoke(hit.point);
                }
            }
        }
    }
}
