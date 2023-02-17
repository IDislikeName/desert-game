using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleMovement : Movement
{
    public IdleMovement(StateMachine stateMachine) : base(stateMachine)
    {
        // this.stateMachine = stateMachine;
    }
    public override void EnterState()
    {
        Debug.Log("enter idle movement");
        // throw new System.NotImplementedException();
    }
    public override void UpdateLogicState()
    {

        // throw new System.NotImplementedException();
    }

    public override void UpdatePhysicsState()
    {

        // throw new System.NotImplementedException();
    }

    public override void ExitState()
    {
        Debug.Log("enter idle movement");
        // throw new System.NotImplementedException();
    }
}
