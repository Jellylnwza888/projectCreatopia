using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class motorController : MonoBehaviour
{
    public bool isdriving = false;
    public List<GameObject> Childobj = new List<GameObject>();

    public List<mortor> MortorObj = new List<mortor>();
    public void InitObject()
    {

        MortorObj.Clear();
        Childobj.Clear();
        for (int i = 0; i < this.transform.childCount; i++)
        {
            Debug.Log(this.transform.GetChild(i).gameObject);
            Childobj.Add(this.transform.GetChild(i).gameObject);
            if (this.transform.GetChild(i).gameObject.GetComponent<ItemInfoController>().item_Info.Item_Type == 3)
            {
                MortorObj.Add(this.transform.GetChild(i).gameObject.GetComponent<mortor>());
            }
        }
    }
    [SerializeField] private bool isPlay = false;
    public void FixedUpdate()
    {
        isPlay = this.gameObject.GetComponent<ObjectDetector>().spwanSystem.maincontroller.isPlay;

        if (isPlay)
        {


            if (isdriving)
            {
                foreach (mortor obj in MortorObj)
                {
                    if (obj != null)
                    {
                        obj.isForce = true;
                    }

                }
            }
            else
            {
                foreach (mortor obj in MortorObj)

                {
                    if (obj != null)
                    {
                        obj.isForce = false;
                    }

                }
            }
        }
    }


}