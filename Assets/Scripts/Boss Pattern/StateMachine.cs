using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public State currentState;

    public void Initialize(State state)
    {
        currentState = state;
        currentState.EnterState();
    }

    public void ChangeState(State state)
    {
        currentState.ExitState();
        currentState = state;
        currentState.EnterState();
    }
}
