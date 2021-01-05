using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBehaviour<T> : StateMachineBehaviour where T : StateBrain
{
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GetBrain(animator)?.EnterState();
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GetBrain(animator)?.ExitState();
    }

    protected T _brainCache;

    protected T GetBrain(Animator animator)
    {
        if (_brainCache == null)
            _brainCache = animator.GetComponent<T>();

        return _brainCache;
    }
}
