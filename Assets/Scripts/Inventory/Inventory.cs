using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Inventory : MonoBehaviour
{
    [SerializeField] private int inventorySize = 20;
    private Item[] itemSlots;
    
    public event Action OnInventoryChanged;
    
    private void Awake()
    {
        itemSlots = new Item[inventorySize];
    }

    public bool AddItem(Item item)
    {
        // 스택 가능한 아이템 처리
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
                switch (item)
                {
                    case HealPotion healPotion:
                        itemSlots[i] = new HealPotion(healPotion);
                        break;
                    default:
                        itemSlots[i] = new Item(item);
                        break;
                }
                    
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