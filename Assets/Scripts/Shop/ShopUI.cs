using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel;
    private Inventory _playerInventory;

    [SerializeField] private Transform slotParent;
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private TMP_Text moneyText;
    
    private Shop _shop;

    private void Start()
    {
        _shop = GetComponent<Shop>();
        _playerInventory = FindFirstObjectByType<Inventory>();

        _shop.OnShopItemChanged += UpdateUI;
        InitializeShopUI();
        
        ToggleShop();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            ToggleShop();
        }
    }

    private void UpdateUI()
    {
        InitializeShopUI();
    }

    private void ClearSlot()
    {
        foreach (Transform child in slotParent)
        {
            Destroy(child.gameObject);
        }
    }

    private void InitializeShopUI()
    {
        ClearSlot();
        
        var size = Mathf.Min(Mathf.Max(_shop.itemCode.Count, 0), 3);
        
        for (int i = 0; i < size; i++)
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

        moneyText.text = GameManager.Instance.gold.ToString();
    }
    
    private void ToggleShop()
    {
        shopPanel.SetActive(!shopPanel.activeSelf);
    }
}
