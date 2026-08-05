using UnityEngine;

public class ShowPosition : MonoBehaviour
{
    void Update()
    {
        // แสดงตำแหน่งของวัตถุใน Console ทุกเฟรม
        Vector3 pos = transform.position;
        Debug.Log("Position: " + pos);
    }
}

