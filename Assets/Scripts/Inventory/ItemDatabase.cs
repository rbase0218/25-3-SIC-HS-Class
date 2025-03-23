using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase Instance { get; private set; }
    
    [SerializeField] private List<Item> items = new List<Item>();
    private Dictionary<int, Item> itemDictionary = new Dictionary<int, Item>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        InitializeItems();
    }

    private void InitializeItems()
    {
        foreach (Item item in items)
        {
            itemDictionary.TryAdd(item.itemID, item);
        }
    }

    public Item GetItemByID(int id)
    {
        if (itemDictionary.TryGetValue(id, out var item))
            return new Item(item);
        return null;
    }
}
