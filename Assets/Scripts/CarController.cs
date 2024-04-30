using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private float horizontalInput, verticalInput;
    private float currentBreakForce, currentSteerAngle;
    private bool isBreaking;

    [SerializeField] private float motorForce, breakForce, maxSteerAngle, maxSpeed;

    [SerializeField] private WheelCollider frontWheelLeftCollider, frontWheelRightCollider, backWheelLeftCollider, backWheelRightCollider;
    [SerializeField] private Transform frontWheelLeftTransform, frontWheelRightTransform, backWheelLeftTransform, backWheelRightTransform;


    void FixedUpdate()
    {
        GetInput();
        Motor();
        Steering();
        UpdateAllWheels();
    }

    
    void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        isBreaking = Input.GetKey(KeyCode.Space);
    }
   
    void Motor()
    {
        float speed = verticalInput * motorForce;

        if(speed > maxSpeed)
        {
            speed = maxSpeed;
        }

        frontWheelLeftCollider.motorTorque = speed;
        frontWheelRightCollider.motorTorque = speed;
        currentBreakForce = isBreaking ? breakForce : 0f;
        Break();
    }
    
    void Break()
    {
       frontWheelLeftCollider.brakeTorque = currentBreakForce;
       frontWheelRightCollider.brakeTorque = currentBreakForce;
       backWheelLeftCollider.brakeTorque = currentBreakForce;
       backWheelRightCollider.brakeTorque = currentBreakForce; 
    }

    void Steering()
    {
       currentSteerAngle = maxSteerAngle * horizontalInput;
       frontWheelLeftCollider.steerAngle = currentSteerAngle;
       frontWheelRightCollider.steerAngle = currentSteerAngle;  
    }

    void UpdateAllWheels()
    {
        UpdateWheel(frontWheelLeftCollider, frontWheelLeftTransform);
        UpdateWheel(frontWheelRightCollider, frontWheelRightTransform);
        UpdateWheel(backWheelLeftCollider, backWheelLeftTransform);
        UpdateWheel(backWheelLeftCollider, backWheelLeftTransform);        
    }

    private void UpdateWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
       Vector3 position;
       Quaternion rotation;
       wheelCollider.GetWorldPose(out position, out rotation);
       wheelTransform.rotation = rotation;
       wheelTransform.position = position;
    }
}
