using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [Header("Camera Settings")]
    public Camera targetCamera;
    public float moveSpeed = 2f;
    public float distanceFromCenter = 10f; // ระยะห่างจากจุดศูนย์กลาง
    public GameObject centerObject; // GameObject เปล่าๆ ที่จะใช้เป็นจุดศูนย์กลาง
    
    [Header("UI Buttons")]
    public Button xPlusButton;   // ขวา
    public Button xMinusButton;  // ซ้าย
    public Button yPlusButton;   // บน
    public Button yMinusButton;  // ล่าง
    public Button zPlusButton;   // หน้า
    public Button zMinusButton;  // หลัง
    
    [Header("Position Display")]
    public Text positionText;
    
    private Vector3 centerPosition;
    
    // ตำแหน่งคงที่สำหรับแต่ละทิศทาง (ห่างจากจุดศูนย์กลาง 10 หน่วย)
    private Vector3[] fixedPositions = new Vector3[6];
    
    void Start()
    {
        // ถ้าไม่ได้กำหนดกล้อง ให้ใช้กล้องหลัก
        if (targetCamera == null)
            targetCamera = Camera.main;
        
        // กำหนดจุดศูนย์กลางจาก GameObject
        UpdateCenterPosition();
        
        // คำนวณตำแหน่งคงที่สำหรับแต่ละทิศทาง
        CalculateFixedPositions();
        
        // เชื่อมต่อปุ่มกับฟังก์ชัน
        SetupButtons();
        
        // อัพเดทการแสดงผลตำแหน่ง
        UpdatePositionDisplay();
    }
    
    void UpdateCenterPosition()
    {
        // ถ้าไม่ได้กำหนด GameObject ให้ใช้ตำแหน่ง 0,0,0
        if (centerObject != null)
        {
            centerPosition = centerObject.transform.position;
        }
        else
        {
            centerPosition = Vector3.zero;
        }
    }
    
    void CalculateFixedPositions()
    {
        // อัพเดทตำแหน่งศูนย์กลางก่อน
        UpdateCenterPosition();
        
        // X+ (ขวา) - กล้องอยู่ทางขวาของจุดศูนย์กลาง
        fixedPositions[0] = centerPosition + Vector3.right * distanceFromCenter;
        
        // X- (ซ้าย) - กล้องอยู่ทางซ้ายของจุดศูนย์กลาง
        fixedPositions[1] = centerPosition + Vector3.left * distanceFromCenter;
        
        // Y+ (บน) - กล้องอยู่ด้านบนของจุดศูนย์กลาง
        fixedPositions[2] = centerPosition + Vector3.up * distanceFromCenter;
        
        // Y- (ล่าง) - กล้องอยู่ด้านล่างของจุดศูนย์กลาง
        fixedPositions[3] = centerPosition + Vector3.down * distanceFromCenter;
        
        // Z+ (หน้า) - กล้องอยู่ด้านหน้าของจุดศูนย์กลาง
        fixedPositions[4] = centerPosition + Vector3.forward * distanceFromCenter;
        
        // Z- (หลัง) - กล้องอยู่ด้านหลังของจุดศูนย์กลาง
        fixedPositions[5] = centerPosition + Vector3.back * distanceFromCenter;
    }
    
    void SetupButtons()
    {
        if (xPlusButton != null)
            xPlusButton.onClick.AddListener(() => MoveCameraToPosition(0)); // X+
            
        if (xMinusButton != null)
            xMinusButton.onClick.AddListener(() => MoveCameraToPosition(1)); // X-
            
        if (yPlusButton != null)
            yPlusButton.onClick.AddListener(() => MoveCameraToPosition(2)); // Y+
            
        if (yMinusButton != null)
            yMinusButton.onClick.AddListener(() => MoveCameraToPosition(3)); // Y-
            
        if (zPlusButton != null)
            zPlusButton.onClick.AddListener(() => MoveCameraToPosition(4)); // Z+
            
        if (zMinusButton != null)
            zMinusButton.onClick.AddListener(() => MoveCameraToPosition(5)); // Z-
    }
    
    public void MoveCameraToPosition(int positionIndex)
    {
        if (positionIndex >= 0 && positionIndex < fixedPositions.Length)
        {
            Vector3 targetPosition = fixedPositions[positionIndex];
            StartCoroutine(SmoothMoveToPosition(targetPosition));
        }
    }
    
    System.Collections.IEnumerator SmoothMoveToPosition(Vector3 targetPos)
    {
        Vector3 startPos = targetCamera.transform.position;
        float elapsedTime = 0;
        float moveTime = 1f / moveSpeed;
        
        while (elapsedTime < moveTime)
        {
            // เคลื่อนที่กล้อง
            targetCamera.transform.position = Vector3.Lerp(startPos, targetPos, elapsedTime / moveTime);
            
            // หมุนกล้องให้มองไปที่จุดศูนย์กลาง
            targetCamera.transform.LookAt(centerPosition);
            
            elapsedTime += Time.deltaTime;
            UpdatePositionDisplay();
            yield return null;
        }
        
        // ตั้งตำแหน่งสุดท้ายและหมุนมองไปที่จุดศูนย์กลาง
        targetCamera.transform.position = targetPos;
        targetCamera.transform.LookAt(centerPosition);
        UpdatePositionDisplay();
    }
    
    void UpdatePositionDisplay()
    {
        if (positionText != null)
        {
            Vector3 pos = targetCamera.transform.position;
            Vector3 rot = targetCamera.transform.eulerAngles;
            positionText.text = $"Camera Position:\nX: {pos.x:F1}\nY: {pos.y:F1}\nZ: {pos.z:F1}\n\nRotation:\nX: {rot.x:F1}\nY: {rot.y:F1}\nZ: {rot.z:F1}";
        }
    }
    
    // ฟังก์ชันสำหรับเรียกจากปุ่ม UI โดยตรง
    public void MoveToXPlus() { MoveCameraToPosition(0); }   // ขวา
    public void MoveToXMinus() { MoveCameraToPosition(1); }  // ซ้าย
    public void MoveToYPlus() { MoveCameraToPosition(2); }   // บน
    public void MoveToYMinus() { MoveCameraToPosition(3); }  // ล่าง
    public void MoveToZPlus() { MoveCameraToPosition(4); }   // หน้า
    public void MoveToZMinus() { MoveCameraToPosition(5); }  // หลัง
    
    // ฟังก์ชันสำหรับเปลี่ยน GameObject ศูนย์กลางขณะเล่น
    public void SetNewCenterObject(GameObject newCenterObject)
    {
        centerObject = newCenterObject;
        CalculateFixedPositions(); // คำนวณตำแหน่งใหม่
    }
    
    // วาดเส้นแสดงตำแหน่งต่างๆ ใน Scene View
    void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            // อัพเดทตำแหน่งศูนย์กลางจาก GameObject
            UpdateCenterPosition();
            
            // วาดจุดศูนย์กลาง
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(centerPosition, 0.5f);
            
            // วาดตำแหน่งคงที่แต่ละจุด
            Color[] colors = { Color.red, Color.red, Color.green, Color.green, Color.blue, Color.blue };
            string[] labels = { "X+", "X-", "Y+", "Y-", "Z+", "Z-" };
            
            for (int i = 0; i < fixedPositions.Length; i++)
            {
                Gizmos.color = colors[i];
                Gizmos.DrawWireSphere(fixedPositions[i], 0.3f);
                Gizmos.DrawLine(centerPosition, fixedPositions[i]);
            }
        }
    }
}