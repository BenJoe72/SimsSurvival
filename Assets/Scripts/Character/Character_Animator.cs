using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Animator))]
public class Character_Animator : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void StartWalk()
    {
        _animator?.SetBool("Walking", true);
    }

    public void EndWalk()
    {
        _animator?.SetBool("Walking", false);
    }

    public void Interact()
    {
        _animator?.SetBool("Interacting", true);
        _animator?.SetTrigger("Interact");
    }

    private void StopInteract()
    {
        _animator?.SetBool("Interacting", false);
    }

    public void SitDown()
    {
        _animator?.SetBool("Sitting", true);
    }

    public void StandUp()
    {
        _animator?.SetBool("Sitting", false);
    }

    public void LayDown()
    {
        _animator?.SetBool("Sleeping", true);
    }

    public void GetUp()
    {
        _animator?.SetBool("Sleeping", false);
    }

    internal void SetToIdle()
    {
        GetUp();
        EndWalk();
        StandUp();
        StopInteract();
    }
}
