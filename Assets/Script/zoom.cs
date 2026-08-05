using UnityEngine;

public class zoom : MonoBehaviour
{
    [Header("Zoom Settings")]
    public float zoomSpeed = 10f;
    public float minZoom = 10f;
    public float maxZoom = 60f;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = GetComponent<Camera>();
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    void Update()
    {
        float scrollInput = Input.mouseScrollDelta.y;
        
        if (scrollInput != 0)
        {
            float newFOV = mainCamera.fieldOfView - (scrollInput * zoomSpeed);
            mainCamera.fieldOfView = Mathf.Clamp(newFOV, minZoom, maxZoom);
        }
    }
}

