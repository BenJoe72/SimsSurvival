using UnityEngine;
using System.Collections;
using Cinemachine;
using UnityEngine.Events;

public class CharacterScript : MonoBehaviour
{
    public Character_Mover mover;
    public Character_Interacter interacter;
    public Character_Animator animator;

    public Manager_Resource Reesources;

    public UnityEvent OnSelect;
    public UnityEvent OnDeselect;

    public void Select()
    {
        OnSelect?.Invoke();
    }

    public void Deselect()
    {
        OnDeselect?.Invoke();
    }
}
