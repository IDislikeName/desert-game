using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM 
using UnityEngine.InputSystem;
#endif

[RequireComponent(typeof(Rigidbody))]
#if ENABLE_INPUT_SYSTEM
[RequireComponent(typeof(PlayerInput))]
#endif
public class PlayerMovement : MonoBehaviour
{
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

    [Header("Dash")]
    [SerializeField]
    float dashForce;
    [SerializeField]
    float dashCoolDown;
    [SerializeField]
    float dashSpeedLimit;
    [SerializeField]
    float dashDuration;
    bool dashing = false;
    bool readyToDash = true;

    [Header("Grapple")]
    [SerializeField]
    float grappleCoolDown;
    bool grappling = false;
    bool readyToGrapple = true;
    LineRenderer line;
    [SerializeField] LayerMask grapplableMask;
    [SerializeField] float maxDistance = 10f;
    [SerializeField] float grappleSpeed = 10f;
    [SerializeField] float grappleShootSpeed = 20f;
    public bool retracting = false;
    Vector3 target;
    Vector3 grappleOffset;

    public enum MovementState { 
        Idle,
        Running,
        Dashing,
        Grappling
    }
    public MovementState currentMoveState;


    //Some system variables
#if ENABLE_INPUT_SYSTEM
    private PlayerInput playerInput;
#endif
    private Animator anim;
    private Rigidbody rb;
    private PlayerInputScript input;
    [SerializeField]
    private GameObject mainCamera;

    private bool IsCurrentDeviceMouse
    {
        get
        {
#if ENABLE_INPUT_SYSTEM
            return playerInput.currentControlScheme == "KeyboardMouse";
#else
				return false;
#endif
        }
    }

    //internal variables
    private Vector3 moveDirection;
    private float rotationVelocity;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        input = GetComponent<PlayerInputScript>();
        line = GetComponent<LineRenderer>();
#if ENABLE_INPUT_SYSTEM
        playerInput = GetComponent<PlayerInput>();
#endif
        grappleOffset = new Vector3(0, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //camera
        mainCamera.transform.position = transform.position + new Vector3(0, 8.5f, -9);

        InputUpdate();
        SpeedControl();
        AnimationUpdate();
        StateHandler();
        if (retracting)
        {
            Vector3 grapplePos = Vector3.Lerp(transform.position, target, grappleSpeed * Time.deltaTime);

            transform.position = grapplePos;

            line.SetPosition(0, transform.position+ grappleOffset);

            if (Vector3.Distance(transform.position+ grappleOffset, target+ grappleOffset) < 0.5f)
            {
                retracting = false;
                grappling = false;
                line.enabled = false;
                readyToGrapple = true;
            }
        }
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void InputUpdate()
    {
        moveDirection = new Vector3(input.move.x, 0, input.move.y).normalized;
        if (input.dash)
        {
            input.dash = false;
            if(readyToDash&&currentMoveState!=MovementState.Grappling)
                Dash();
        }
        if (input.grapple)
        {
            input.grapple = false;
            if (readyToGrapple && currentMoveState != MovementState.Dashing)
                Startgrapple();

        }
    }
    private void Move()
    {
        if (currentMoveState == MovementState.Dashing) return;
        if (currentMoveState == MovementState.Grappling) return;
        rb.AddForce(moveDirection.normalized * accelForce * 10f, ForceMode.Force);
        if (input.move != Vector2.zero)
        {
            var _targetRotation = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg +
                                mainCamera.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref rotationVelocity,
                rotationSmoothTime);

            // rotate to face input direction relative to camera position
            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }
        
    }
    private void SpeedControl()
    {
        rb.drag = drag;
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        float curSpeed = moveSpeed;
        // limit velocity if needed
        if (flatVel.magnitude > curSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
    private void AnimationUpdate()
    {
        if (currentMoveState ==MovementState.Running)
        {
            anim.SetBool("Running",true);
        }
        else
        {
            anim.SetBool("Running", false);
        }
    }

    void StateHandler()
    {
        if (grappling)
        {
            currentMoveState = MovementState.Grappling;
        }
        else if (dashing)
        {
            currentMoveState = MovementState.Dashing;
            moveSpeed = dashSpeedLimit;
        }
        else if (input.move!=Vector2.zero)
        {
            currentMoveState = MovementState.Running;
            moveSpeed = walkSpeedlimit;
        }
        else
        {
            currentMoveState = MovementState.Idle;
        }
    }

    void Dash()
    {
        readyToDash = false;
        dashing = true;
        rb.AddForce(transform.forward * dashForce, ForceMode.Impulse);
        StartCoroutine(ResetDash());
    }
    IEnumerator ResetDash()
    {
        yield return new WaitForSeconds(dashDuration);
        dashing = false;
        
        yield return new WaitForSeconds(dashCoolDown);
        readyToDash = true;
    }

    void Startgrapple()
    {
        readyToGrapple = false;
        Vector3 direction = Aim();
        transform.forward = direction;

        RaycastHit hit;
        if (Physics.Raycast(transform.position + grappleOffset, transform.forward, out hit, maxDistance, grapplableMask))
        {
            grappling = true;
            target = hit.point;
            line.enabled = true;
            line.positionCount = 2;

            StartCoroutine(GrappleHit());
        }
        else
        {
            grappling = true;
            target = transform.position+ transform.forward * maxDistance;
            line.enabled = true;
            line.positionCount = 2;
            StartCoroutine(GrappleMiss());
        }
    }
    IEnumerator GrappleHit()
    {
        float t = 0;
        float time = 10;

        line.SetPosition(0 ,transform.position+ grappleOffset);
        line.SetPosition(1,transform.position + grappleOffset);

        Vector3 newPos;

        for (; t < time; t += grappleShootSpeed * Time.deltaTime)
        {
            newPos = Vector3.Lerp(transform.position, target, t / time);
            line.SetPosition(0, transform.position+ grappleOffset);
            line.SetPosition(1, newPos);
            yield return null;
        }

        line.SetPosition(1, target);
        retracting = true;
    }
    IEnumerator GrappleMiss()
    {
        float t = 0;
        float time = 10;

        line.SetPosition(0, transform.position + grappleOffset);
        line.SetPosition(1, transform.position + grappleOffset);

        Vector3 newPos;

        for (; t < time; t += grappleShootSpeed * Time.deltaTime)
        {
            newPos = Vector3.Lerp(transform.position+ grappleOffset, target, t / time);
            line.SetPosition(0, transform.position+ grappleOffset);
            line.SetPosition(1, newPos);
            yield return null;
        }

        line.SetPosition(1, target);
        t = 0;
        time = 10;

        for (; t < time; t += grappleShootSpeed * Time.deltaTime)
        {
            newPos = Vector3.Lerp(target, transform.position+ grappleOffset, t / time);
            line.SetPosition(0, transform.position+ grappleOffset);
            line.SetPosition(1, newPos);
            yield return null;
        }

        line.SetPosition(1, transform.position + grappleOffset);
        grappling = false;
        line.enabled = false;
        readyToGrapple = true;
    }
    private Vector3 Aim()
    {
        var (success, position) = GetMousePosition();
        if (success)
        {
            // Calculate the direction
            Vector3 direction = position - transform.position;

            // You might want to delete this line.
            // Ignore the height difference.
            direction.y = 0;

            return direction;
            // Make the transform look in the direction.
            //transform.forward = direction;
        }
        else
        {
            return Vector3.zero;
        }
    }

    private (bool success, Vector3 position) GetMousePosition()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity))
        {
            // The Raycast hit something, return with the position.
            return (success: true, position: hitInfo.point);
        }
        else
        {
            // The Raycast did not hit anything.
            return (success: false, position: Vector3.zero);
        }
    }
}
