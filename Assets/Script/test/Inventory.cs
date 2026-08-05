using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
   
 // เก็บวัตถุที่เลือกไว้ในคลัง
    private List<GameObject> items = new List<GameObject>();
    
    public void AddItem(GameObject item)
    {
        items.Add(item);
        Debug.Log("Added " + item.name + " to inventory");
    }
    
    public List<GameObject> GetItems()
    {
        return items;
    }
    
    // อาจมีเมธอดเพิ่มเติมตามความต้องการ เช่น
    public void RemoveItem(GameObject item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            Debug.Log("Removed " + item.name + " from inventory");
        }
    }
}