using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(CurrentCharacter_Data))]
public class Manager_Click : MonoBehaviour
{
    [Header("Old stuff")]
    public Camera MainCamera;

    [Header("Events")]
    public ClickConfiguration[] ClickConfigurations;

    private CurrentCharacter_Data _currentCharacter;
    private ClickConfiguration _currentConfiguration;

#region UI click setup
    private int fingerID = -1;
    private void Awake()
    {
#if !UNITY_EDITOR
        fingerID = 0; 
#endif
    }
#endregion UI click setup

    private void Start()
    {
        _currentCharacter = GetComponent<CurrentCharacter_Data>();
    }

    private bool _hoverRequested = false;

    public void RequestHover()
    {
        _hoverRequested = true;
    }

    public void SetState(GameState newState)
    {
        ClickConfiguration newConfig = ClickConfigurations.FirstOrDefault(x => x.State == newState);
        if (newConfig != null)
            _currentConfiguration = newConfig;
    }

    public void Hover(Vector2 screenPos)
    {
        if (!_hoverRequested)
            return;

        if (ValidWorldClick())
        {
            _currentConfiguration.HandleHover(GetRay(screenPos));
            _hoverRequested = false;
        }
    }

    public void HandleMouseClick(MouseClickData data)
    {
        if (ValidWorldClick())
            _currentConfiguration.HandleClick(GetRay(data.ScreenPosition), _currentCharacter.character, data);
    }

    private Ray GetRay(Vector2 screenPos)
    {
        return MainCamera.ScreenPointToRay(screenPos);
    }

    private bool ValidWorldClick()
    {
        return _currentConfiguration != null && !EventSystem.current.IsPointerOverGameObject(fingerID);
    }

    [System.Serializable]
    public class ClickConfiguration
    {
        public string Name;
        public GameState State;

        [Header("Events")]
        public Vector3Event HoverEvent;

        public ClickSet<Vector2> ScreenPositionClick;
        public ClickSet<Vector3> PositionClick;
        public ClickSet<Interaction> InteractionClick;
        public ClickSet<Interactable> InteractableClick;
        public ClickSet<CharacterScript> CharacterClick;

        public void HandleHover(Ray ray)
        {
            // Hover
            RaycastHit position;
            if (PositionClick.RayCast(ray, out position))
                HoverEvent?.Invoke(position.point);
        }

        public void HandleClick(Ray ray, CharacterScript currentCharacter, MouseClickData data)
        {
            // Screen position
            ScreenPositionClick.HandleClicks(data.ScreenPosition, data);

            // Position
            RaycastHit position;
            if (PositionClick.RayCast(ray, out position))
                PositionClick.HandleClicks(position.point, data);

            // Character
            RaycastHit characterHit;
            if (CharacterClick.RayCast(ray, out characterHit))
            {
                CharacterScript character = characterHit.collider.GetComponent<CharacterScript>();
                if (character != null)
                    CharacterClick.HandleClicks(character, data);
            }
            else
            {
                // Interactable
                RaycastHit interactableHit;
                if (InteractableClick.RayCast(ray, out interactableHit))
                {
                    Interactable interactable = interactableHit.collider.GetComponent<Interactable>();
                    if (interactable != null)
                        InteractableClick.HandleClicks(interactable, data);
                }

                // Interaction
                RaycastHit interactionHit;
                if (InteractionClick.RayCast(ray, out interactionHit))
                {
                    Interactable interactable = interactionHit.collider.GetComponent<Interactable>();
                    if (interactable != null)
                        InteractionClick.HandleClicks(new Interaction(currentCharacter, interactable, interactionHit.point, currentCharacter.Resources), data);
                }
            }
        }

        [System.Serializable]
        public class ClickSet<T>
        {
            public LayerMask ClickLayer;
            public ClickEvents LeftClick;
            public ClickEvents RightClick;

            public void HandleClicks(T target, MouseClickData data)
            {
                switch (data.Button)
                {
                    default:
                    case MouseButton.LEFT:
                        LeftClick.HandleClicks(target, data.EventType);
                        break;
                    case MouseButton.RIGHT:
                        RightClick.HandleClicks(target, data.EventType);
                        break;
                }
            }

            public bool RayCast(Ray ray, out RaycastHit hit)
            {
                return Physics.Raycast(ray, out hit, float.MaxValue, ClickLayer);
            }

            [System.Serializable]
            public class ClickEvents
            {
                public TypedScriptableEvent<T> ClickDownEvent;
                public TypedScriptableEvent<T> ClickHoldEvent;
                public TypedScriptableEvent<T> ClickUpEvent;

                public void HandleClicks(T target, MouseEventType type)
                {
                    switch (type)
                    {
                        default:
                        case MouseEventType.DOWN:
                            ClickDownEvent?.Invoke(target);
                            break;
                        case MouseEventType.UP:
                            ClickUpEvent?.Invoke(target);
                            break;
                        case MouseEventType.HOLD:
                            ClickHoldEvent?.Invoke(target);
                            break;
                    }
                }
            }
        }
    }
}
