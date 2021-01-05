using UnityEngine;
using System.Collections;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "MouseInputHandler", menuName = "Configurations/Input/MouseInputHandler")]
public class MouseInputHandler_Scriptable: InputHandler_Scriptable
{
    public Vector2Event OnPressedWithPosition;
    public Vector2Event OnHoldWithPosition;
    public Vector2Event OnReleasedWithPosition;

    public override bool HandleRawInput(CallbackContext context)
    {
        if (context.performed)
            OnPressedWithPosition?.Invoke(Mouse.current.position.ReadValue());
        
        if (context.canceled)
            OnReleasedWithPosition?.Invoke(Mouse.current.position.ReadValue());

        return base.HandleRawInput(context);
    }

    public override void Hold()
    {
        base.Hold();

        if (_holdTimer >= HoldTime)
            OnHoldWithPosition?.Invoke(Mouse.current.position.ReadValue());
    }
}
