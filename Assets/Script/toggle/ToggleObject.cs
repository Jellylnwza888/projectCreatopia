// using UnityEngine;
// using UnityEngine.UI;

// public class ToggleObject : MonoBehaviour
// {
//     public Toggle  Bouton; // ← เปลี่ยนชื่อเป็น Button เพื่อให้เข้าใจง่ายขึ้น
//     public GameObject ToolUI;

//     void Start()
//     {
//         toggle.onValueChanged.AddListener(OnToggleChanged); // ← บรรทัดนี้ (บรรทัด 11)
//         ToolUI.SetActive(toggle.isOn);
//     }

//     void OnToggleChanged(bool isOn)
//     {
//         ToolUI.SetActive(isOn);
//     }
// }

//      // targetObject.SetActive(!targetObject.activeSelf);