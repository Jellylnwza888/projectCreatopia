using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SelectObjectToDrag : MonoBehaviour
{
    public Camera cam;
    public MainControllerSelectObject mainControllerSelectObject;

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (Input.GetMouseButtonDown(0))
            {

                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log("Hi1");
                    GameObject clickedObject = hit.collider.gameObject;

                    if (clickedObject.CompareTag("Draggable"))
                    {
                        if (!mainControllerSelectObject.selectedObjects.Contains(clickedObject))
                        {
                            mainControllerSelectObject.selectedObjects.Add(clickedObject);
                            SetObjectColor(clickedObject, Color.green); // Change color when selected
                        }
                     
                    }
                }
            }
            if (Input.GetMouseButtonDown(1))
            {

                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log("Hi1");
                    GameObject clickedObject = hit.collider.gameObject;

                    if (clickedObject.CompareTag("Draggable"))
                    {
                        mainControllerSelectObject.selectedObjects.Remove(clickedObject);
                            SetObjectColor(clickedObject, Color.white); // Reset color when deselected
                    }
                }
            }
        }
    }

    void SetObjectColor(GameObject obj, Color color)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = color;
        }
    }
}
