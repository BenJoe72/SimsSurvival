using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent))]
public class Character_Mover : MonoBehaviour
{
    [Header("Events")]
    public UnityEvent _StartMovement;
    public UnityEvent _StopMovement;

    private NavMeshAgent _nmAgent;

    private bool _isMoving = false;
    private Action _arrivedCallback;

    private void Start()
    {
        _nmAgent = GetComponent<NavMeshAgent>();
    }

    public void RegisterArriveCallback(Action callback)
    {
        _arrivedCallback += callback;
    }

    private void Update()
    {
        if (!_isMoving)
            return;

        float dist = _nmAgent.remainingDistance;
        if (!_nmAgent.pathPending && dist != Mathf.Infinity && _nmAgent.pathStatus == NavMeshPathStatus.PathComplete && _nmAgent.remainingDistance < _nmAgent.stoppingDistance)
        {
            _isMoving = false;
            _StopMovement?.Invoke();
            _arrivedCallback?.Invoke();
            _arrivedCallback = null;
        }
    }

    public void SetDestination(Vector3 destionation)
    {
        if (_nmAgent != null)
        {
            _isMoving = true;
            _nmAgent.SetDestination(destionation);
            _StartMovement?.Invoke();
        }
    }
}
