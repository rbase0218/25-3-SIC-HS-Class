using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase Instance { get; private set; }
    
    [SerializeField] private List<Item> items = new List<Item>();
    [SerializeField] private List<HealPotion> healPotions = new List<HealPotion>();
    
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
            itemDictionary.TryAdd(item.itemID, item);
            
        foreach (HealPotion potion in healPotions)
            itemDictionary.TryAdd(potion.itemID, potion);
    }

    public Item GetItemByID(int id)
    {
        if (itemDictionary.TryGetValue(id, out Item item))
        {
            if (item is HealPotion healPotion)
                return new HealPotion(healPotion);
            else
                return new Item(item);
        }
        return null;
    }
}
