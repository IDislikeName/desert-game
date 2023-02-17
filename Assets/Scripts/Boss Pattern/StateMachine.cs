using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public State currentState;

    public LeapAttack leapAttackState;
    public IdleMovement idleMovementState;

    private void Start()
    {
        leapAttackState = new LeapAttack(this);
        idleMovementState = new IdleMovement(this);
    }

    public void Initialize(State state)
    {
        currentState = state;
    }

    public void ChangeState(State state)
    {
        currentState.ExitState();
        currentState = state;
        currentState.EnterState();
    }
}
