using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Thirdpersoncam : MonoBehaviour
{

    [Header("References")]
    public Transform player;
    public Transform orientation;
    public Transform playerobj;
    public Transform rb;

    public float rotationSpeed;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //lock cursor to the center of the screen
        Cursor.visible = false; //hide the cursor
    }
    private void Update()
    {
        //rotate orientation
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized; //rotate orientation

        //rotate player object
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput; //calculate input direction
        
        if (inputDir != Vector3.zero) //if there is any input
        {
            playerobj.forward = Vector3.Slerp(playerobj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed); //rotate player object
        }
    }


}
