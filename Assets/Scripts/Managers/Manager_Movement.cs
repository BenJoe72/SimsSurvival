using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System;

[RequireComponent(typeof(CurrentCharacter_Data))]
public class Manager_Movement : MonoBehaviour
{
    private CurrentCharacter_Data _currentCharacter;

    private void Start()
    {
        _currentCharacter = GetComponent<CurrentCharacter_Data>();
    }

    public void SetDestination(Vector3 destination, Action callback)
    {
        _currentCharacter.character.mover.RegisterArriveCallback(callback);
        _currentCharacter.character.mover.SetDestination(destination);
    }
}
