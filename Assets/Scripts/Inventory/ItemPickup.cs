using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private int itemID;
    [SerializeField] private int amount = 1;
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    private void Start()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        
        // 아이템 아이콘 설정
        if (ItemDatabase.Instance != null)
        {
            Item item = ItemDatabase.Instance.GetItemByID(itemID);
            if (item != null && item.itemIcon != null)
            {
                spriteRenderer.sprite = item.itemIcon;
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController pc = other.GetComponent<PlayerController>();
            
            if (pc != null)
            {
                pc.PickUpItem(itemID, amount);
                Destroy(gameObject);
            }
        }
    }
}