using System;
using UnityEngine;

public interface IPlayerMovement
{
    void Start();
}

public class PlayerMovement : MonoBehaviour, IPlayerMovement
{

    [Header("Movement")]
    public float moveSpeed;// ความเร็วในการเคลื่อนที่
    public float groundedDrag; // การลากเมื่ออยู่บนพื้น

    [Header("Ground Check")] 
    public float playerHeight; // ความสูงของตัวละคร
    public LayerMask whatIsGround; // เลเยอร์ที่ใช้ตรวจสอบพื้น
    bool grounded; // ตรวจสอบว่าตัวละครอยู่บนพื้นหรือไม่



    public Transform orientation; // ออริเอนเทชันของกล้อง

    float horizontalInput; // ค่าการเคลื่อนที่ในแนวนอน
    float verticalInput; // ค่าการเคลื่อนที่ในแนวตั้ง
    Vector3 MoveDirection; // ทิศทางการเคลื่อนที่

    Rigidbody rb; // Rigidbody ของตัวละคร

    public void Start()
    {
        rb = GetComponent<Rigidbody>(); // รับ Rigidbody ของตัวละคร
        rb.freezeRotation = true; // ปิดการหมุนของ Rigidbody
    }

    [Obsolete]
    private void Update()
    {
        // ตรวจสอบว่าตัวละครอยู่บนพื้นหรือไม่
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);

        Myinput(); 

        // ปรับค่าการลากของ Rigidbody ตามสถานะของตัวละคร
        if (grounded)
        {
            rb.drag = groundedDrag; // ถ้าตัวละครอยู่บนพื้นให้ใช้การลากที่กำหนด
        }
        else
        {
            rb.drag = 0; // ถ้าตัวละครอยู่ในอากาศให้ไม่ใช้การลาก
        }

        SpeedControl(); // เรียกใช้ฟังก์ชัน SpeedControl เพื่อควบคุมความเร็วของตัวละคร
    }

    private void Myinput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal"); // รับค่าการเคลื่อนที่ในแนวนอน
        verticalInput = Input.GetAxisRaw("Vertical"); // รับค่าการเคลื่อนที่ในแนวตั้ง
    }

    private void FixedUpdate()
    {
        Myinput(); // เรียกใช้ฟังก์ชัน Myinput เพื่อรับค่าการเคลื่อนที่
        Moveplayer(); // เรียกใช้ฟังก์ชัน Moveplayer เพื่อเคลื่อนที่ตัวละคร
    }

    private void Moveplayer()
    {
        // คำนวณทิศทางการเคลื่อนที่
        MoveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // เคลื่อนที่ตัวละคร
        rb.AddForce(MoveDirection.normalized * moveSpeed * 10f, ForceMode.Force); // ใช้ ForceMode.Force เพื่อให้การเคลื่อนที่นุ่มนวล
    }

    [Obsolete]
    private void SpeedControl()
    {
        // ควบคุมความเร็วของตัวละคร
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z); // รับค่าความเร็วในแนวนอน
        if (flatVel.magnitude > moveSpeed) // ถ้าความเร็วมากกว่าความเร็วที่กำหนด
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed; // จำกัดความเร็วให้เท่ากับความเร็วที่กำหนด
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z); // ตั้งค่าความเร็วใหม่
        }
    }
    




}
