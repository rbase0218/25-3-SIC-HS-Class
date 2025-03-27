using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    // Item 코드를 가지고 있으며, 가격 정보를 가지고 있음
    public List<int> itemCode;
    public List<int> itemPrice;
    
    public event Action OnShopItemChanged;

    public void AddItem(int id, int price)
    {
        itemCode.Add(id);
        itemPrice.Add(price);
        
        OnShopItemChanged?.Invoke();
    }

    public void ClearAllItem()
    {
        itemCode.Clear();
        itemPrice.Clear();
        
        OnShopItemChanged?.Invoke();
    }
}
