using System.Collections.Generic;
using UnityEngine;

public class SeatSystem : MonoBehaviour
{
    public bool isSeated;
    private ObjectDetector objectDetector;
    //private motorController motorController;
    private WheelDrives wheelDrives;
    private GameObject parent;


    private void Awake()
    {
        //objectDetector = transform.parent.GetComponent<ObjectDetector>();
        // motorController = transform.parent.GetComponent<motorController>();
        //wheelDrives = transform.parent.GetComponent<WheelDrives>();

        objectDetector = GetComponentInParent<ObjectDetector>();
        //motorController = GetComponentInParent<motorController>();
        //wheelDrives = GetComponentInParent<WheelDrives>();
        parent = transform.parent.gameObject;

        // ปิดระบบขับรถเริ่มต้น
        //wheelDrives.enabled = false;
    }
    public WheelDrives wh;
    private void FixedUpdate()
    {
        if (wh == null)
        {
            SetUpWheelDrive();
        }

    }
    public void SetUpWheelDrive()
    {
        foreach (Transform trans in parent.transform)
        {
            Debug.Log(trans.gameObject.name);
            if (trans.gameObject.GetComponent<WheelDrives>() != null)
            {
                wh = trans.gameObject.GetComponent<WheelDrives>();
            }
        }



    } 
    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {                
            Debug.Log("yes");
            if (objectDetector.InRadius)
            {

                Debug.Log("Enter Vehicle");
                EnterVehicle();
            }


        }

        if (isSeated && Input.GetKeyDown(KeyCode.LeftShift))
        {
            ExitVehicle();
        }

        if (isSeated){
            GetComponent<ClickableObject>().spwanSystem.maincontroller.player.transform.position = transform.position;
        }
    }

    private void EnterVehicle()
    {
        isSeated = true;
        //motorController.isdriving = true;
        if(wh != null){
            wh.enabled = true;
        }        
        
        wh.Drive(); //เข้า

        // ย้ายผู้เล่นมาที่ที่นั่ง
        GetComponent<ClickableObject>().spwanSystem.maincontroller.player.transform.position = transform.position;
    }

    private void ExitVehicle()
    {
        isSeated = false;     
        wh.StopDrive(); //ออก
        //motorController.isdriving = false;
        if(wh != null){
            wh.enabled = false;
        }      


    }
}