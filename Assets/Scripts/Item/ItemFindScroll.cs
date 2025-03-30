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
        
        // 플레이어 오브젝트를 불러오고 ItemFinder를 실행하면 된다.
        var pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        pc.StartItemFind();
    }
}
