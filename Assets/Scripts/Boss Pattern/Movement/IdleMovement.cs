using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleMovement : Movement
{
    public IdleMovement(StateMachine stateMachine, Boss boss) : base(stateMachine, boss) { }
    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("enter idle movement");
    }
    public override void ExitState()
    {
        Debug.Log("exit idle movement"); //doesn't get called because there's no logic for it
        base.ExitState();
    }
}
