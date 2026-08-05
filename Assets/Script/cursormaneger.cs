using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Texture2D customCursor; // ใส่ Texture ของ Cursor ที่ต้องการ
    public Vector2 hotSpot = Vector2.zero; // จุดศูนย์กลางของ Cursor
    public bool lockCursor = false; // ล็อค Cursor หรือไม่

    void Start()
    {
        SetCursor(customCursor);
    }

    void Update()
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked; // ล็อค Cursor ให้อยู่กลางจอ
            Cursor.visible = false; // ซ่อน Cursor
        }
        else
        {
            Cursor.lockState = CursorLockMode.None; // ปลดล็อค Cursor
            Cursor.visible = true; // แสดง Cursor
        }
    }

    public void SetCursor(Texture2D cursorTexture)
    {
        Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.Auto);
    }
}
