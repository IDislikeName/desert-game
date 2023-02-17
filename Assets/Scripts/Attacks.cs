using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class State : MonoBehaviour
{
    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();

}

public class Attack : State


{

    public int damage;
    public GameObject[] projectiles;
    public GameObject[] mainWeapon;
    public override void EnterState()
    {
        
        throw new System.NotImplementedException();
    }
    public override void UpdateState()
    {

        throw new System.NotImplementedException();
    }
    public override void ExitState()
    {

        throw new System.NotImplementedException();
    }

    public void Execute()
    {
       
    }

    public void SpawnWeapon()
    {

    }
    
    
}

