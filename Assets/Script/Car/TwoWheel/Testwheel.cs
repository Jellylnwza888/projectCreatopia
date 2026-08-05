using UnityEngine;
using System.Collections.Generic;

public class Testwheel : MonoBehaviour
{
     public HingeJoint leftWheel;
    public HingeJoint rightWheel;
    public HingeJoint FrontRightWheel;
    public HingeJoint FrontLeftWheel;
    public float motorForce = 500f; // Motor power
    public float maxMotorSpeed = 1000f; // Max motor speed

    void Update()
    {
        JointMotor motor = new JointMotor();
        motor.force = motorForce;

        if (Input.GetKey(KeyCode.W))
        {
            motor.targetVelocity = maxMotorSpeed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            motor.targetVelocity = -maxMotorSpeed;
        }
        else
        {
            motor.targetVelocity = 0;
        
        }

        leftWheel.motor = motor;
        rightWheel.motor = motor;
        FrontRightWheel.motor = motor;
        FrontLeftWheel.motor = motor;
    }
}
