using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class PlayerController : MonoBehaviour
{
    // 플레이어가 가지고 있는 상태
    // Attack
    // Death
    // Move
    
    // 1.  플레이어는 한 번에 하나의 상태만 유지한다.

    private Rigidbody2D _rig;
    private CapsuleCollider2D _coll;

    private PlayerAttack _attack;
    private PlayerMove _move;
    private PlayerDeath _death;

    private Inventory _inventory;

    [SerializeField] private UnitStatus status;

    private void Awake()
    {
        _rig = GetComponent<Rigidbody2D>();
        _coll = GetComponent<CapsuleCollider2D>();

        _attack = GetComponent<PlayerAttack>();
        _move = GetComponent<PlayerMove>();
        _death = GetComponent<PlayerDeath>();

        _inventory = GetComponent<Inventory>();
        _move.Initialize(_rig, status.moveSpeed, status.jumpPower);
    }

    private void Update()
    {
        bool aPressed = Input.GetKey(KeyCode.A);
        bool dPressed = Input.GetKey(KeyCode.D);
        
        _move.HandleMovement(aPressed, dPressed);
    }
}
