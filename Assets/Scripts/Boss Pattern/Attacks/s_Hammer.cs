using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_Hammer : s_Attack
{
    public s_Hammer(StateMachine stateMachine, Boss boss) : base(stateMachine, boss)
    {
        damage = 10;
    }

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("enter hammer state");
    }

    public override void ExitState()
    {
        Debug.Log("exit hammer state");
        base.ExitState();
    }

    public override void UpdateLogicState()
    {
        base.UpdateLogicState();
    }

    public override void UpdatePhysicsState()
    {
        base.UpdatePhysicsState();
    }
}
