// using UnityEngine;
// using UnityEngine.UI;

// public class RightClickRotate : MonoBehaviour
// {
//     public GameObject rotateIconUI; // UI สำหรับปุ่ม Rotate
//     private GameObject Objclick; // เก็บ Cube ที่ถูกคลิก

//     // Rotation speed for WASD keys
//     public float rotationSpeed = 90f; // 90 degrees per second

//     void Update()
//     {
//         // Right-click rotation selection logic
//         if (Input.GetMouseButtonDown(1)) // คลิกขวา
//         {
//             RaycastHit hit;
//             Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

//             if (Physics.Raycast(ray, out hit))
//             {
//                 if (hit.collider.CompareTag("Rotatable")) // ต้องตั้ง Tag เป็น "Rotatable"
//                 {
//                     Objclick = hit.collider.gameObject;
//                     ShowRotateIcon(hit.point);
//                 }
//             }
//         }

//         // WASD rotation controls
//         if (Objclick != null)
//         {
//             // Rotate up (W)
//             if (Input.GetKey(KeyCode.W))
//             {
//                 Objclick.transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
//             }
//             // Rotate down (S)
//             if (Input.GetKey(KeyCode.S))
//             {
//                 Objclick.transform.Rotate(Vector3.left, rotationSpeed * Time.deltaTime);
//             }
//             // Rotate left (A)
//             if (Input.GetKey(KeyCode.A))
//             {
//                 Objclick.transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
//             }
//             // Rotate right (D)
//             if (Input.GetKey(KeyCode.D))
//             {
//                 Objclick.transform.Rotate(Vector3.back, rotationSpeed * Time.deltaTime);
//             }
//         }
//     }

//     public void RotateObject() // ฟังก์ชันให้ปุ่ม Rotate ใช้
//     {
//         if (Objclick != null)
//         {
//             Objclick.transform.Rotate(Vector3.up, 45f); // หมุนทีละ 45 องศา
//         }
//     }

//     private void ShowRotateIcon(Vector3 position)
//     {
//         rotateIconUI.SetActive(true);
//         rotateIconUI.transform.position = Camera.main.WorldToScreenPoint(position); // แปลงตำแหน่ง 3D → 2D
//     }
// }