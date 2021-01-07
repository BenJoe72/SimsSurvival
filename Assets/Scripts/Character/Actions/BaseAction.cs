using UnityEngine;
using System.Collections;
using System;

public abstract class BaseAction : ScriptableObject
{
    public string Name;

    public ActionCondition Condition;

    public InteractionEvent BeginInteractionEvent;
    public InteractionEvent InteractionEvent;

    public virtual bool CanPerform(Interaction interaction) => Condition?.Evaluate(interaction.usedResource) ?? true;

    public virtual void Activate(Interaction interaction)
    {
        interaction.interactionMethod += (c, v) => { InteractionEvent?.Invoke(interaction); }; // TODO: refactor to avoid lambda expressions
        BeginInteractionEvent?.Invoke(interaction);
    }
}
