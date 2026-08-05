using System.Collections.Generic;
using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    public bool isUIVisible ;
    public GameObject ToolUI;
    public Camera cam;
    public SpwanSystem spwanSystem; //

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    if (Input.GetMouseButtonDown(0)){
                        Debug.Log("Left click");

                        spwanSystem.ClickOBJ = transform.gameObject;
                    }

                    else if (Input.GetMouseButtonDown(1))
                    { 
                        isUIVisible = !isUIVisible; 

                        if (isUIVisible){
                        
                            ToolUI.SetActive(true);
                            Debug.Log("Right click");
                        }
                        else{
                            ToolUI.SetActive(false);
                            Debug.Log("Right click");
                        }
                        // ToolUI.SetActive(true);
                        // Debug.Log("Right click");

                        spwanSystem.ClickOBJ = transform.gameObject;
                    }
                    else if (Input.GetMouseButtonDown(2))
                        Debug.Log("Middle click");
                }
            }
        }

        
    }
}