using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    protected Boss boss;
    protected StateMachine stateMachine;

    public State(StateMachine stateMachine, Boss boss)
    {
        this.stateMachine = stateMachine;
        this.boss = boss;
    }

    public virtual void EnterState() { }

    public virtual void UpdateLogicState()
    {
        // Debug.Log("state logic");
    } //update

    public virtual void UpdatePhysicsState()
    {
        // Debug.Log("state physics");
    } //fixed update

    public virtual void ExitState() { }
}