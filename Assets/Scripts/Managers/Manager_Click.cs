using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CurrentCharacter_Data))]
public class Manager_Click : MonoBehaviour
{
    public Camera _MainCamera;
    public LayerMask _ClickLayer;

    [Header("Events")]
    public Vector2Event _ClickScreenPositionEvent;
    public InteractionEvent _ClickInteractionEvent;
    
    private CurrentCharacter_Data _currentCharacter;

    private void Start()
    {
        _currentCharacter = GetComponent<CurrentCharacter_Data>();
    }

    public void Clicked(Vector2 screenPos)
    {
        Ray ray = _MainCamera.ScreenPointToRay(screenPos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, float.MaxValue, _ClickLayer))
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
