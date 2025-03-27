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

    private void Start()
    {
        buyButton.onClick.AddListener(OnClickBuyButton);
    }

    public void SetItem(Item item, int price)
    {
        itemIcon.sprite = item.itemIcon;
        itemText.text = item.itemName;
        itemGold.text = price.ToString();
    }

    private void OnClickBuyButton()
    {
        
    }
    
}
