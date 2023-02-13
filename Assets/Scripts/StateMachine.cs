using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public enum State
    {
        Move,
        Attack,
        StunLock
    }

    public enum Movement
    {
        Idle,
        Dash_Torward,
        Backdash,
        Channeling,
        Reposition
    }
    public enum Combat
    {

    }

    private State _state; // local variable that represent our state
    public Movement movement;
    public Combat combat;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void SwitchMovement()
    {
      
    }
}
