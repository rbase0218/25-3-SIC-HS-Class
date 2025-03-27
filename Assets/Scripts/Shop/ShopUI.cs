using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel;
    private Inventory _playerInventory;

    [SerializeField] private Transform slotParent;
    [SerializeField] private GameObject slotPrefab;
    
    private Shop _shop;

    private void Start()
    {
        _shop = GetComponent<Shop>();
        _playerInventory = FindFirstObjectByType<Inventory>();
        InitializeShopUI();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            ToggleShop();
        }
    }

    private void InitializeShopUI()
    {
        for (int i = 0; i < 3; i++)
        {
            var slotObj = Instantiate(slotPrefab, slotParent);
            var slot = slotObj.GetComponent<ShopSlotUI>();
            
            if (slot != null)
            {
                var itemCode = _shop.itemCode[i];
                var item = ItemDatabase.Instance.GetItemByID(itemCode);
                
                slot.ClearSlot();
                slot.SetItem(item, _shop.itemPrice[i], _playerInventory);
            }
        }
    }
    
    private void ToggleShop()
    {
        shopPanel.SetActive(!shopPanel.activeSelf);
    }
}
