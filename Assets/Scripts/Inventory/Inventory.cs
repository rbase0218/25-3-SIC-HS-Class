using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Inventory : MonoBehaviour
{
    // 인벤토리가 보유할 수 있는 아이템 개수
    [SerializeField] private int inventorySize = 20;
    // 인벤토리 슬롯
    private Item[] itemSlots;
    
    // 인벤토리에 있는 아이템이 변경될 때 마다 발동합니다.
    public event Action OnInventoryChanged;
    
    private void Awake()
    {
        itemSlots = new Item[inventorySize];
    }

    public bool AddItem(Item item)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            if (itemSlots[i] != null &&
                itemSlots[i].itemID == item.itemID &&
                itemSlots[i].currentStackSize < itemSlots[i].maxStackSize)
            {
                itemSlots[i].currentStackSize += item.currentStackSize;
                OnInventoryChanged?.Invoke();
                return true;
            }
        }
        for (int i = 0; i < inventorySize; i++)
        {
            if (itemSlots[i] == null)
            {
                itemSlots[i] = new Item(item);
                OnInventoryChanged?.Invoke();
                return true;
            }
        }
        return false;
    }
    
    public void RemoveItem(int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < inventorySize)
        {
            itemSlots[slotIndex] = null;
            OnInventoryChanged?.Invoke();
        }
    }
    
    public void UseItem(int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < inventorySize && itemSlots[slotIndex] != null)
        {
            itemSlots[slotIndex].Use();
            if (itemSlots[slotIndex].itemType == Item.ItemType.Consumable)
            {
                itemSlots[slotIndex].currentStackSize--;
                if (itemSlots[slotIndex].currentStackSize <= 0)
                    RemoveItem(slotIndex);
            }
            OnInventoryChanged?.Invoke();
        }
    }

    public int GetInventorySize()
    {
        return inventorySize;
    }

    public Item[] GetInventoryItems()
    {
        return itemSlots;
    }
}