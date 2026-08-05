
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class ObjectDetector : MonoBehaviour
{
    // This script detects objects within a certain radius and triggers an action when the player presses 'E'
    public GameObject cartext;
    public SpwanSystem spwanSystem; // Reference to the SpwanSystem script
    public float radius = 5f;
    public LayerMask detectionLayer; // Player layer 

    [Header("Info Text")]
    public string m_Title;
    [TextArea(minLines: 10, maxLines: 50)]
    public string m_Description;
    public bool InRadius = false;
    private bool playerInRange = false;
    private bool isPlayerDriving = false;

  

    void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, detectionLayer);

        if (hitColliders.Length > 0)
        {
            InRadius = true;
            // cartext.SetActive(true); // Show the text when an object is detected
        }
        else
        {
            InRadius = false;
            // cartext.SetActive(false); // Hide the text when no objects are detected
        }

        foreach (var hitCollider in hitColliders)
        {
            // Debug.Log("Detected: " + hitCollider.gameObject.name);
            // if (Input.GetKeyDown(KeyCode.E))
            // {
            //     spwanSystem.isdriving = true;
            // }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);

    }

    void OnGUI()
    {
        if (InRadius)
        {
            GUIStyle style = new GUIStyle(EditorStyles.boldLabel)
            {
                fontSize = 30,
                alignment = TextAnchor.MiddleCenter,
         
            };
   
            style.normal.textColor = Color.white;
            style.fontStyle = FontStyle.Bold;

            float width = 400f;
            float height = 100f;
            float x = (Screen.width - width) / 2;
            float y = (Screen.height - height) / 2;

            GUI.Label(new Rect(x, y, width, height), "Press E", style);
        }
    }

}
