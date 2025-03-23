using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private int itemID;
    [SerializeField] private int amount = 1;
    
    private SpriteRenderer _spriteRenderer;
    
    private void Start()
    {
        if (_spriteRenderer == null)
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        
        if (ItemDatabase.Instance != null)
        {
            Item item = ItemDatabase.Instance.GetItemByID(itemID);
            if (item != null && item.itemIcon != null)
            {
                _spriteRenderer.sprite = item.itemIcon;
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