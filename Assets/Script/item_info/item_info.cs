using UnityEngine;

[CreateAssetMenu(fileName = "item_info", menuName = "ScriptableObjects/item_info", order = 1)]

public class Item_info : ScriptableObject 
{
    public string Item_name;
    public int Item_Type;
    public int ItemBlockInfo; //itemtypeว่าเชื่อมกับtypeอะไร

}
