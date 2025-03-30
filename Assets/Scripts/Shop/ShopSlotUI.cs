using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlotUI : MonoBehaviour
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private TMP_Text itemText;
    [SerializeField] private TMP_Text itemGold;
    [SerializeField] private Button buyButton;

    private Inventory inv;
    private Item itemData;

    private void Start()
    {
        buyButton.onClick.AddListener(OnClickBuyButton);
    }

    public void SetItem(Item item, int price, Inventory inventory)
    {
        inv = inventory;
        
        itemData = item;
        itemIcon.sprite = itemData.itemIcon;
        itemText.text = itemData.itemName;
        
        itemGold.text = price.ToString();
    }

    public void ClearSlot()
    {
        itemIcon.sprite = null;
        itemText.text = "";
        itemGold.text = "";
    }

    private void OnClickBuyButton()
    {
        int itemPrice = (GameManager.Instance.onShopFree) ? 0 : int.Parse(itemGold.text);
        
        // 플레이어가 보유한 골드 : GameManager.Instance.gold
        // ItemSlot이 가지고 있는 아이템의 가격 : itemPrice
        if(GameManager.Instance.gold >= itemPrice)
        {
            GameManager.Instance.gold -= itemPrice;
            inv.AddItem(itemData);
        }
    }
    
}
