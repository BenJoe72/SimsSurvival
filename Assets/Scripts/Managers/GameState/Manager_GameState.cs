using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class Manager_GameState : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private bool isBuild;

    public void SwitchMode()
    {
        isBuild = !isBuild;
        _animator.SetBool("Build", isBuild);
    }
}
