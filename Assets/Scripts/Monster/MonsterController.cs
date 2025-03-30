using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    private Rigidbody2D _rig;
    private BoxCollider2D _coll;

    private MonsterAttack _attack;
    private MonsterMove _move;

    [SerializeField] private UnitStatus status;

    public float searchRange = .0f;
    public float waitTime = .0f;
    
    private Transform _playerTransform;
    private bool _isPlayerDetected = false;
    private Vector3 _startPosition;
    
    private void Awake()
    {
        _rig = GetComponent<Rigidbody2D>();
        _coll = GetComponent<BoxCollider2D>();

        _attack = GetComponent<MonsterAttack>();
        _move = GetComponent<MonsterMove>();
        
        // 시작 위치 저장
        _startPosition = transform.position;
    }
    
    private void Start()
    {
        // 시작 위치가 손실되었는지 확인
        if (transform.position != _startPosition)
        {
            Debug.LogWarning("Monster position changed during initialization. Restoring to: " + _startPosition);
            transform.position = _startPosition;
            if (_rig != null)
            {
                _rig.position = _startPosition;
            }
        }
        
        _playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
        
        // 약간의 지연 후 초기화 (모든 컴포넌트가 준비되었는지 확인)
        StartCoroutine(DelayedInitialization());
    }
    
    private IEnumerator DelayedInitialization()
    {
        // 1프레임 기다림
        yield return null;
        
        // 위치 유효성 재확인
        if (_rig.position.sqrMagnitude < 0.01f && _startPosition.sqrMagnitude > 0.01f)
        {
            Debug.LogWarning("Rigidbody position reset to start position: " + _startPosition);
            _rig.position = _startPosition;
        }
        
        // 컴포넌트 초기화
        _move.Initialized(_rig, _playerTransform, status.moveSpeed, waitTime, status.attackRange);
        _attack.Initialize(status, _playerTransform);
        
        Debug.Log("Monster initialized at position: " + transform.position);
    }
    
    private void Update()
    {
        // 위치 확인 (디버깅용)
        if (transform.position.sqrMagnitude < 0.01f && _startPosition.sqrMagnitude > 0.01f)
        {
            Debug.LogWarning("Monster teleported to zero! Restoring position to: " + _startPosition);
            transform.position = _startPosition;
            _rig.position = _startPosition;
            return;
        }
        
        CheckPlayerInRange();
        Movement();
    }
    
    private void CheckPlayerInRange()
    {
        if (_playerTransform == null)
            return;
            
        float distanceToPlayer = Vector2.Distance(transform.position, _playerTransform.position);
        _isPlayerDetected = distanceToPlayer <= searchRange;
    }
    
    private void Movement()
    {
        if (_isPlayerDetected)
        {
            _move.ChasePlayer(_playerTransform.position);
        }
        else
        {
            _move.RandomMovement();
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, searchRange);
    }
}