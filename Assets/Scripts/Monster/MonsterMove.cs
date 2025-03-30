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
    
    private Transform _playerTransform;
    private float _attackRange = 0f;
    private bool _isInAttackRange = false;
    
    // 블럭 경계 감지를 위한 변수들
    [SerializeField] private float _groundCheckDistance = 1.0f;
    [SerializeField] private LayerMask _groundLayer;
    private float _initialY;
    
    public void Initialized(Rigidbody2D rig, Transform playerTransform, float moveSpeed, float waitTime, float attackRange)
    {
        _rig = rig;
        _moveSpeed = moveSpeed;
        _waitTime = waitTime;
        _attackRange = attackRange;
        _playerTransform = playerTransform;
        
        // 초기 Y 위치 저장
        _initialY = transform.position.y;
        
        SetNewRandomTarget();
    }
    
    private void Update()
    {
        if (_isChasingPlayer)
        {
            CheckAttackRange();
            
            if (!_isInAttackRange)
                MoveToTarget();
                
            return;
        }
        
        if (_isMoving)
        {
            MoveToTarget();
            
            if (Vector2.Distance(new Vector2(transform.position.x, 0), new Vector2(_targetPosition.x, 0)) < 0.1f)
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
    
    private void CheckAttackRange()
    {
        if (_playerTransform == null)
            return;
            
        float distanceToPlayer = Vector2.Distance(transform.position, _playerTransform.position);
        _isInAttackRange = distanceToPlayer <= _attackRange;
        
        if (!_isInAttackRange)
        {
            Vector2 playerPos = _playerTransform.position;
            
            // 플레이어가 같은 Y 레벨에 있는지 확인
            if (Mathf.Abs(playerPos.y - _initialY) > 0.5f)
            {
                // 다른 Y 레벨이면 추적 중단
                _isChasingPlayer = false;
                SetNewRandomTarget();
                return;
            }
            
            // 플레이어 위치를 향해 이동하되, Y 값은 초기값으로 고정
            _targetPosition = new Vector2(playerPos.x, _initialY);
            _movementDirection = (new Vector2(_targetPosition.x - transform.position.x, 0)).normalized;
            
            // 블럭 경계 체크
            if (!IsGroundAhead())
            {
                // 블럭 끝에 도달했으면 추적 중단
                _isChasingPlayer = false;
                _movementDirection = -_movementDirection; // 방향 전환
                SetNewRandomTarget();
                return;
            }
            
            UpdateFacingDirection();
        }
    }
    
    public void RandomMovement()
    {
        _isChasingPlayer = false;
        _isInAttackRange = false;
    }
    
    public void ChasePlayer(Vector2 playerPosition)
    {
        // 플레이어가 다른 Y 레벨에 있는지 확인
        if (Mathf.Abs(playerPosition.y - _initialY) > 0.5f)
        {
            // 다른 Y 레벨이면 추적하지 않음
            _isChasingPlayer = false;
            return;
        }
        
        _isChasingPlayer = true;
        // Y 값은 초기값으로 고정
        _targetPosition = new Vector2(playerPosition.x, _initialY);
        _movementDirection = (new Vector2(_targetPosition.x - transform.position.x, 0)).normalized;
        
        // 플레이어를 향해 이동할 때 블럭 경계 체크
        if (!IsGroundAhead())
        {
            _isChasingPlayer = false;
            return;
        }
        
        UpdateFacingDirection();
    }
    
    private void SetNewRandomTarget()
    {
        // 현재 위치를 기준으로 랜덤 방향 선택
        float randomX = Random.Range(-_randomMoveDistance, _randomMoveDistance);
        
        // 랜덤 값이 너무 작으면 최소한의 이동 거리 보장
        if (Mathf.Abs(randomX) < 0.5f)
        {
            randomX = (randomX >= 0) ? 0.5f : -0.5f;
        }
        
        // 블럭 경계 체크
        if (randomX > 0 && !IsGroundAhead(randomX))
        {
            randomX = -randomX; // 방향 전환
        }
        else if (randomX < 0 && !IsGroundAhead(randomX))
        {
            randomX = -randomX; // 방향 전환
        }
        
        // 이동 타겟 설정 (현재 위치 기반)
        Vector2 currentPos = transform.position;
        _targetPosition = new Vector2(currentPos.x + randomX, _initialY);
        
        // 방향 벡터 계산 (정규화)
        _movementDirection = new Vector2(randomX, 0).normalized;
        _isMoving = true;
        
        // 디버그 로그 추가
        Debug.Log($"New target set: Current={currentPos}, Target={_targetPosition}, Direction={_movementDirection}");
        
        UpdateFacingDirection();
    }
    
    private void MoveToTarget()
    {
        // 이동하기 전에 현재 위치 확인 (0,0으로 텔레포트 방지)
        if (_rig.position.sqrMagnitude < 0.01f && transform.position.sqrMagnitude > 0.01f)
        {
            // 리지드바디 위치가 (0,0)이고 트랜스폼 위치가 다르면 리지드바디 위치 재설정
            _rig.position = transform.position;
            Debug.LogWarning("Fixed rigidbody position: " + _rig.position);
            return;
        }
        
        // 이동하기 전에 블럭 경계 체크
        if (!IsGroundAhead())
        {
            // 블럭 끝에 도달했으면 방향 전환
            _movementDirection = -_movementDirection;
            _targetPosition = (Vector2)transform.position + _movementDirection * _randomMoveDistance;
            UpdateFacingDirection();
            return;
        }
        
        // 이동 방향과 속도 계산
        Vector2 movement = _movementDirection * _moveSpeed * Time.deltaTime;
        movement.y = 0f; // Y축 이동 고정
        
        // 현재 Y 위치 유지
        Vector2 newPosition = _rig.position + movement;
        newPosition.y = _initialY;
        
        // 새 위치가 유효한지 확인 (NaN이나 무한대 방지)
        if (float.IsNaN(newPosition.x) || float.IsInfinity(newPosition.x) || 
            float.IsNaN(newPosition.y) || float.IsInfinity(newPosition.y))
        {
            Debug.LogError("Invalid position calculated: " + newPosition);
            return;
        }
        
        _rig.position = newPosition;
    }
    
    private bool IsGroundAhead(float distance = 0)
    {
        // 이동 방향으로 레이캐스트를 쏴서 블럭이 있는지 확인
        Vector2 rayStart = transform.position;
        Vector2 rayDirection = distance != 0 ? new Vector2(distance > 0 ? 1 : -1, -1) : new Vector2(_movementDirection.x, -1);
        
        // 아래 방향으로 레이캐스트를 쏴서 블럭이 있는지 확인 (발 아래)
        RaycastHit2D groundHit = Physics2D.Raycast(
            rayStart + new Vector2(_movementDirection.x * _groundCheckDistance, 0),
            Vector2.down,
            _groundCheckDistance,
            _groundLayer
        );
        
        // 디버깅을 위한 레이 표시
        Debug.DrawRay(
            rayStart + new Vector2(_movementDirection.x * _groundCheckDistance, 0),
            Vector2.down * _groundCheckDistance,
            groundHit ? Color.green : Color.red,
            0.1f
        );
        
        return groundHit;
    }
    
    private void UpdateFacingDirection()
    {
        if (_movementDirection.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (_movementDirection.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }
    
    private void OnDrawGizmos()
    {
        // 레이캐스트 시각화
        Gizmos.color = Color.red;
        Gizmos.DrawLine(
            transform.position,
            transform.position + new Vector3(_movementDirection.x * _groundCheckDistance, -_groundCheckDistance, 0)
        );
    }
}