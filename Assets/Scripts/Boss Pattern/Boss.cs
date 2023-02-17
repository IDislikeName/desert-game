using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public StateMachine stateMachine;
    public GameObject _gameObject;
    public Transform _transform;
    public Rigidbody _rb;

    [HideInInspector] public LeapAttack leapAttackState;
    [HideInInspector] public IdleMovement idleMovementState;

    private void Start()
    {
        _gameObject = this.gameObject;
        _rb = _gameObject.GetComponent<Rigidbody>();
        _transform = _gameObject.GetComponent<Transform>();

        stateMachine = new StateMachine();

        leapAttackState = new LeapAttack(stateMachine, this);
        idleMovementState = new IdleMovement(stateMachine, this);
        stateMachine.Initialize(leapAttackState);
    }

    private void Update()
    {
        stateMachine.currentState.UpdateLogicState();
    }

    private void FixedUpdate()
    {
        stateMachine.currentState.UpdatePhysicsState();
    }
}
