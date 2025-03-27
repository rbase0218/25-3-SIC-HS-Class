using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    private Transform _playerTransform;
    private UnitStatus _status;
    
    private float _attackCooldown = 0f;
    private bool _canAttack = true;
    
    public void Initialize(UnitStatus status, Transform playerTransform)
    {
        _status = status;
        _playerTransform = playerTransform;
    }
    
    private void Update()
    {
        TryAttack();
    }
    
    private void TryAttack()
    {

    }
    
    private void Attack()
    {

    }
}
