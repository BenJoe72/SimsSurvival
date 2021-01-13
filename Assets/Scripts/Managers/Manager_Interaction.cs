using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;
using System;
using System.Linq;

[RequireComponent(typeof(CurrentCharacter_Data))]
public class Manager_Interaction : MonoBehaviour
{
    public Manager_Movement _MovementManager;

    private CurrentCharacter_Data _currentCharacter;

    private Dictionary<Interaction, float> _repeatActions;

    private void Start()
    {
        _currentCharacter = GetComponent<CurrentCharacter_Data>();
        _repeatActions = new Dictionary<Interaction, float>();

        if (_MovementManager == null)
            Debug.LogError("Movement Manager has not been set for the Interaction Manager. This connection is required.");
    }

    private void Update()
    {
        var runInteractions = _repeatActions.Where(x => x.Value <= Time.time).ToDictionary(x => x.Key, y => y.Value);
        foreach (var rInt in runInteractions.Keys)
        {
            if (rInt.interactable != null)
            {
                rInt.interactionMethod?.Invoke(rInt.character, rInt.interactable);
                _repeatActions[rInt] = Time.time + rInt.action.RepeatDelay;
            }

            // The interactable can be destoryed during interaction that's why we check again
            if (rInt.interactable == null || (rInt.action.FinishCondition != null && rInt.action.FinishCondition.Evaluate(rInt.character.Resources)))
                GoToIdle(rInt.character);
        }
    }

    public void InteractWith(Interaction interaction)
    {
        GoToIdle();

        _MovementManager.SetDestination(interaction.position, () =>
            {
                if (interaction.action.IsRepeating)
                    _repeatActions.Add(interaction, Time.time);
                else
                    interaction.interactionMethod?.Invoke(interaction.character, interaction.interactable);
            });
    }

    private void GoToIdle()
    {
        GoToIdle(_currentCharacter.character);
    }

    private void GoToIdle(CharacterScript character)
    {
        var characterInteractions = _repeatActions.Keys.Where(x => x.character == character).ToList();
        foreach (var cInt in characterInteractions)
            _repeatActions.Remove(cInt);

        character.mover.EnableNavmesh();
        character.animator.SetToIdle();
    }
}
