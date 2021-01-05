using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Manager_Character : MonoBehaviour
{
    public CharacterScript _currentCharacter; // TODO: hide later when selection is ready

    [Header("Events")]
    public CharacterEvent _CharacterChanged;

    private void Start()
    {
        ChangeCharacter(_currentCharacter);
    }

    public void ChangeCharacter(CharacterScript newchar)
    {
        _currentCharacter.Deselect();
        _currentCharacter = newchar;
        _currentCharacter.Select();

        _CharacterChanged?.Invoke(newchar);
    }
}
