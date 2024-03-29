﻿using UnityEngine;
using System.Collections;
using System;

public class Interaction
{
    public CharacterScript character;
    public Interactable interactable;
    public Manager_Resource usedResource;
    public BaseAction action;
    public Vector3 position;
    public Action<CharacterScript, Interactable> interactionMethod;

    public Interaction(CharacterScript character, Interactable interactable, Vector3 position, Manager_Resource usedResource = null)
    {
        this.character = character;
        this.interactable = interactable;
        this.position = position;
        this.usedResource = usedResource;
    }

    public Interaction(CharacterScript character, Interactable interactable, BaseAction action, Vector3 position, Manager_Resource usedResource = null)
    {
        this.character = character;
        this.interactable = interactable;
        this.action = action;
        this.position = position;
        this.usedResource = usedResource;
    }

    public void Activate()
    {
        action.Activate(this);
    }
}
