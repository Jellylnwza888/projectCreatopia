using UnityEngine;

public class FrontWheel : MonoBehaviour
{
    public WheelCollider wheelCollider;
    public Transform wheelMesh;
    public float steerAngle = 30f;

    void Update()
    {
        UpdateVisual();
    }

    void FixedUpdate()
    {
        float steerInput = Input.GetAxis("Horizontal");
        wheelCollider.steerAngle = steerInput * steerAngle;
    }

    void UpdateVisual()
    {
        float rotateSpeed = wheelCollider.rpm / 60f * 360f * Time.deltaTime;  //การหมุนล้อ
        wheelMesh.Rotate(rotateSpeed, 0, 0);
        

        wheelMesh.localEulerAngles = new Vector3(
            wheelMesh.localEulerAngles.x,
            wheelCollider.steerAngle,
            wheelMesh.localEulerAngles.z
        );
    }
}
