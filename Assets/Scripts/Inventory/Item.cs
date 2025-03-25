using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Item
{
    public string itemName;
    
    public int itemID;
    public string itemDescription;
    
    // Item의 아이콘
    public Sprite itemIcon;
    
    public int maxStackSize = 1;
    public int currentStackSize = 1;
    
    public enum ItemType
    {
        Consumable,
        Quest,
    }
    
    public ItemType itemType;
    
    public Item() {}
    
    public Item(string name, int id, string description, ItemType type, int maxStack = 1)
    {
        itemName = name;
        itemID = id;
        itemDescription = description;
        itemType = type;
        maxStackSize = maxStack;
        currentStackSize = 1;
    }
    public Item(Item item)
    {
        itemName = item.itemName;
        itemID = item.itemID;
        itemDescription = item.itemDescription;
        itemIcon = item.itemIcon;
        maxStackSize = item.maxStackSize;
        currentStackSize = item.currentStackSize;
        itemType = item.itemType;
    }
    
    public virtual void Use()
    {
        Debug.Log("사용한 아이템 : " + itemName);
    }
}