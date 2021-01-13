using UnityEngine;
using System.Collections;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "MouseInputHandler", menuName = "Configurations/Input/MouseInputHandler")]
public class MouseInputHandler_Scriptable: InputHandler_Scriptable
{
    public MouseButton Button;

    public MouseClickDataEvent OnPressedWithPosition;
    public MouseClickDataEvent OnHoldWithPosition;
    public MouseClickDataEvent OnReleasedWithPosition;

    public override bool HandleRawInput(CallbackContext context)
    {
        if (context.performed)
            OnPressedWithPosition?.Invoke(new MouseClickData(MouseEventType.DOWN, Button, Mouse.current.position.ReadValue()));

        if (context.canceled)
            OnReleasedWithPosition?.Invoke(new MouseClickData(MouseEventType.UP, Button, Mouse.current.position.ReadValue()));

        return base.HandleRawInput(context);
    }

    public override void Hold()
    {
        base.Hold();

        if (_holdTimer >= HoldTime)
            OnHoldWithPosition?.Invoke(new MouseClickData(MouseEventType.HOLD, Button, Mouse.current.position.ReadValue()));
    }
}
