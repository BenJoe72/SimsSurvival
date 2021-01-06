using UnityEngine;
using System.Collections;
using static UnityEngine.InputSystem.InputAction;

[CreateAssetMenu(fileName = "InputHandler", menuName = "Configurations/Input/InputHandler")]
public class InputHandler_Scriptable : ScriptableObject
{
    public float HoldTime = .1f;

    protected bool _holding = false;
    protected float _holdTimer = 0f;

    [Header("Events")]
    public ScriptableEvent OnPressed;
    public ScriptableEvent OnHold;
    public ScriptableEvent OnReleased;

    public virtual bool HandleRawInput(CallbackContext context)
    {
        bool result = false;

        if (context.performed)
        {
            // If we are only checking for holding, we don't want to duplicate the first input
            if (HoldTime > 0)
                OnPressed?.Invoke();

            StartHold();
            result = true;
        }
        
        if (context.canceled)
        {
            OnReleased?.Invoke();
            StopHold();
            result = false;
        }

        return result;
    }

    protected virtual void StartHold()
    {
        _holding = true;
        _holdTimer = 0f;
    }

    protected virtual void StopHold()
    {
        _holding = false;
    }

    public virtual void Hold()
    {
        _holdTimer += Time.deltaTime;
        if (_holdTimer >= HoldTime)
        {
            OnHold?.Invoke();
        }
    }
}
