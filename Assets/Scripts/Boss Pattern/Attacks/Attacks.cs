using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    protected StateMachine stateMachine;

    public State(StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public virtual void EnterState()
    {

    }

    public virtual void UpdateLogicState()
    {

    } //update

    public virtual void UpdatePhysicsState()
    {

    } //fixed update

    public virtual void ExitState()
    {

    }
}

public class Attack : State
{

    public int damage;
    public GameObject[] projectiles;
    public GameObject[] mainWeapon;

    public Attack(StateMachine stateMachine) : base(stateMachine)
    {
        // this.stateMachine = stateMachine;
    }

    public override void EnterState()
    {
        throw new System.NotImplementedException();
    }
    public override void UpdateLogicState()
    {

        throw new System.NotImplementedException();
    }

    public override void UpdatePhysicsState()
    {

        throw new System.NotImplementedException();
    }

    public override void ExitState()
    {

        throw new System.NotImplementedException();
    }
    public void SpawnWeapon()
    {

    }
}

