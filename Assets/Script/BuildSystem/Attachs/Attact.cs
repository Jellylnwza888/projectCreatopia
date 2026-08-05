using UnityEngine;

public class Attact : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<ItemInfoController>() != null)
        {
            if (other.gameObject.GetComponent<ItemInfoController>().item_Info.ItemBlockInfo == 1) //block
            {
                Debug.LogWarning("yes");
                if (GetComponent<FixedJoint>() == null)
                {
                    FixedJoint fx = this.gameObject.AddComponent<FixedJoint>();
                    fx.connectedBody = other.transform.GetComponent<Rigidbody>();
                }
            }
            if (other.gameObject.GetComponent<ItemInfoController>().item_Info.ItemBlockInfo == 2) //wheel
            {
                Debug.LogWarning("yes");
                if (GetComponent<HingeJoint >() == null)
                {
                    HingeJoint hinge = this.gameObject.AddComponent<HingeJoint>();
                    hinge.connectedBody = other.transform.GetComponent<Rigidbody>();
                }
            }

        }

    }
}
