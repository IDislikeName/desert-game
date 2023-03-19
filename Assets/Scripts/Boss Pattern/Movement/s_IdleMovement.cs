using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_IdleMovement : s_Movement
{
    public s_IdleMovement(StateMachine stateMachine, Boss boss) : base(stateMachine, boss) { }

    float idleTime;
    bool idleTimeIsOver;

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("enter idle movement");

        idleTimeIsOver = false;
        idleTime = Random.Range(2f, 5f); //idle for this many seconds
    }

    public override void UpdateLogicState()
    {
        base.UpdateLogicState();
        idleTime -= Time.deltaTime;
        if (idleTime <= 0f)
        {
            idleTimeIsOver = true;
            // stateMachine.ChangeState(boss.chaseState);
            stateMachine.ChangeState(boss.pillarState);
        }
    }

    public override void ExitState()
    {
        Debug.Log("exit idle movement"); //doesn't get called because there's no logic for it
        base.ExitState();
    }
}
