
using UnityEngine;
using System.Collections.Generic;


public class UnlimitWheels : MonoBehaviour
{
    public List<HingeJoint> Wheel;


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
        foreach (var wh in Wheel)
        {
            wh.motor = motor;    
        }

    }
}
