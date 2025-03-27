using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            ToggleShop();
        }
    }
    
    private void ToggleShop()
    {
        shopPanel.SetActive(!shopPanel.activeSelf);
    }
}
