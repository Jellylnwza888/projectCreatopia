using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiDragAndDrop : MonoBehaviour
{
    public Camera cam;
    private float zCoord;
    public float maxDragRadius = 3f;
    public MainControllerSelectObject mainControllerSelectObject;
    private List<Vector3> originalPositions = new List<Vector3>();
    private Vector3 dragStartOffset;
    private bool isDragging = false;

    void Update()
    {
        try
        {


            if (Input.GetMouseButtonDown(0) && mainControllerSelectObject.selectedObjects.Count > 0)
            {
                dragStartOffset = mainControllerSelectObject.selectedObjects[0].transform.position - GetMouseWorldPos();

                originalPositions.Clear();
                foreach (var obj in mainControllerSelectObject.selectedObjects)
                {
                    originalPositions.Add(obj.transform.position);
                }

                isDragging = true;
            }


            if (Input.GetMouseButton(0) && isDragging)
            {
                Vector3 targetPosition = GetMouseWorldPos() + dragStartOffset;
                Vector3 direction = targetPosition - originalPositions[0];

                if (direction.magnitude > maxDragRadius)
                {
                    direction = direction.normalized * maxDragRadius;
                }

                try
                {
                    for (int i = 0; i < mainControllerSelectObject.selectedObjects.Count; i++)
                    {
                        mainControllerSelectObject.selectedObjects[i].transform.position = originalPositions[i] + direction;
                    }
                }
                catch
                {

                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
            }
        }
        catch
        {

        }
    }

    // Vector3 GetMouseWorldPos()
    // {
    //     Vector3 mousePos = Input.mousePosition;
    //     mousePos.z = Camera.main.WorldToScreenPoint(mainControllerSelectObject.selectedObjects[0].transform.position).z;
    //     return Camera.main.ScreenToWorldPoint(mousePos);
    // }
    Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zCoord; // รักษาความลึกให้คงที่
        //mousePos.z = Camera.main.WorldToScreenPoint(mainControllerSelectObject.selectedObjects[0].transform.position).z; 
        // return Camera.main.ScreenToWorldPoint(mousePos); 
        return cam.ScreenToWorldPoint(mousePoint);
    }



}
