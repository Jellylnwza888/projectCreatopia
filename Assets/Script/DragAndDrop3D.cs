using UnityEngine;

public class DragAndDrop3D : MonoBehaviour
{
    public Camera cam;
    private GameObject selectedObject;
    private Vector3 offset;
    private float zCoord;
    public maincontroller maincontroller;
    private Vector3 originalPosition;
    public float maxDragRadius = 5f;

    // ตัวแปรใหม่สำหรับการเคลื่อนไหวที่นุ่มนวลขึ้น
    public float smoothSpeed = 10f;
    public float returnSpeed = 5f;
    public bool returnToOriginal = true;

    // ส่วนแสดงผลเมื่อเลือกวัตถุ
    private Material originalMaterial;
    public Color highlightColor = new Color(1f, 0.8f, 0.2f, 0.7f);
    private bool isDragging = false;
    private bool wasOutOfBounds = false;

    // การแสดงขอบเขตการลาก (ตัวเลือก)
    public bool showDragBoundary = true;
    private LineRenderer boundaryIndicator;

    void Start()
    {
        if (cam == null)
            cam = Camera.main;

        // if (showDragBoundary)
        // {
        //     CreateBoundaryIndicator();
        // }
    }

    // void CreateBoundaryIndicator() //------------------โชว์ระยะที่สมารถลากได้-------------------
    // {
    //     GameObject boundary = new GameObject("DragBoundary");
    //     boundaryIndicator = boundary.AddComponent<LineRenderer>();
    //     boundaryIndicator.positionCount = 33; // จุดมากขึ้น = วงกลมเรียบขึ้น
    //     boundaryIndicator.startWidth = 0.05f;
    //     boundaryIndicator.endWidth = 0.05f;
    //     boundaryIndicator.material = new Material(Shader.Find("Sprites/Default"));
    //     boundaryIndicator.startColor = new Color(0.5f, 0.5f, 1f, 0.3f);
    //     boundaryIndicator.endColor = new Color(0.5f, 0.5f, 1f, 0.3f);
    //     boundaryIndicator.enabled = false;
    // }

    void Update()
    {
        if (maincontroller.isCraft)
        {
            HandleInput();

            if (isDragging && selectedObject != null)
            {
                MoveSelectedObject();
            }
        }
    }

    void HandleInput()
    {
        // เมื่อกดเมาส์ - เลือกวัตถุ
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Draggable"))
                {
                    SelectObject(hit.collider.gameObject);
                }
            }
        }

        // เมื่อปล่อยเมาส์ - ปล่อยวัตถุ
        if (Input.GetMouseButtonUp(0) && selectedObject != null)
        {
            ReleaseObject();
        }
    }

    void SelectObject(GameObject obj)
    {
        selectedObject = obj;
        originalPosition = selectedObject.transform.position;
        zCoord = cam.WorldToScreenPoint(selectedObject.transform.position).z;
        offset = selectedObject.transform.position - GetMouseWorldPos();
        isDragging = true;

        // // ---------------------เปลี่ยนสีวัตถุเมื่อเลือก-----------------
        // Renderer renderer = selectedObject.GetComponent<Renderer>();
        // if (renderer != null)
        // {
        //     originalMaterial = renderer.material;
        //     Material highlightMaterial = new Material(originalMaterial);
        //     highlightMaterial.color = highlightColor;
        //     renderer.material = highlightMaterial;
        // }

        // แสดงวงขอบเขต
        if (showDragBoundary && boundaryIndicator != null)
        {
            ShowBoundaryCircle(originalPosition);
        }
    }

    void ShowBoundaryCircle(Vector3 center)
    {
        if (boundaryIndicator == null) return;

        boundaryIndicator.enabled = true;

        // สร้างจุดเป็นวงกลม
        for (int i = 0; i < boundaryIndicator.positionCount; i++)
        {
            float angle = i * 360f / (boundaryIndicator.positionCount - 1) * Mathf.Deg2Rad;
            float x = Mathf.Sin(angle) * maxDragRadius;
            float z = Mathf.Cos(angle) * maxDragRadius;
            boundaryIndicator.SetPosition(i, new Vector3(center.x + x, center.y, center.z + z));
        }
    }

    void ReleaseObject() //-----------------ใช้กับGizmoได้-------------------
    {
        Collider[] hitColliders = Physics.OverlapSphere(selectedObject.transform.position, 0.5f);
        bool snapped = false;

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("SnapSocket"))
            {
                // Snap to the socket position
                selectedObject.transform.position = hitCollider.transform.position;
                selectedObject.transform.rotation = hitCollider.transform.rotation;
                snapped = true;
                break;
            }
        } 
             if (snapped)
        {
            Debug.Log("Object snapped!");
        }


        // คืนสภาพวัสดุกลับเหมือนเดิม
        Renderer renderer = selectedObject.GetComponent<Renderer>();
        if (renderer != null && originalMaterial != null)
        {
            renderer.material = originalMaterial;
        }

        // // กลับไปตำแหน่งเดิมถ้าลากเกินขอบเขตและเปิดใช้งาน returnToOriginal
        // if (wasOutOfBounds && returnToOriginal)
        // {
        //     StartCoroutine(ReturnToOriginalPosition());
        // }
        else
        {
            isDragging = false;
            selectedObject = null;
        }

        // ซ่อนเส้นขอบเขต
        if (boundaryIndicator != null)
        {
            boundaryIndicator.enabled = false;
        }
    }

    System.Collections.IEnumerator ReturnToOriginalPosition()
    {
        Vector3 startPos = selectedObject.transform.position;
        float journeyLength = Vector3.Distance(startPos, originalPosition);
        float startTime = Time.time;

        while (selectedObject != null && Vector3.Distance(selectedObject.transform.position, originalPosition) > 0.01f)
        {
            float distCovered = (Time.time - startTime) * returnSpeed;
            float fractionOfJourney = distCovered / journeyLength;

            selectedObject.transform.position = Vector3.Lerp(startPos, originalPosition, fractionOfJourney);
            yield return null;
        }

        if (selectedObject != null)
        {
            selectedObject.transform.position = originalPosition;
        }

        isDragging = false;
        selectedObject = null;
    }

    void MoveSelectedObject()
    {
        Vector3 targetPosition = GetMouseWorldPos() + offset;
        Vector3 direction = targetPosition - originalPosition;

        // ตรวจสอบว่าอยู่ในขอบเขตหรือไม่
        wasOutOfBounds = direction.magnitude > maxDragRadius;

        if (wasOutOfBounds)
        {
            direction = direction.normalized * maxDragRadius;
            targetPosition = originalPosition + direction;
        }

        // การเคลื่อนที่แบบนุ่มนวลโดยใช้การประมาณค่า
        selectedObject.transform.position = Vector3.Lerp(
            selectedObject.transform.position,
            targetPosition,
            smoothSpeed * Time.deltaTime
        );
    }

    Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zCoord; // รักษาความลึกให้คงที่
        return cam.ScreenToWorldPoint(mousePoint);
    }
}