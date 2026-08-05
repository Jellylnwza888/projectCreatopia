using UnityEngine;

// สคริปต์สำหรับตัวเชื่อมระหว่างตัวรถกับล้อ
public class WheelConnector : MonoBehaviour
{
    public float suspensionDistance = 0.2f;
    public float suspensionSpring = 20000f;
    public float suspensionDamper = 2000f;
    
    private ConfigurableJoint vehicleJoint; // เชื่อมกับตัวรถ
    private ConfigurableJoint wheelJoint;   // เชื่อมกับล้อ
    
    public void ConnectVehicleAndWheel(Rigidbody vehicleRb, Rigidbody wheelRb, Vector3 attachPoint)
    {
        // ตั้งตำแหน่งของตัวเชื่อม
        transform.position = attachPoint;
        
        // สร้าง joint เชื่อมกับตัวรถ
        vehicleJoint = gameObject.AddComponent<ConfigurableJoint>();
        vehicleJoint.connectedBody = vehicleRb;
        
        // ตั้งค่า joint กับตัวรถให้เคลื่อนที่ขึ้นลงตามแนวแกน Y ได้อย่างเดียว (ระบบกันสะเทือน)
        vehicleJoint.xMotion = ConfigurableJointMotion.Locked;
        vehicleJoint.yMotion = ConfigurableJointMotion.Limited;
        vehicleJoint.zMotion = ConfigurableJointMotion.Locked;
        vehicleJoint.angularXMotion = ConfigurableJointMotion.Locked;
        vehicleJoint.angularYMotion = ConfigurableJointMotion.Locked;
        vehicleJoint.angularZMotion = ConfigurableJointMotion.Locked;
        
        // ตั้งค่าขีดจำกัดการเคลื่อนที่ (ระยะกันสะเทือน)
        SoftJointLimit limit = new SoftJointLimit();
        limit.limit = suspensionDistance;
        vehicleJoint.linearLimit = limit;
        
        // ตั้งค่าสปริง (ความนุ่มนวลของระบบกันสะเทือน)
        JointDrive drive = new JointDrive();
        drive.positionSpring = suspensionSpring;
        drive.positionDamper = suspensionDamper;
        drive.maximumForce = float.MaxValue;
        vehicleJoint.yDrive = drive;
        
        // สร้าง joint เชื่อมกับล้อ
        wheelJoint = gameObject.AddComponent<ConfigurableJoint>();
        wheelJoint.connectedBody = wheelRb;
        
    }
    
    private void ConfigureSteeringWheelJoint()
    {
        // ล้อเลี้ยว: สามารถหมุนรอบแกน Y ได้ (เลี้ยวซ้าย-ขวา)
        wheelJoint.xMotion = ConfigurableJointMotion.Locked;
        wheelJoint.yMotion = ConfigurableJointMotion.Locked;
        wheelJoint.zMotion = ConfigurableJointMotion.Locked;
        wheelJoint.angularXMotion = ConfigurableJointMotion.Locked;
        wheelJoint.angularYMotion = ConfigurableJointMotion.Free;
        wheelJoint.angularZMotion = ConfigurableJointMotion.Locked;
        
        // ล้อต้องสามารถหมุนได้รอบแกน Z ด้วย (การหมุนของล้อเมื่อรถเคลื่อนที่)
        wheelJoint.angularZMotion = ConfigurableJointMotion.Free;
    }
    
    private void ConfigureDriveWheelJoint()
    {
        // ล้อขับเคลื่อน: สามารถหมุนรอบแกน Z ได้อย่างอิสระ (หมุนเพื่อขับเคลื่อน)
        wheelJoint.xMotion = ConfigurableJointMotion.Locked;
        wheelJoint.yMotion = ConfigurableJointMotion.Locked;
        wheelJoint.zMotion = ConfigurableJointMotion.Locked;
        wheelJoint.angularXMotion = ConfigurableJointMotion.Locked;
        wheelJoint.angularYMotion = ConfigurableJointMotion.Locked;
        wheelJoint.angularZMotion = ConfigurableJointMotion.Free;
    }
    
    public void DetachWheel()
    {
        if (wheelJoint != null)
        {
            Destroy(wheelJoint);
        }
    }
    
    public void DetachVehicle()
    {
        if (vehicleJoint != null)
        {
            Destroy(vehicleJoint);
        }
    }
    
    public void DetachAll()
    {
        DetachWheel();
        DetachVehicle();
        Destroy(gameObject);
    }
}