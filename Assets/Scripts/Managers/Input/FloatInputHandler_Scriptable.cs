using UnityEngine;
using System.Collections;
using static UnityEngine.InputSystem.InputAction;

[CreateAssetMenu(fileName = "FloatInputHandler", menuName = "Configurations/Input/FloatInputHandler")]
public class FloatInputHandler_Scriptable : InputHandler_Scriptable
{
    public FloatEvent OnPressedWithValue;
    public FloatEvent OnHoldWithValue;
    public FloatEvent OnReleasedWithValue;

    private float _cachedValue;

    public override bool HandleRawInput(CallbackContext context)
    {
        if (context.performed)
        {
            _cachedValue = context.ReadValue<float>();
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
