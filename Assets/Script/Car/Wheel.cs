using UnityEngine;

public class Wheel : MonoBehaviour
{
    public WheelCollider wheelCollider;
    public Transform wheelMesh;
    public bool wheelTurn; // true = ล้อหน้า
    public bool isForce = true;
    public int speed = 500;
    private Rigidbody parentRb;

    void Start()
    {
        if (transform.parent != null)
        {
            parentRb = transform.parent.GetComponent<Rigidbody>();
        }
    }

    void Update()
    {
        UpdateWheelVisual();
    }

    void FixedUpdate()
    {
        if (wheelCollider == null) return;

        // ขับเคลื่อน
        if (Input.GetKey(KeyCode.W))
        {
            wheelCollider.motorTorque = speed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            wheelCollider.motorTorque = -speed;
        }
        else
        {
            wheelCollider.motorTorque = 0;
        }

        // เลี้ยว เฉพาะล้อหน้า
        if (wheelTurn)
        {
            float steerInput = Input.GetAxis("Horizontal"); // A / D
            wheelCollider.steerAngle = steerInput * 30f; // ปรับองศาตามต้องการ
        }
    }

    void UpdateWheelVisual()
    {
        if (wheelTurn)
        {
            wheelMesh.localEulerAngles = new Vector3(
                wheelMesh.localEulerAngles.x,
                wheelCollider.steerAngle,
                wheelMesh.localEulerAngles.z
            );
        }

        float rotateSpeed = wheelCollider.rpm / 60f * 360f * Time.deltaTime;
        wheelMesh.Rotate(0, 0,rotateSpeed); 
    }
}
