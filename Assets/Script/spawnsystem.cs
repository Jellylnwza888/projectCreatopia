using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class SpwanSystem : MonoBehaviour
{

    public List<GameObject> OriginalObj;
    public List<GameObject> AssemblyObj;
    private bool isUIVisible = false;
    public bool toolkit;
    public bool isseat;
    public bool isdriving;

    public Camera cam;
    public maincontroller maincontroller;

    public GameObject ToolUI;


    public List<GameObject> SelectedObjects = new List<GameObject>();   //เลือกหลายวัตถุ

    public GameObject Playerposition;
    public GameObject DriveText;
    public LayerMask PlayerLayer;

    public GameObject prefabToSpawn;
    public Transform spawnPoint;
    public LayerMask dectectionLayerfordrag;


    public void NewAssemblyObj()
    {
        GameObject obj = new GameObject("AssemblyObj " + AssemblyObj.Count); //parent
        obj.AddComponent<ObjectDetector>();
        obj.GetComponent<ObjectDetector>().cartext = DriveText;
        obj.GetComponent<ObjectDetector>().detectionLayer = PlayerLayer;
        obj.GetComponent<ObjectDetector>().spwanSystem = this.gameObject.GetComponent<SpwanSystem>();
        obj.transform.position = new Vector3(Playerposition.transform.position.x, 0, Playerposition.transform.position.z + 5);  // วางตำแหน่งที่ศูนย์กลางของ AssemblyObj *จุดที่เกิดobj*
        AssemblyObj.Add(obj);
        obj.AddComponent<MainControllerSelectObject>();
        obj.AddComponent<MultiDragAndDrop>();
        obj.GetComponent<MultiDragAndDrop>().cam = cam;
        obj.GetComponent<MultiDragAndDrop>().mainControllerSelectObject = obj.GetComponent<MainControllerSelectObject>();
        obj.tag = "controlcamera";
    }
    public void CreateCube(int index) //child
    {

        if (maincontroller.isCraft)
        {

            GameObject obj = Instantiate(OriginalObj[index]);
            GameObject AssemblyObjs = AssemblyObj[AssemblyObj.Count - 1];
            obj.transform.parent = AssemblyObjs.transform;
            obj.transform.localPosition = Vector3.zero; // วางตำแหน่งที่ศูนย์กลางของ AssemblyObj
            //obj.transform.localPosition = new Vector3(Playerposition.transform.localPosition.x, Playerposition.transform.localPosition.y, Playerposition.transform.localPosition.z); 
            // obj.transform.gameObject.AddComponent<mortor>();

            // AssemblyObj[AssemblyObj.Count - 1].transform.position = new Vector3(Playerposition.transform.localPosition.x, 0, Playerposition.transform.localPosition.z + 5); ; // วางตำแหน่งที่ศูนย์กลางของ AssemblyObj

            obj.AddComponent<Attact>(); //Add fixjoint
            obj.AddComponent<DragAndDrop3D>();
            obj.GetComponent<DragAndDrop3D>().maincontroller = maincontroller;
            obj.GetComponent<DragAndDrop3D>().cam = cam;
            obj.tag = "Draggable";
            obj.layer = dectectionLayerfordrag; // ตั้งค่า layer detectionLayerfordrag
            obj.AddComponent<ClickableObject>();
            obj.GetComponent<ClickableObject>().ToolUI = ToolUI;
            obj.GetComponent<ClickableObject>().spwanSystem = this.gameObject.GetComponent<SpwanSystem>();
            obj.GetComponent<ClickableObject>().cam = cam;
            //DragAndDrop3D
            obj.AddComponent<SelectObjectToDrag>();
            obj.GetComponent<SelectObjectToDrag>().cam = cam;
            obj.GetComponent<SelectObjectToDrag>().mainControllerSelectObject = AssemblyObjs.GetComponent<MainControllerSelectObject>();
            obj.AddComponent<HelpDragAndDrop>();
            obj.GetComponent<HelpDragAndDrop>().cam = cam;
            obj.GetComponent<HelpDragAndDrop>().mainControllerSelectObject = AssemblyObjs.GetComponent<MainControllerSelectObject>();
            obj.GetComponent<HelpDragAndDrop>().detectionLayer = dectectionLayerfordrag;
            obj.AddComponent<DrawLine>();

            //obj.AddComponent<SnapSocket>();
            //obj.AddComponent<Slot>();
            // obj.AddComponent<Car>();
            // obj.AddComponent<Wheel>();




            //obj.AddComponent<ObjectSelector>();
            if (obj.GetComponent<ItemInfoController>().item_Info.Item_Type == 2)
            {
                obj.AddComponent<SeatSystem>();
            }
            if (obj.GetComponent<ItemInfoController>().item_Info.Item_Type == 3)
            {
                AssemblyObjs.AddComponent<motorController>(); //------------------//
                AssemblyObjs.AddComponent<WheelDrives>();
            }

            // if(obj.GetComponent<ItemInfoController>().item_Info.Item_Type == 4)//---------เช็คไอเทมล้อ------------------//
            // {
            //     CarController carController = AssemblyObjs.GetComponent<CarController>();
            //     if (carController != null)
            //     {
            //         WheelCollider wheelCollider = obj.AddComponent<WheelCollider>();

            //         JointSpring spring = wheelCollider.suspensionSpring;
            //         spring.spring = 35000f;
            //         spring.damper = 4500f;
            //         spring.targetPosition = 0.5f;
            //         wheelCollider.suspensionSpring = spring;
            //         wheelCollider.suspensionDistance = 0.3f;

            //         AssignWheelToCarController(obj, wheelCollider, carController);
            //     }
            // }

        }

    }


    public GameObject ClickOBJ;//

    public void Rotate()
    {

        if (ClickOBJ != null)
        {
            Debug.LogWarning("ClickOBJ is not null, rotating object.");
        }

        // float posZ = ClickOBJ.transform.eulerAngles.z;
        // posZ += 20;//รัศมีของการหมุน
        // ClickOBJ.transform.eulerAngles = new Vector3(0, 0, posZ);//หมุน


        // float posZ = ClickOBJ.transform.eulerAngles.z;
        // posZ += 20;//รัศมีของการหมุน
        // ClickOBJ.transform.eulerAngles = new Vector3(0, 0, posZ);//หมุน
        // // ClickOBJ.transform.Translate(10, 0 ,0);//ใช้ในการเคลื่อนที่
    }
    public void MovesUp()
    {

        // float posZ = ClickOBJ.transform.eulerAngles.z;
        // posZ+=20;
        // ClickOBJ.transform.eulerAngles = new Vector3(0,  0,posZ );//หมุน

        ClickOBJ.transform.Rotate( -45, 0,  0);//ใช้ในการเคลื่อนที่
    }
    public void MovesDown()
    {
        ClickOBJ.transform.Rotate( 45, 0 ,0);//ใช้ในการเคลื่อนที่
    }
    public void MovesLeft()
    {
        ClickOBJ.transform.Rotate( 0, -45, 0);//ใช้ในการเคลื่อนที่
    }
    public void MovesRight()
    {
        ClickOBJ.transform.Rotate( 0, 45, 0);//ใช้ในการเคลื่อนที่
    }
    public void DeleateObject()
    {
        if (ClickOBJ != null)
        {
            Destroy(ClickOBJ); // ลบวัตถุเมื่อคลิกซ้าย
        }
    }

    public void DuplicateObject()
    {
        if (ClickOBJ != null)
        {
            GameObject duplicate = Instantiate(ClickOBJ);
            duplicate.transform.parent = AssemblyObj[AssemblyObj.Count - 1].transform;
            duplicate.transform.position = ClickOBJ.transform.position + new Vector3(5, 0, 0); // วางตำแหน่งข้างๆ
            duplicate.transform.rotation = ClickOBJ.transform.rotation;
        }
    }


    //เชื่อมกับระบบtoolkit 
    public void ToggleUI()
    {
        // สลับสถานะการแสดงผลของ UI
        isUIVisible = !isUIVisible;

        // ถ้าคลิกและ UI ไม่แสดงอยู่ ก็ให้แสดง UI
        if (isUIVisible)
        {
            ToolUI.SetActive(true);
        }
        else
        {
            ToolUI.SetActive(false);
        }
    }


    
    
    


    


}