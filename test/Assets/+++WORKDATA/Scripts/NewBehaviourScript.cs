using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewBehaviourScript : MonoBehaviour
{
        
    //int updateCounter = 0;
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

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        movingSpeed = defaultSpeed;
    }

    void Update()
    {
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        cameraPitch = cameraPitch + mouseDelta.y * rotationSensetivity;
        cameraPitch = Mathf.Clamp(value: cameraPitch, min: -maxCamPitch, maxCamPitch);

        cameraRoll = cameraRoll + mouseDelta.x * rotationSensetivity;

        transformCameraFollow.localEulerAngles = new Vector3(cameraPitch, cameraRoll, 0f);



    }

    private void FixedUpdate()
    {
       
        
        if (SlopeCheck())
        {
            rb.velocity = new Vector3(inputVector.x * movingSpeed, rb.velocity.y, inputVector.y * movingSpeed);
        }
    }

    void OnJump()
    {
        Debug.Log("Jump");
        if (GroundCheck())
        {
            rb.velocity = new Vector3(0f, jumpForce, 0f);
        }
    }

    void OnSneak(InputValue inputValue)
    {
        Debug.Log("Sneak:" + inputValue.Get<float>());
        float isSneak = inputValue.Get<float>();

        //round float to nearest int that we can compare it to a whole number
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
        Debug.Log("move" + inputValue.Get<Vector2>());


        inputVector = inputValue.Get<Vector2>();


    }

    void OnSprint(InputValue inputValue)
    {
        Debug.Log("Sprint:" + inputValue.Get<float>());
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

    bool GroundCheck()
    {
        return Physics.Raycast(transformRayStart.position, Vector3.down, rayLength, layerGroundCheck);
    }

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

}
