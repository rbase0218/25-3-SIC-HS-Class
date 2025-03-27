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
        var itemPrice = int.Parse(itemGold.text);
        if(GameManager.Instance.gold >= itemPrice)
        {
            GameManager.Instance.gold -= itemPrice;
            inv.AddItem(itemData);
        }
    }
    
}
