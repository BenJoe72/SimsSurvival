using UnityEngine;
using System.Collections;
using static UnityEngine.InputSystem.InputAction;

[CreateAssetMenu(fileName = "Vector2InputHandler", menuName = "Configurations/Input/Vector2InputHandler")]
public class Vector2InputHandler_Scriptable : InputHandler_Scriptable
{
    public Vector2Event OnPressedWithValue;
    public Vector2Event OnHoldWithValue;
    public Vector2Event OnReleasedWithValue;

    private Vector2 _cachedValue;

    public override bool HandleRawInput(CallbackContext context)
    {
        if (context.performed)
        {
            _cachedValue = context.ReadValue<Vector2>();
            OnPressedWithValue?.Invoke(_cachedValue);
        }
        
        if (context.canceled)
            OnReleasedWithValue?.Invoke(_cachedValue);

        return base.HandleRawInput(context);
    }

    public override void Hold()
    {
        base.Hold();

        if (_holdTimer >= HoldTime)
            OnHoldWithValue?.Invoke(_cachedValue);
    }
}
