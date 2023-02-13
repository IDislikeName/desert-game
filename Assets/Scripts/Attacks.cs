using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class State : MonoBehaviour
{
    abstract public void NextState();

}

public class Attack : State


{

    public int damage;
    public GameObject[] projectiles;
    public GameObject[] mainWeapon;
    public override void NextState()
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

