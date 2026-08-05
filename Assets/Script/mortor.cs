using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mortor : MonoBehaviour //ตัวก้อนแคปซูล
{
    public GameObject Parent;
    public bool isForce;
    public int Speed;
    private void Start() {
        Parent = transform.parent.gameObject; //เมื่อมีแรงกำลังทำกับวัตถุนี้อยู่
    }

    [System.Obsolete]
private void Update() {
    Rigidbody rb = Parent.GetComponent<Rigidbody>();
    
    // W - เดินหน้า (แกน +Z)
    if(Input.GetKey(KeyCode.W) && isForce) {
        rb.AddForce(0, 0, Speed);
    }
    
    // S - ถอยหลัง (แกน -Z)
    if(Input.GetKey(KeyCode.S) && isForce) {
        rb.AddForce(0, 0, -Speed);
    }
    
    // A - ไปทางซ้าย (แกน -X)
    if(Input.GetKey(KeyCode.A) && isForce) {
        rb.AddForce(-Speed, 0, 0);
    }
    
    // D - ไปทางขวา (แกน +X)
    if(Input.GetKey(KeyCode.D) && isForce) {
        rb.AddForce(Speed, 0, 0);
    }
}

// // เพิ่มส่วนนี้ใน script motor ที่มีอยู่แล้ว
// public List<Transform> wheels = new List<Transform>();

// // ในส่วนที่มีการหมุน หรือเคลื่อนที่ ให้เพิ่มโค้ดเพื่อหมุนล้อทั้งหมด
// private void RotateWheels(float speed)
// {
//     foreach (Transform wheel in wheels)
//     {
//         wheel.Rotate(speed * Time.deltaTime, 0, 0);
//     }
// }
}