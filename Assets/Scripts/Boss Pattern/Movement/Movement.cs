using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : State
{
    public Movement(StateMachine stateMachine, Boss boss) : base(stateMachine, boss) { }
}
