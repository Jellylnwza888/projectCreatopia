using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class CarController : MonoBehaviour
{


    public float acceleration = 10f;
    public float steering = 2f;
    public float maxSpeed = 20f;
    public float drag = 0.98f;

    private Rigidbody rb;
    private bool isDriving = false; // เพิ่มตัวแปรนี้

    public List<FrontWheel> frontWheels = new List<FrontWheel>();
    public List<RearWheel> rearWheels = new List<RearWheel>();

    public void AddFrontWheel(GameObject obj)
    {
        FrontWheel wheel = obj.AddComponent<FrontWheel>();
        wheel.wheelCollider = obj.GetComponent<WheelCollider>();
        wheel.wheelMesh = obj.transform.Find("WheelMesh");
        frontWheels.Add(wheel);
    }

    public void AddRearWheel(GameObject obj)
    {
        RearWheel wheel = obj.AddComponent<RearWheel>();
        wheel.wheelCollider = obj.GetComponent<WheelCollider>();
        wheel.wheelMesh = obj.transform.Find("WheelMesh");
        rearWheels.Add(wheel);
    }



    void SetupWheel(GameObject obj, bool isFrontWheel)
    {
        Wheel wheelScript = obj.AddComponent<Wheel>();
        wheelScript.wheelCollider = obj.GetComponent<WheelCollider>();
        wheelScript.wheelMesh = obj.transform.Find("WheelMesh");
        wheelScript.wheelTurn = isFrontWheel;
        wheelScript.isForce = true;
        wheelScript.speed = 500;
    }


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    [System.Obsolete]
    void Update()
    {
        if (isDriving)
        {
            HandleMovement();
        }
    }

    [System.Obsolete]
    private void HandleMovement()
    {
        float moveInput = Input.GetAxisRaw("Vertical");
        float steerInput = Input.GetAxisRaw("Horizontal");

        if (moveInput != 0)
        {
            transform.Rotate(0, 0, -steerInput * steering * moveInput);
        }

        Vector2 force = transform.up * moveInput * acceleration;
        rb.AddForce(force);

        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }

        rb.velocity *= drag;
    }

    [System.Obsolete]
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("ชนกับ: " + collision.gameObject.name);
        rb.velocity *= 0.5f;
    }

    // --- ฟังก์ชันที่เรียกเมื่อต้องการนั่งรถ ---
    public void EnterCar()
    {
        isDriving = true;
    }

    // --- ฟังก์ชันที่เรียกเมื่อลงจากรถ ---
    public void ExitCar()
    {
        isDriving = false;
    }
}