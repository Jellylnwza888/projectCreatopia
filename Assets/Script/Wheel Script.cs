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

// // จากนั้นเรียกใช้ฟังก์ชัน RotateWheels ในส่วน Update หรือตามที่เหมาะสม
// // เช่น ถ้ากดปุ่ม W หรือ S ให้เรียกใช้
// if(Input.GetKey(KeyCode.W) && isForce) {
//     rb.AddForce(0, 0, Speed);
//     RotateWheels(Speed); // หมุนล้อด้วยความเร็ว
// }