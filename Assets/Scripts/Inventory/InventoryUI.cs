using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InventoryUI : MonoBehaviour
{
    private Inventory _playerInventory;
    [SerializeField] private Transform itemsParent;
    [SerializeField] private GameObject itemSlotPrefab;
    [SerializeField] private GameObject inventoryPanel;
    private List<ItemSlotUI> itemSlots = new List<ItemSlotUI>();
    private void Start()
    {
        _playerInventory = FindFirstObjectByType<Inventory>();
        if (_playerInventory != null)
            _playerInventory.OnInventoryChanged += UpdateUI;
        InitializeInventoryUI();
        UpdateUI();
        inventoryPanel.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleInventory();
        }
    }
    
    public void ToggleInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
    }
    private void InitializeInventoryUI()
    {
        for (int i = 0; i < _playerInventory.GetInventorySize(); i++)
        {
            GameObject slotObj = Instantiate(itemSlotPrefab, itemsParent);
            ItemSlotUI slot = slotObj.GetComponent<ItemSlotUI>();
            if (slot != null)
            {
                slot.SetSlotIndex(i);
                slot.ClearSlot();
                slot.SetInventory(_playerInventory);
                itemSlots.Add(slot);
            }
        }
    }
    
    private void UpdateUI()
    {
        Item[] items = _playerInventory.GetInventoryItems();
        for (int i = 0; i < itemSlots.Count; i++)
        {
            if (i < items.Length)
                itemSlots[i].SetItem(items[i]);
            else
                itemSlots[i].ClearSlot();
        }
    }
}