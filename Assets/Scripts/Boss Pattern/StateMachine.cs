using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    // [HideInInspector]
    public State currentState;
    [HideInInspector]
    public LeapAttack leapAttackState;
    [HideInInspector]
    public IdleMovement idleMovementState;

    private void Start()
    {
        Debug.Log("start");
        leapAttackState = new LeapAttack(this);
        idleMovementState = new IdleMovement(this);
        Initialize(leapAttackState);
    }

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
