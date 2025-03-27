using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Vector2 _moveDirection = Vector2.zero;
    private Rigidbody2D _rigidbody;
    private float _moveSpeed;
    private float _jumpPower;
    
    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundCheckDistance = 0.1f;
    [SerializeField] private LayerMask groundLayer;
    
    private void Awake()
    {
        groundLayer = LayerMask.GetMask("Ground");
    }
    
    private void FixedUpdate()
    {
        CheckGround();
        Move();
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 origin = transform.position;
        Vector3 target = origin + Vector3.down * groundCheckDistance;
        Gizmos.DrawLine(origin, target);
    }
    
    public void Initialize(Rigidbody2D rigidbody, float moveSpeed, float jumpPower)
    {
        _rigidbody = rigidbody;
        _moveSpeed = moveSpeed;
        _jumpPower = jumpPower;
    }
    
    public void HandleMovement(bool aPressed, bool dPressed, bool jumpPressed)
    {
        _moveDirection = Vector2.zero;
        
        if (aPressed) _moveDirection.x = -1f;
        if (dPressed) _moveDirection.x = 1f;
        if(jumpPressed) Jump();
    }
    
    public void Jump()
    {
        if (isGrounded)
        {
            _rigidbody.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
        }
    }
    
    private void Move()
    {
        _rigidbody.velocity = new Vector2(_moveDirection.x * _moveSpeed, _rigidbody.velocity.y);
    }
    
    private void CheckGround()
    {
        Vector2 origin = transform.position;
        Vector2 direction = Vector2.down;
        
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, groundCheckDistance, groundLayer);
        isGrounded = hit.collider != null;
        
        Debug.DrawRay(origin, direction * groundCheckDistance, isGrounded ? Color.green : Color.red);
    }
}
