using UnityEngine;

public class SnapSocket : MonoBehaviour
{
    public int requiredItemType; // ใส่ใน inspector เช่น "Axle", "Wheel"
    public Transform snapPoint;     // จุดล็อกที่อยากให้ part มาวาง

    private void OnTriggerEnter(Collider other)
    {
        DragAndDrop3D part = other.GetComponent<DragAndDrop3D>();
        Item_info info = other.GetComponent<Item_info>();

        if (part != null && info != null && info.Item_Type == requiredItemType)
        {
            SnapPart(part);
        }
    }

    private void SnapPart(DragAndDrop3D part)
    {
        // ปิดการ drag
        part.enabled = false;

        // ย้ายตำแหน่ง part ไป snap point
        part.transform.position = snapPoint.position;
        part.transform.rotation = snapPoint.rotation;

        // ล็อกเป็นลูกของ slot (optional)
        part.transform.SetParent(this.transform);

        Debug.Log($"{part.name} snapped to {name}");
    }
}
