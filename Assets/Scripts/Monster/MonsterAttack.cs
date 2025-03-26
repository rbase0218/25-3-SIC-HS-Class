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
        if (!_canAttack)
        {
            _attackCooldown -= Time.deltaTime;
            if (_attackCooldown <= 0f)
                _canAttack = true;
        }
        
        TryAttack();
    }
    
    private void TryAttack()
    {
        if (_playerTransform == null || !_canAttack)
            return;
        
        float distanceToPlayer = Vector2.Distance(transform.position, _playerTransform.position);
        
        if (distanceToPlayer <= _status.attackRange)
            Attack();
    }
    
    private void Attack()
    {
        _canAttack = false;
        _attackCooldown = 1f / _status.attackSpeed;
        
        var pc = _playerTransform.GetComponent<PlayerController>();
        
        pc.GetStatus().TakeDamage(_status.damage);
    }
}
