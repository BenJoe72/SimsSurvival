using UnityEngine;
using System.Collections;

public class CurrentCharacter_Data : CharacterEventListener
{
   [HideInInspector] public CharacterScript character;

    public override void Invoke(CharacterScript value)
    {
        base.Invoke(value);
        character = value;
    }
}
