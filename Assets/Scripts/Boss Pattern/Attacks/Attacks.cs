using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : State
{
    public int damage;
    public GameObject[] projectiles;
    public GameObject[] mainWeapon;

    public Attack(StateMachine stateMachine, Boss boss) : base(stateMachine, boss) { }

    public void SpawnWeapon() { }
}

