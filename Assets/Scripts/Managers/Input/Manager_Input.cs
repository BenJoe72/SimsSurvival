using UnityEngine;
using System.Collections;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class Manager_Input : MonoBehaviour
{
    private List<InputHandler_Scriptable> _heldButtons;

    private CallbackContext _contextCache;

    private void Start()
    {
        _heldButtons = new List<InputHandler_Scriptable>();
    }

    private void Update()
    {
        foreach (var button in _heldButtons)
        {
            button.Hold();
        }
    }

    // This is a bad idea, if it goes wrong you only have yourself to blame
    public void SetContext(CallbackContext context)
    {
        _contextCache = context;
    }

    public void HandleInput(InputHandler_Scriptable handler)
    {
        // Error handling in case the context setting did not occur or the context input has already been handled
        if (_contextCache.Equals(default))
            return;

        if (handler.HandleRawInput(_contextCache))
        {
            if (!_heldButtons.Contains(handler))
                _heldButtons.Add(handler);
        }
        else
        {
            if (_heldButtons.Contains(handler))
                _heldButtons.Remove(handler);
        }
    }
}
