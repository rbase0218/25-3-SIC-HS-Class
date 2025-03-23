using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ItemSlotUI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private Item item;
    private int slotIndex;
    private Inventory inventory;
    public void SetItem(Item newItem)
    {
        item = newItem;
        if (item != null)
        {
            itemIcon.sprite = item.itemIcon;
            itemIcon.enabled = true;
        }
        else
            ClearSlot();
    }
    public void ClearSlot()
    {
        item = null;
        itemIcon.sprite = null;
        itemIcon.enabled = false;
    }

    public void SetSlotIndex(int index)
    {
        slotIndex = index;
    }
    public void SetInventory(Inventory inven)
    {
        inventory = inven;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (item != null && inventory != null)
                inventory.UseItem(slotIndex);
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (item != null && inventory != null)
                inventory.RemoveItem(slotIndex);
        }
    }
}