using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class Manager_GameState : MonoBehaviour
{
    public GameState[] GameStates;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void SwitchState(GameState newState)
    {
        foreach (var state in GameStates)
        {
            _animator.SetBool(state.Name, state == newState);
        }
    }
}
