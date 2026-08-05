// using UnityEngine;
// using System.Collections.Generic;

// public class Selection : MonoBehaviour
// {
//     public GameObject inventoryUI;
//     public Material highlightMaterial;
//     private List<GameObject> selectedObjects = new List<GameObject>();
//     private Dictionary<GameObject, Material> originalMaterials = new Dictionary<GameObject, Material>();
//     public Inventory inventorySystem; // อ้างอิงไปยังระบบ inventory ของคุณ

//     private void Update()
//     {
//         // ตรวจสอบว่ามีการคลิกเมาส์หรือไม่
//         if (Input.GetMouseButtonDown(0))
//         {
//             // สร้าง ray จากตำแหน่งเมาส์บนหน้าจอไปยังโลก 3D
//             Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//             RaycastHit hit;

//             // แสดง ray เพื่อการ debug
//             Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 1.0f);

//             if (Physics.Raycast(ray, out hit))
//             {
//                 Debug.Log("Hit: " + hit.transform.name); // บันทึกว่า ray ชนกับวัตถุอะไร

//                 var selection = hit.transform;
//                 if (selection != null && selection.CompareTag("Draggable"))
//                 {
//                     // ตรวจสอบการกด Ctrl
//                     bool isCtrlPressed = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);

//                     // ถ้าไม่ได้กด Ctrl และไม่ได้คลิกเลือกวัตถุที่เลือกไว้แล้ว ให้ล้างการเลือกเก่า
//                     if (!isCtrlPressed && !selectedObjects.Contains(selection.gameObject))
//                     {
//                         ClearSelection();
//                     }

//                     // ถ้ากด Ctrl + Click บนวัตถุที่เลือกไว้แล้ว จะเป็นการยกเลิกการเลือก
//                     if (selectedObjects.Contains(selection.gameObject))
//                     {
//                         DeselectObject(selection.gameObject);
//                     }
//                     else
//                     {
//                         // เลือกวัตถุใหม่
//                         SelectObject(selection.gameObject);
//                     }
//                 }
//                 else if (!Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.RightControl))
//                 {
//                     // คลิกบนพื้นที่ว่างและไม่ได้กด Ctrl จะล้างการเลือกทั้งหมด
//                     ClearSelection();
//                 }
//             }
//         }
//         if (Input.GetKeyDown(KeyCode.S) && selectedObjects.Count > 0)
//         {
//             inventoryUI.SetActive(!inventoryUI.activeSelf); // สลับการแสดงผล

//             // เมื่อเปิด inventory UI ให้บันทึกวัตถุที่เลือกลงใน inventory
//             if (inventoryUI.activeSelf)
//             {
//                 SaveToInventory();
//             }
//         }

//         // กดปุ่ม ESC เพื่อยกเลิกการเลือกทั้งหมด
//         if (Input.GetKeyDown(KeyCode.Escape))
//         {
//             ClearSelection();
//         }
//     }

//     private void SelectObject(GameObject obj)
//     {
//         selectedObjects.Add(obj);
//         HighlightSelection(obj);
//         Debug.Log("Selected: " + obj.name + ", Total selected: " + selectedObjects.Count);
//     }

//     private void DeselectObject(GameObject obj)
//     {
//         RestoreOriginalMaterial(obj);
//         selectedObjects.Remove(obj);
//         Debug.Log("Deselected: " + obj.name + ", Total selected: " + selectedObjects.Count);
//     }

//     private void HighlightSelection(GameObject obj)
//     {
//         Renderer renderer = obj.GetComponent<Renderer>();
//         if (renderer != null)
//         {
//             // เก็บ material เดิมก่อนเปลี่ยน
//             if (!originalMaterials.ContainsKey(obj))
//             {
//                 originalMaterials.Add(obj, renderer.material);
//             }
//             renderer.material = highlightMaterial;
//         }
//     }

//     private void RestoreOriginalMaterial(GameObject obj)
//     {
//         Renderer renderer = obj.GetComponent<Renderer>();
//         if (renderer != null && originalMaterials.ContainsKey(obj))
//         {
//             renderer.material = originalMaterials[obj];
//             originalMaterials.Remove(obj);
//         }
//     }

//     private void ClearSelection()
//     {
//         // สร้างรายการชั่วคราวเพื่อหลีกเลี่ยงข้อผิดพลาดในการลูป
//         GameObject[] tempArray = new GameObject[selectedObjects.Count];
//         selectedObjects.CopyTo(tempArray);

//         foreach (GameObject obj in tempArray)
//         {
//             DeselectObject(obj);
//         }

//         selectedObjects.Clear();
//         Debug.Log("Selection cleared");
//     }

//     private void SaveToInventory()
//     {
//         if (inventorySystem != null)
//         {
//             foreach (GameObject obj in selectedObjects)
//             {
//                 // เพิ่มวัตถุลงใน inventory
//                 inventorySystem.AddItem(obj);

//                 // อาจจะซ่อนหรือทำลายวัตถุในโลกเกมหลังจากเก็บแล้ว
//                 obj.SetActive(false);
//             }

//             Debug.Log("Saved " + selectedObjects.Count + " items to inventory");
//             ClearSelection();
//         }
//         else
//         {
//             Debug.LogError("Inventory system reference not set!");
//         }
//     }

//     // เมธอดสำหรับการเข้าถึงรายการวัตถุที่เลือกจากที่อื่น
//     public GameObject[] GetSelectedObjects()
//     {
//         return selectedObjects.ToArray();
//     }
// }