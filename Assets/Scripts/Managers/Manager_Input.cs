using UnityEngine;
using System.Collections;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Manager_Input : MonoBehaviour
{
    [Header("Events")]
    public Vector2Event _LeftClickDownWithPositon;
    public Vector2Event _RightClickDownWithPositon;

    public void MouseLeftClick(CallbackContext context)
    {
        if (context.performed)
        {
            _LeftClickDownWithPositon?.Invoke(Mouse.current.position.ReadValue());
        }
    }

    public void MouseRightClick(CallbackContext context)
    {
        if (context.performed)
        {
            _RightClickDownWithPositon?.Invoke(Mouse.current.position.ReadValue());
        }
    }
}
