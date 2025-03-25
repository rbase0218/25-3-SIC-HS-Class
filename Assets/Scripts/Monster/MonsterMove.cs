using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonoBehaviour
{
    private Rigidbody2D _rig;
    private float _moveSpeed = .0f;
    private float _waitTime = .0f;
    
    private float _timer = .0f;
    private Vector2 _targetPosition;
    private bool _isMoving = false;
    private bool _isChasingPlayer = false;
    
    private Vector2 _movementDirection;
    private float _randomMoveDistance = 3f;
    
    public void Initialized(Rigidbody2D rig, float moveSpeed, float waitTime)
    {
        _rig = rig;
        _moveSpeed = moveSpeed;
        _waitTime = waitTime;
        
        SetNewRandomTarget();
    }
    
    private void Update()
    {
        if (_isChasingPlayer)
        {
            MoveToTarget();
            return;
        }
        
        if (_isMoving)
        {
            MoveToTarget();
            
            if (Vector2.Distance(transform.position, _targetPosition) < 0.1f)
            {
                _isMoving = false;
                _timer = 0f;
            }
        }
        else
        {
            _timer += Time.deltaTime;
            
            if (_timer >= _waitTime)
                SetNewRandomTarget();
        }
    }
    
    public void RandomMovement()
    {
        _isChasingPlayer = false;
    }
    
    public void ChasePlayer(Vector2 playerPosition)
    {
        _isChasingPlayer = true;
        _targetPosition = playerPosition;
        _movementDirection = (_targetPosition - (Vector2)transform.position).normalized;
        UpdateFacingDirection();
    }
    
    private void SetNewRandomTarget()
    {
        float randomX = Random.Range(-_randomMoveDistance, _randomMoveDistance);
        _targetPosition = (Vector2)transform.position + new Vector2(randomX, 0f);
        _movementDirection = (_targetPosition - (Vector2)transform.position).normalized;
        _isMoving = true;
        UpdateFacingDirection();
    }
    
    private void MoveToTarget()
    {
        Vector2 movement = _movementDirection * _moveSpeed * Time.deltaTime;
        _rig.position += movement;
    }
    
    private void UpdateFacingDirection()
    {
        if (_movementDirection.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (_movementDirection.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }
}