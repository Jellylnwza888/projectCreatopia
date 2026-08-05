using System.Collections;
using Unity.Collections;
using UnityEngine;


public class DrawLine : MonoBehaviour
{
    [HideInInspector]public Transform pointA;
    [HideInInspector] public Transform pointB;
    private LineRenderer line;

    void SetUpLine()
    {
        pointA = this.transform;
        // Create LineRenderer and attach to this GameObject
        line = gameObject.AddComponent<LineRenderer>();

        // Set the number of line points
        line.positionCount = 2;

        // Optional: set width
        line.startWidth = 0.1f;
        line.endWidth = 0.1f;

        // Optional: set material (requires a material that supports lines like Unlit/Color)
        line.material = new Material(Shader.Find("Unlit/Color"));
        line.material.color = Color.red;
    }
    void Start()
    {
        try
        {

            SetUpLine();

        }
        catch
        {

        }
    }

    void Update()
    {
        try
        {

            if (pointA != null && pointB != null)
            {
                if (line == null)
                {
                    SetUpLine();

                }
                // Update line if points move
                line.SetPosition(0, pointA.position);
                line.SetPosition(1, pointB.position);

                Debug.Log("EE");
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    pointB.transform.gameObject.transform.position = pointA.gameObject.transform.position;
                    Destroy(line);
                    Debug.LogWarning("GOoooooo!!");
                    pointB.transform.gameObject.GetComponent<HelpDragAndDrop>().shortestDistance = Mathf.Infinity;
                    pointA.transform.gameObject.GetComponent<HelpDragAndDrop>().shortestDistance = Mathf.Infinity;
                    pointA.transform.gameObject.GetComponent<HelpDragAndDrop>().closestCollider = null;

                }
            }

        }
        catch
        {

        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (line)
            {
                Destroy(line);
            }
        }
    }



}
