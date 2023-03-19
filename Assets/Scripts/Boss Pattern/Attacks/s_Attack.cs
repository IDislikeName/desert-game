using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_Attack : State
{
    public int damage;
    // public GameObject[] projectiles;
    // public GameObject[] mainWeapon;
    public GameObject weapon;

    public s_Attack(StateMachine stateMachine, Boss boss) : base(stateMachine, boss) { }

    public virtual void SpawnWeapon() { }
}

