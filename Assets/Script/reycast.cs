using UnityEngine;

public class reycas : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.collider.name);
        }
        
    }
}
