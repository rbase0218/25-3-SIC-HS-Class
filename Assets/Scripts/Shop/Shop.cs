using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    // Item 코드를 가지고 있으며, 가격 정보를 가지고 있음
    public List<int> itemCode;
    public List<int> itemPrice;

    private void Awake()
    {
    }
}
