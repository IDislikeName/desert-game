using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_Chase : s_Movement
{
    public s_Chase(StateMachine stateMachine, Boss boss) : base(stateMachine, boss) { }

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("enter chase movement");
    }

    public override void UpdateLogicState()
    {
        base.UpdateLogicState();

        //if in range -> melee attack
        if (boss.CheckInMeleeRange())
        {
            stateMachine.ChangeState(boss.hammerState);
        }
    }

    public override void UpdatePhysicsState()
    {
        base.UpdatePhysicsState();
        boss.Move();
    }

    public override void ExitState()
    {
        boss.Stop();
        Debug.Log("exit chase movement"); //doesn't get called because there's no logic for it
        base.ExitState();
    }
}
