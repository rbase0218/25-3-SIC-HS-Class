using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rig;
    private CapsuleCollider2D _coll;

    private PlayerAttack _attack;
    private PlayerMove _move;
    private PlayerDeath _death;

    [SerializeField] private ItemFinder finder;

    private Inventory _inventory;

    [SerializeField] private UnitStatus status;
    [SerializeField] private SliderUI healthBar;

    private void Awake()
    {
        _rig = GetComponent<Rigidbody2D>();
        _coll = GetComponent<CapsuleCollider2D>();

        _attack = GetComponent<PlayerAttack>();
        _move = GetComponent<PlayerMove>();
        _death = GetComponent<PlayerDeath>();

        _inventory = GetComponent<Inventory>();
        _move.Initialize(_rig, status.moveSpeed, status.jumpPower);

        status.Initialize();
        healthBar.SetTargetStatus(status);
    }

    private void Start()
    {
        status.OnHealthChanged += healthBar.OnHealthChanged;
    }

    private void Update()
    {
        bool aPressed = Input.GetKey(KeyCode.A);
        bool dPressed = Input.GetKey(KeyCode.D);
        bool spacePressed = Input.GetKeyDown(KeyCode.Space);
        
        _move.HandleMovement(aPressed, dPressed, spacePressed);

        if (Input.GetKeyDown(KeyCode.Q))
        {
            status.TakeDamage(100);
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            PickUpItem(1002);
        }
    }

    public void PickUpItem(int itemID, int amount = 1)
    {
        if (ItemDatabase.Instance == null) return;
        
        Item item = ItemDatabase.Instance.GetItemByID(itemID);
        if (item != null)
        {
            item.currentStackSize = amount;
            bool collected = _inventory.AddItem(item);
            
            if (collected)
            {
                Debug.Log($"Collected: {item.itemName} x{amount}");
            }
            else
            {
                Debug.Log($"Couldn't collect {item.itemName}. Inventory full!");
            }
        }
    }

    public void Heal(int amount)
    {
        status.Heal(amount);
    }
    
    public void HealFull()
    {
        Heal(status.maxHealth);
    }

    public UnitStatus GetStatus()
    {
        return status;
    }
}
