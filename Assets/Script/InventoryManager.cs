using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public GameObject itemSlotPrefab; // Prefab ของ Slot Item
    public Transform inventoryGrid;   // ตำแหน่งของ Grid Layout
    public Text fundsText;
    public Text itemNameText, itemDescText;
    public Button buyButton;

    private int funds = 56781;
    private InventoryItem selectedItem;

    void Start()
    {
        UpdateFundsUI();
        LoadInventory();
    }

    void LoadInventory()
    {
        foreach (ItemData item in ItemDatabase.GetAllItems())
        {
            GameObject slot = Instantiate(itemSlotPrefab, inventoryGrid);
            slot.GetComponentInChildren<Text>().text = item.name;
            slot.GetComponent<Button>().onClick.AddListener(() => ShowItemDetails(item));
        }
    }

    void ShowItemDetails(ItemData item)
    {
        selectedItem = new InventoryItem();
        itemNameText.text = item.name;
        itemDescText.text = "Price: " + item.price;
        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(() => BuyItem(item));
    }

    void BuyItem(ItemData item)
    {
        if (funds >= item.price)
        {
            funds -= item.price;
            UpdateFundsUI();
            Debug.Log("Bought " + item.name);
        }
    }

    void UpdateFundsUI()
    {
        fundsText.text = "Funds: " + funds;
    }
}

internal class InventoryItem
{
    internal bool isCraft;
}

public static class ItemDatabase
{
    public static List<ItemData> GetAllItems()
    {
        return new List<ItemData>
        {
            new ItemData("Hydraulic Cylinder", 175),
            new ItemData("Motor", 300),
            new ItemData("Gear", 150),
        };
    }
}

public class ItemData
{
    public string name;
    public int price;

    public ItemData(string name, int price)
    {
        this.name = name;
        this.price = price;
    }
}

