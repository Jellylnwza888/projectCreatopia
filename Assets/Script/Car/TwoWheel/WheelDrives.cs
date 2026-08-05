using UnityEngine;

public class WheelDrives : MonoBehaviour
{
    public RearWheel leftWheel; //ดึงscipterของล้อซ้าย
    public RearWheel rightWheel; //
    public RearWheel leftSteerWheel; 
    public RearWheel rightSteerWheel; 

    //----------------ตัวควบคุมแรงของล้อ
    
    //public float motorForce = 500f;
    //public float maxMotorSpeed = 1000f;
    //public float steerAngle = 30f;

    public void Drive()
    {
        leftWheel.enabled = true;
        rightWheel.enabled = true;
        leftSteerWheel.enabled = true;
        rightSteerWheel.enabled = true;
        
    }

    public void StopDrive()
    {
        leftWheel.enabled = false;
        rightWheel.enabled = false;
        leftSteerWheel.enabled = false;
        rightSteerWheel.enabled = false;
    }

    void Update()
    {
        //if (!enabled) return; // ไม่ทำงานหากสคริปต์ถูกปิด
        
        //HandleMovement();
        //HandleSteering();
    }

    // private void HandleMovement()
    // {
    //     JointMotor motor = new JointMotor();
    //     motor.force = motorForce;

    //     if (Input.GetKey(KeyCode.W))
    //     {
    //         motor.targetVelocity = maxMotorSpeed;
    //     }
    //     else if (Input.GetKey(KeyCode.S))
    //     {
    //         motor.targetVelocity = -maxMotorSpeed;
    //     }
    //     else
    //     {
    //         motor.targetVelocity = 0;
    //     }

    //     leftWheel.motor = motor;
    //     rightWheel.motor = motor;
    // }

    // private void HandleSteering()
    // {
    //     float steerInput = Input.GetAxis("Horizontal");
    //     JointSpring spring = new JointSpring();
    //     spring.spring = 1000f;
    //     spring.damper = 100f;
    //     spring.targetPosition = steerInput * steerAngle;

    //     leftSteerWheel.spring = spring;
    //     rightSteerWheel.spring = spring;
    // }
}