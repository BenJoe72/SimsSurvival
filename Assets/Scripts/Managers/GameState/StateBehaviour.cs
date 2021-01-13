using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBehaviour : StateMachineBehaviour
{
    public GameState State;
    public GameStateEvent OnEnterState;
    public GameStateEvent OnExitState;
    public GameStateEvent OnUpdateState;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        OnEnterState?.Invoke(State);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        OnExitState?.Invoke(State);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        OnUpdateState?.Invoke(State);
    }
}
