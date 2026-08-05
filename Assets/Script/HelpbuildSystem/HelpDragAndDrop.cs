using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpDragAndDrop : MonoBehaviour
{

    public Camera cam;
    [HideInInspector] public GameObject selectedObject;
    private Vector3 offset;
    private float zCoord;
    private Vector3 originalPosition;
    public float maxDragRadius = 5f;
 
    [Header("CheckRadius")]

    public float radius = 5f;
    public LayerMask detectionLayer; // Assign layers to filter objects
    [SerializeField] private DrawLine drawLine;

    [HideInInspector]public float shortestDistance = Mathf.Infinity;
    [HideInInspector]public Collider closestCollider = null;
    public MainControllerSelectObject mainControllerSelectObject;
    private void Start()
    {
        drawLine = GetComponent<DrawLine>();

    }
    void Update()
    {
        if (mainControllerSelectObject.selectedObjects.Count == 0)
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                return;
            }


            Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, detectionLayer);
            // Debug.Log("EE");
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.CompareTag("Draggable"))
                    {
                        selectedObject = hit.collider.gameObject;
                        zCoord = cam.WorldToScreenPoint(selectedObject.transform.position).z;
                        offset = selectedObject.transform.position - GetMouseWorldPos();
                        originalPosition = selectedObject.transform.position;
                    }
                }
            }

            if (Input.GetMouseButton(0) && selectedObject != null && selectedObject == this.gameObject)
            {
                Vector3 targetPosition = GetMouseWorldPos() + offset;
                Vector3 direction = targetPosition - originalPosition;

                if (direction.magnitude > maxDragRadius)
                {
                    direction = direction.normalized * maxDragRadius;
                }

                selectedObject.transform.position = originalPosition + direction;


                foreach (var hitCollider in hitColliders)
                {
                    // Debug.Log(hitCollider.name);
                    if (hitCollider.gameObject != this.gameObject && drawLine.pointB == null)
                    {


                        float distance = Vector3.Distance(this.transform.position, hitCollider.transform.position);

                        if (distance < shortestDistance || hitCollider.gameObject.GetComponent<DrawLine>().pointB == null || closestCollider == null)
                        {
                            if (hitCollider.gameObject.GetComponent<DrawLine>().pointB == null)
                            {

                                shortestDistance = distance;
                                Debug.Log(hitCollider.name + "--------" + this.name);
                                Debug.Log("Detected: " + distance);
                                drawLine.pointB = hitCollider.gameObject.transform;


                                closestCollider = hitCollider;
                            }
                        }
                    }

                }



            }

        }
        if (Input.GetMouseButtonUp(0)) // Release the object
        {
            selectedObject = null;
            drawLine.pointB = null;

        }


    }

    Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zCoord; // Keep depth consistent
        return cam.ScreenToWorldPoint(mousePoint);
    }




    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);

    }
}
