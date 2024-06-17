using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
        
    //Movement
    float movingSpeed;

    Rigidbody rb;
    Vector2 inputVector;
    [SerializeField] float defaultSpeed = 5f;
    [SerializeField] float sprintSpeed = 8f;
    [SerializeField] float sneakSpeed = 1.5f;
    [SerializeField] float jumpForce = 5f;

    //groundcheck
    [SerializeField] private Transform transformRayStart;
    [SerializeField] private float rayLength = 0.5f;
    [SerializeField] private LayerMask layerGroundCheck;

    //slopcheck
    [SerializeField] float maxAngleSlope = 30f;


    //cam
    [SerializeField] Transform transformCameraFollow;
    [SerializeField] float rotationSensetivity = 1f;
    private float cameraPitch;
    private float cameraRoll;
    [SerializeField] private float maxCamPitch = 80f;

    private void Start() //make sure everything is set right from the start
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; //rotation freeze

        movingSpeed = defaultSpeed;
    }

    void Update()
    {
        Vector2 mouseDelta = Mouse.current.delta.ReadValue(); //where is the mouse

        //cam movement related to the mosue movement
        cameraPitch = cameraPitch + mouseDelta.y * rotationSensetivity;
        cameraPitch = Mathf.Clamp(value: cameraPitch, min: -maxCamPitch, maxCamPitch);

        cameraRoll = cameraRoll + mouseDelta.x * rotationSensetivity;

        transformCameraFollow.localEulerAngles = new Vector3(cameraPitch, cameraRoll, 0f);



    }

    private void FixedUpdate()
    {
       //change movement if slope is too high
        
        if (SlopeCheck())
        {
            rb.velocity = new Vector3(inputVector.x * movingSpeed, rb.velocity.y, inputVector.y * movingSpeed);
        }
    }

    void OnJump()
    {
        //if you touch the ground you can jump
        if (GroundCheck())
        {
            rb.velocity = new Vector3(0f, jumpForce, 0f);
        }
    }

    //sneaking
    void OnSneak(InputValue inputValue)
    {
      
        float isSneak = inputValue.Get<float>();

        //if youre not sneaking youre walking normally
        
        if (Mathf.RoundToInt(isSneak) == 1)
        {
            movingSpeed = sneakSpeed;
        }
        else
        {
            movingSpeed = defaultSpeed;
        }
    }

    void OnMove(InputValue inputValue)
    {
        inputVector = inputValue.Get<Vector2>();
    }


    //basically the same as sneak
    void OnSprint(InputValue inputValue)
    {
        
        float isSprint = inputValue.Get<float>();

        //round float to nearest int that we can compare it to a whole number
        if (Mathf.RoundToInt(isSprint) == 1)
        {
            movingSpeed = sprintSpeed;
        }
        else
        {
            movingSpeed = defaultSpeed;
        }
    }

    //are you touching the ground? 
    bool GroundCheck()
    {
        return Physics.Raycast(transformRayStart.position, Vector3.down, rayLength, layerGroundCheck);
    }


    //basically if you see the ground as a mirror, is the raycast sloped or not?
    bool SlopeCheck()
    {
        RaycastHit hit;

        Physics.Raycast(transformRayStart.position, Vector3.down, out hit, rayLength, layerGroundCheck);

        if(hit.collider != null)
        {
            float angle = Vector3.Angle(hit.normal, Vector3.up);
            if (angle > maxAngleSlope)
            {
                return false;
            }
        }
        return true;
    }

    //WINNING / Dying
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
