using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Item
{
    // Item의 이름
    public string itemName;
    
    // Item이 가지고 있는 고유한 값. ( 우리의 주민등록번호 )
    public int itemID;
    // Item이 어떤 역할을 하는지 알려줄 설명
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