using UnityEngine;

public class RearWheel : MonoBehaviour
{
    public float acceleration = 2000f; //อัตราเร่งของล้อ
    public float steering = 2f;
    public float maxSpeed = 10000f;
    public float drag = 0.98f;
    public Rigidbody rb;

    private bool isDriving = false; // เพิ่มตัวแปรนี้

    public WheelCollider wheelCollider;
    public Transform wheelMesh;
    public int motorForce = 500;
    private Quaternion intialRotaion;

    void Update()
    {
        UpdateVisual();
    }

    void FixedUpdate()
    {
        float moveInput = Input.GetAxis("Vertical");
        wheelCollider.motorTorque = moveInput * motorForce;
    }

    void UpdateVisual()
    {
        float rotateSpeed = wheelCollider.rpm / 60f * 360f * Time.deltaTime; //การหมุนล้อ
        wheelMesh.Rotate(0 , 0,rotateSpeed);

        intialRotaion = transform.rotation;

        transform.rotation = Quaternion.Euler(intialRotaion.eulerAngles.x, transform.rotation.eulerAngles.y,intialRotaion.eulerAngles.z);
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
}
