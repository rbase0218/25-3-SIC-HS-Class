using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HealPotion : Item
{
    [SerializeField] private int healAmount = 20;

    public HealPotion() : base() { }

    public HealPotion(string name, int id, string description, int maxStack, int healAmount) 
        : base(name, id, description, ItemType.Consumable, maxStack)
    {
        this.healAmount = healAmount;
    }

    public HealPotion(HealPotion item) : base(item)
    {
        healAmount = item.healAmount;
    }

    public override void Use()
    {
        base.Use();
        
        Debug.Log("힐 포션 사용: " + itemName + " (회복량: " + healAmount + ")");
        
        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        player?.Heal(healAmount);
    }
}
