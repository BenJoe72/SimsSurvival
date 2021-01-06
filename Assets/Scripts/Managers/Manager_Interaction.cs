using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;

[RequireComponent(typeof(CurrentCharacter_Data))]
public class Manager_Interaction : MonoBehaviour
{
    public Manager_Movement _MovementManager;

    private CurrentCharacter_Data _currentCharacter;

    private void Start()
    {
        _currentCharacter = GetComponent<CurrentCharacter_Data>();

        if (_MovementManager == null)
            Debug.LogError("Movement Manager has not been set for the Interaction Manager. This connection is required.");
    }

    public void InteractWith(Interaction interaction)
    {
        GoToIdle();

        _MovementManager.SetDestination(interaction.position, () =>
            {
                interaction.interactionMethod?.Invoke(interaction.character, interaction.interactable);
            });
    }

    private void GoToIdle()
    {
        _currentCharacter.character.mover.EnableNavmesh();
        _currentCharacter.character.animator.SetToIdle();
    }
}
