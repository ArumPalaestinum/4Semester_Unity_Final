using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    // Movement
    float movingSpeed;
    Rigidbody rb;
    Vector2 inputVector;
    [SerializeField] float defaultSpeed = 5f;
    [SerializeField] float sprintSpeed = 8f;
    [SerializeField] float sneakSpeed = 1.5f;
    [SerializeField] float jumpForce = 5f;

    // Ground check
    [SerializeField] private Transform transformRayStart;
    [SerializeField] private float rayLength = 0.5f;
    [SerializeField] private LayerMask layerGroundCheck;

    // Slope check
    [SerializeField] float maxAngleSlope = 30f;

    // Camera
    [SerializeField] Transform transformCameraFollow;
    [SerializeField] float rotationSensetivity = 1f;
    private float cameraPitch;
    private float cameraRoll;
    [SerializeField] private float maxCamPitch = 80f;

    private void Start() 
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Rotation freeze

        movingSpeed = defaultSpeed;
    }

    void Update()
    {
        Vector2 mouseDelta = Mouse.current.delta.ReadValue(); // Where is the mouse

        // Camera movement related to the mouse movement
        cameraPitch = cameraPitch + mouseDelta.y * rotationSensetivity;
        cameraPitch = Mathf.Clamp(cameraPitch, -maxCamPitch, maxCamPitch);

        cameraRoll = cameraRoll + mouseDelta.x * rotationSensetivity;

        transformCameraFollow.localEulerAngles = new Vector3(cameraPitch, cameraRoll, 0f);
    }


    //filled with a bunch movement related things. a nightmare to debug and actually do
    private void FixedUpdate()
    {
        // Calculate the camera's forward and right vectors
        Vector3 cameraForward = transformCameraFollow.forward;
        Vector3 cameraRight = transformCameraFollow.right;

        // Ensure movement is only in the horizontal plane
        cameraForward.y = 0;
        cameraRight.y = 0;

        // Normalize vectors to get correct direction
        cameraForward.Normalize();
        cameraRight.Normalize();

        // Calculate the movement direction based on input and camera orientation
        Vector3 moveDirection = cameraForward * inputVector.y + cameraRight * inputVector.x;

        // Check if the player is on a slope within the acceptable angle
        if (SlopeCheck())
        {
            // Apply movement direction to the player's velocity
            rb.velocity = new Vector3(moveDirection.x * movingSpeed, rb.velocity.y, moveDirection.z * movingSpeed);
        }
        else
        {
            // If on a steep slope, only apply vertical velocity (stop movement on steep slopes)
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }

    void OnJump()
    {
        // If you touch the ground you can jump
        if (GroundCheck())
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z); // Use current horizontal velocity
        }
    }

    // Sneaking
    void OnSneak(InputValue inputValue)
    {
        float isSneak = inputValue.Get<float>();

        // If you're not sneaking you're walking normally
        if (Mathf.RoundToInt(isSneak) == 1)
        {
            movingSpeed = sneakSpeed;
        }
        else
        {
            movingSpeed = defaultSpeed;
        }
    }

    // Use new imput system 
    void OnMove(InputValue inputValue)
    {
        inputVector = inputValue.Get<Vector2>();
    }

    // Basically the same as sneak
    void OnSprint(InputValue inputValue)
    {
        float isSprint = inputValue.Get<float>();

        // Round float to nearest int that we can compare it to a whole number
        if (Mathf.RoundToInt(isSprint) == 1)
        {
            movingSpeed = sprintSpeed;
        }
        else
        {
            movingSpeed = defaultSpeed;
        }
    }

    // Are you touching the ground? 
    bool GroundCheck()
    {
        return Physics.Raycast(transformRayStart.position, Vector3.down, rayLength, layerGroundCheck);
    }

    // Basically if you see the ground as a mirror, is the raycast sloped or not?
    bool SlopeCheck()
    {
        RaycastHit hit;
        Physics.Raycast(transformRayStart.position, Vector3.down, out hit, rayLength, layerGroundCheck);

        if (hit.collider != null)
        {
            float angle = Vector3.Angle(hit.normal, Vector3.up);
            if (angle > maxAngleSlope)
            {
                return false;
            }
        }
        return true;
    }

    // Winning / Dying
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WinZone"))
        {
            SceneManager.LoadScene("Winning");
        }

        if (other.CompareTag("Enemy"))
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}