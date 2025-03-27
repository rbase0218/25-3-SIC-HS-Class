using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemFindScroll : Item
{
    public ItemFindScroll() : base() { }

    public ItemFindScroll(string name, int id, string description, ItemType type, int maxStack)
        : base(name, id, description, type, maxStack)
    {
    }

    public ItemFindScroll(ItemFindScroll item) : base(item)
    {
    }

    public override void Use()
    {
        base.Use();
    }
}
