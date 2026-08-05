using System;
using System.Collections;
using System.Collections.Generic;


using UnityEngine;

public class maincontroller : MonoBehaviour
{
    [SerializeField] private SpwanSystem spwanSystem;

    // public Camera cam;
    public Rigidbody rb; // ตัวแปรสำหรับเก็บ Rigidbody ของ AssemblyObj
    public bool isCraft = false;
    public bool isPlay = true;
    public GameObject AssemblyObj;
    public GameObject Maincamera;
    public GameObject Craftcamera;
    public GameObject player; // ตัวแปรสำหรับเก็บผู้เล่น

    public GameObject inventoryItem; // Prefab ของ InventoryItem ที่จะสร้างขึ้น
    public GameObject inventoryUI;
    public GameObject CharacterMovement;// ตัวแปรสำหรับเก็บการเคลื่อนที่ของผู้เล่น 

    public GameObject prefabToSpawn;



    [Obsolete]
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf); // สลับการแสดงผล
        }

        // กด C เพื่อสลับโหมด Craft/Play
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (isCraft)
            {
                Plays();
            }
            else
            {
                Craft();
            }
            // กด ESC เพื่อเข้าสู่โหมด Play
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Plays();
            }
        }
    }

    public void Craft()
    {


        CharacterMovement.SetActive(false); // ปิดการเคลื่อนที่ของผู้เล่น

        inventoryItem.SetActive(true); // เปิดใช้งาน InventoryItem

        // InventoryItem inventoryItem = AssemblyObj.GetComponent<InventoryItem>();

        // if (inventoryItem != null)
        // {
        //     inventoryItem.isCraft = true; // ตั้งค่า isCraft เป็น true
        // }

        isCraft = true;
        isPlay = false;


        foreach (var obj in spwanSystem.AssemblyObj)
        {
            Destroy(obj.GetComponent<Rigidbody>());

        }
        // ลบ Rigidbody ออกเพื่อให้วัตถุหยุดเคลื่อนที่
        Destroy(AssemblyObj.GetComponent<Rigidbody>());
        spwanSystem.NewAssemblyObj();
        // ตั้งค่าตำแหน่งและการหมุนใหม่
        AssemblyObj.transform.position = Vector3.zero;
        AssemblyObj.transform.rotation = Quaternion.Euler(0, 56, -125);

        Maincamera.SetActive(false);
        Craftcamera.SetActive(true);
        // spwanSystem.AssemblyObj[spwanSystem.AssemblyObj.Count - 1].transform.position = new Vector3(playerObj.transform.localPosition.x, 0, playerObj.transform.localPosition.z + 5); ; // วางตำแหน่งที่ศูนย์กลางของ AssemblyObj
        Transform AssemblyObjPos;
        AssemblyObjPos = spwanSystem.AssemblyObj[spwanSystem.AssemblyObj.Count - 1].transform;
        Craftcamera.transform.position = new Vector3(AssemblyObjPos.position.x, AssemblyObjPos.position.y + 10, AssemblyObjPos.position.z);
        Craftcamera.transform.rotation = Quaternion.Euler(95f, 0f, 0f);


        if (player != null)
        {
            // ตั้งค่าตำแหน่งให้อยู่ตรงหน้าผู้เล่น
            Vector3 playerPosition = player.transform.position;
            Vector3 playerForward = player.transform.forward;
            float distanceFromPlayer = 2.0f; // ระยะห่างจากผู้เล่น

            // คำนวณตำแหน่งที่อยู่ด้านหน้าผู้เล่น
            Vector3 newPosition = playerPosition + (playerForward * distanceFromPlayer);

            // กำหนดตำแหน่งและการหมุนให้ AssemblyObj
            AssemblyObj.transform.position = newPosition;
            AssemblyObj.transform.rotation = Quaternion.Euler(0, 56, -125);
        }
        else
        {
            // กรณีไม่พบ player ให้ใช้ตำแหน่งเริ่มต้น
            AssemblyObj.transform.position = Vector3.zero;
            AssemblyObj.transform.rotation = Quaternion.Euler(0, 56, -125);
        }
        

         
    }


    public void Plays()
    {




        Vector3 spawnPosition = new Vector3(0, 0, 0); // หรือกำหนดตำแหน่งที่หนูต้องการ (จุดเกิดของวัตถุ)
        Quaternion spawnRotation = Quaternion.identity; // ไม่มีการหมุน

        GameObject spawnedObj = Instantiate(prefabToSpawn, spawnPosition, spawnRotation);

        Rigidbody rb = spawnedObj.GetComponent<Rigidbody>();
        if (rb == null)
        {
            //rb = spawnedObj.AddComponent<Rigidbody>();   
            //rb.isKinematic = true; 
        }



        for (int i = 0; i < spwanSystem.AssemblyObj[spwanSystem.AssemblyObj.Count - 1].transform.childCount; i++)
        {
            if (spwanSystem.AssemblyObj[spwanSystem.AssemblyObj.Count - 1].transform.GetChild(i).GetComponent<BoxCollider>() != null)
            {
                spwanSystem.AssemblyObj[spwanSystem.AssemblyObj.Count - 1].transform.GetChild(i).GetComponent<BoxCollider>().isTrigger = false;

            }
            if (spwanSystem.AssemblyObj[spwanSystem.AssemblyObj.Count - 1].transform.GetChild(i).GetComponent<Rigidbody>() != null)
            {
                spwanSystem.AssemblyObj[spwanSystem.AssemblyObj.Count - 1].transform.GetChild(i).GetComponent<Rigidbody>().isKinematic = false;

            }

        }

        CharacterMovement.SetActive(true);

        inventoryItem.SetActive(false);

        if (spwanSystem.AssemblyObj[spwanSystem.AssemblyObj.Count - 1].GetComponent<motorController>() != null)
        {
            spwanSystem.AssemblyObj[spwanSystem.AssemblyObj.Count - 1].GetComponent<motorController>().InitObject();
        }
        isPlay = true;
        isCraft = false;



        // foreach (var obj in spwanSystem.AssemblyObj)
        // {
        //     obj.AddComponent<Rigidbody>(); // เพิ่ม Rigidbody ให้กับวัตถุใน AssemblyObj
        // }

        // ตรวจสอบก่อนว่าไม่มี Rigidbody อยู่แล้ว
        // if (AssemblyObj.GetComponent<Rigidbody>() == null)
        // {
        //     AssemblyObj.AddComponent<Rigidbody>();
        // }

        Maincamera.SetActive(true);
        Craftcamera.SetActive(false);


        Cursor.visible = false; // ถูกต้อง - ซ่อนเคอร์เซอร์


    }


}