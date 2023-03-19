using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [HideInInspector] public StateMachine stateMachine;
    // [HideInInspector] public GameObject _gameObject;
    // [HideInInspector] public Transform _transform;
    [HideInInspector] public Rigidbody _rb;

    [HideInInspector] public s_LeapAttack leapAttackState;
    [HideInInspector] public s_IdleMovement idleMovementState;
    [HideInInspector] public s_Chase chaseState;
    [HideInInspector] public s_Hammer hammerState;
    [HideInInspector] public s_Pillar pillarState;


    [Header("Movement")]
    [SerializeField]
    float accelForce;
    [SerializeField]
    float walkSpeedlimit;
    [SerializeField]
    float moveSpeed;
    [SerializeField]
    float drag;
    [Tooltip("How fast the character turns to face movement direction")]
    [Range(0.0f, 0.3f)]
    public float rotationSmoothTime = 0.12f;
    private Vector3 moveDirection;
    private float rotationVelocity;

    [Header("Triggers and other game objects for boss logic.")]
    public GameObject target; //TODO: Set dynamically
    [Tooltip("Where the melee hitbox is.")]
    public GameObject melee_hitbox;

    public LayerMask whatIsPlayer;


    [Header("Boss weapons.")]
    public GameObject pillarPrefab;
    public GameObject hammerPrefab;

    private void Start()
    {
        // _gameObject = this.gameObject;
        _rb = this.GetComponent<Rigidbody>();
        // _transform = _gameObject.GetComponent<Transform>();

        stateMachine = new StateMachine();

        leapAttackState = new s_LeapAttack(stateMachine, this);
        idleMovementState = new s_IdleMovement(stateMachine, this);
        chaseState = new s_Chase(stateMachine, this);
        hammerState = new s_Hammer(stateMachine, this);
        pillarState = new s_Pillar(stateMachine, this, pillarPrefab);

        stateMachine.Initialize(idleMovementState);
    }

    private void Update()
    {
        //TODO: REMOVE (only for test purposes)
        if (Input.GetKeyDown(KeyCode.R))
        {
            stateMachine.ChangeState(idleMovementState);
        }

        stateMachine.currentState.UpdateLogicState();
    }

    private void FixedUpdate()
    {
        stateMachine.currentState.UpdatePhysicsState();
    }

    public void Move()
    {
        if (target == null)
        {
            stateMachine.ChangeState(idleMovementState);
        }
        else
        {
            moveDirection = (target.transform.position - transform.position).normalized;
            _rb.AddForce(moveDirection.normalized * accelForce * 10f, ForceMode.Force);

            Quaternion lookRotation = Quaternion.LookRotation(moveDirection);

            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
        }
    }

    public void Stop()
    {
        _rb.velocity = Vector3.zero;
    }

    public bool CheckInMeleeRange()
    {
        Collider[] hits = Physics.OverlapSphere(melee_hitbox.transform.position, 1f, whatIsPlayer);
        if (hits.Length > 0)
            return true;

        return false;
    }

    private void OnDrawGizmos()
    {
        Color color = Color.red;
        color.a = 0.2f;
        Gizmos.color = color;

        Gizmos.DrawSphere(melee_hitbox.transform.position, 1f);
    }
}
