using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;          // ตัวรถ
    public Vector3 offset = new Vector3(0f, 5f, -10f);  // ระยะห่างกล้อง
    public float followSpeed = 10f;   // ความเร็วในการตาม

    void LateUpdate()
    {
        if (target == null) return;

        // คำนวณตำแหน่งเป้าหมายของกล้อง
        Vector3 desiredPosition = target.position + offset;

        // เคลื่อนตำแหน่งกล้องอย่างนุ่มนวล
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

        // กล้องมองไปที่รถ
        transform.LookAt(target);
    }
}
