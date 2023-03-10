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
    float moveSpeed;
    [SerializeField]
    float drag;
    [Tooltip("How fast the character turns to face movement direction")]
    [Range(0.0f, 0.3f)]
    public float rotationSmoothTime = 0.12f;



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
#if ENABLE_INPUT_SYSTEM
        playerInput = GetComponent<PlayerInput>();
#endif
    }

    // Update is called once per frame
    void Update()
    {
        //camera
        mainCamera.transform.position = transform.position + new Vector3(0,10.5f,-11);
        moveDirection = new Vector3(input.move.x,0,input.move.y).normalized;
        SpeedControl();

    }
    private void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
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
}
