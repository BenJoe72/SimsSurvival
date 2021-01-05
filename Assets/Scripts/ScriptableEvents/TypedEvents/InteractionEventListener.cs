using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InteractionEventListener : TypedScriptableEventListener<Interaction>
{
    public CharacterScript Character;
    public Interactable Interactable;

    // TODO: refactor the hell out of this
    public override void Invoke(Interaction value)
    {
        if ((Character == null && Interactable == null) || (Character != null && value.character == Character) || (Interactable != null && value.interactable == Interactable))
            base.Invoke(value);
    }
}
