using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameStateEventListener : TypedScriptableEventListener<GameState>
{
    public GameState CheckState;

    public override void Invoke(GameState value)
    {
        if (CheckState == null || value == CheckState)
            base.Invoke(value);
    }
}
