// using UnityEngine;

// public class SnapManager : MonoBehaviour
// {
//     public GameObject bodyObject; // อ้างถึงตัว body ที่มี WheelDrives ติดอยู่

//     private void OnTriggerEnter(Collider other)
//     {
//         if (other.CompareTag("Wheel"))
//         {
//             HingeJoint wheelHinge = other.GetComponent<HingeJoint>();
//             WheelDrives wheelDrives = bodyObject.GetComponent<WheelDrives>();

//             // เช็คตำแหน่ง snap slot (ตัวอย่าง: ใช้ชื่อ slot หรือตำแหน่ง world)
//             if (transform.name == "LeftWheelSlot")
//             {
//                 wheelDrives.leftWheel = wheelHinge;
//                 Debug.Log("Assigned LEFT wheel!");
//             }
//             else if (transform.name == "RightWheelSlot")
//             {
//                 wheelDrives.rightWheel = wheelHinge;
//                 Debug.Log("Assigned RIGHT wheel!");
//             }

//             // ดูดเข้า slot (snap effect)
//             other.transform.position = transform.position;
//             other.transform.rotation = transform.rotation;
//         }
//     }
// }
