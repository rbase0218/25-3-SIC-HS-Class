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
    
    private void Awake()
    {
        _rig = GetComponent<Rigidbody2D>();
        _coll = GetComponent<BoxCollider2D>();

        _attack = GetComponent<MonsterAttack>();
        _move = GetComponent<MonsterMove>();
    }
    
    private void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
        _move.Initialized(_rig, _playerTransform, status.moveSpeed, waitTime, status.attackRange);
        _attack.Initialize(status, _playerTransform);
    }
    
    private void Update()
    {
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
