using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Movem : MonoBehaviour
{
    // Variables
    [SerializeField] private float walkSpeed = 3f;
    [SerializeField] private float runSpeed = 6f;

    // Bow Movement
    [Header("Bow Movement Settings")]
    public float bowWalkSpeed = 3f;

    private Vector3 velocity;
    private bool isGrounded;

    // Gravity and Ground Check
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float groundCheckRadius = 0.4f;
    [SerializeField] private LayerMask groundMask;

    // Jumping
    [SerializeField] private float jumpHeight = 1.5f;

    // References
    private CharacterController controller;
    private Transform cameraTransform;

    // [SerializeField] private Transform aimPosition; // Aim target
    // [SerializeField] private float aimSpeed = 0.5f;
    // [SerializeField] private LayerMask aimMask;

    private void BowMovement()
    {
        BowMovementInput();
    }

    void BowMovementInput()
    {
        float bowMoveX = Input.GetAxis("Horizontal");
        float bowMoveZ = Input.GetAxis("Vertical");

        if (Input.GetMouseButton(1))
        {
            Vector3 bowMoveDirection = new Vector3(bowMoveX, 0f, bowMoveZ).normalized;

            if (bowMoveDirection.magnitude > 0.1f)
            {
                Vector3 moveDirection = transform.TransformDirection(bowMoveDirection);
                transform.position += moveDirection * bowWalkSpeed * Time.deltaTime;

                if (cameraTransform != null)
                {
                    Vector3 lookDirection = cameraTransform.forward;
                    lookDirection.y = 0f;
                    if (lookDirection.magnitude > 0.1f)
                    {
                        transform.rotation = Quaternion.LookRotation(lookDirection);
                    }
                }
            }
        }
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
        cameraTransform = Camera.main?.transform; // Safely get the camera reference

        // Lock the mouse cursor
        SetCursorState(true);
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            BowMovementInput();
        }
        else
        {
            Move();
        }

        // Unlock the cursor for debugging or menus
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetCursorState(false);
        }
        else if (Input.GetMouseButtonDown(0)) // Re-lock cursor on left mouse click
        {
            SetCursorState(true);
        }
    }


    private void Move()
    {
        // Ground Check
        Vector3 groundCheckPosition = controller.bounds.center + Vector3.down * (controller.bounds.extents.y + groundCheckRadius);
        isGrounded = Physics.CheckSphere(groundCheckPosition, groundCheckRadius, groundMask);

        if (isGrounded)
        {
            if (velocity.y < 0)
            {
                velocity.y = -2f; // Stabilize when grounded
            }

            // Jump logic
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Jump triggered!");
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }

        // Input for Movement (W, A, S, D or Arrow Keys)
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Calculate movement direction relative to the camera
        Vector3 cameraForward = Vector3.Scale(cameraTransform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 cameraRight = Vector3.Scale(cameraTransform.right, new Vector3(1, 0, 1)).normalized;
        Vector3 moveDir = (cameraForward * moveZ + cameraRight * moveX).normalized;

        // Rotate the player to face the movement direction
        if (moveDir.magnitude >= 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }

        // Move the player
        bool isRunning = Input.GetKey(KeyCode.LeftShift) && moveZ > 0.1f;
        float speed = isRunning ? runSpeed : walkSpeed;
        Vector3 moveVelocity = moveDir * speed;
        controller.Move(moveVelocity * Time.deltaTime);

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void SetCursorState(bool locked)
    {
        Cursor.lockState = locked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !locked;
    }
}