using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMove : MonoBehaviour
{
    private Vector2 _moveDir = Vector2.zero;
    
    private Rigidbody2D _rig;
    
    // Player의 Status들이 필요하다.
    private float _moveSpeed;
    private float _jumpPower;

    public void Initialize(Rigidbody2D rig, float moveSpeed, float jumpPower)
    {
        _rig = rig;
        _moveSpeed = moveSpeed;
        _jumpPower = jumpPower;
    }

    private void FixedUpdate()
    {
        Movement();
    }

    // HandleMovement : 키의 입력을 받아서, 플레이어가 어디로 이동해야하는지 값을 정하는 함수.
    public void HandleMovement(bool aPressed, bool dPressed)
    {
        _moveDir = Vector2.zero;

        if (aPressed) _moveDir.x = -1f;
        else if (dPressed) _moveDir.x = 1f;
    }

    private void Movement()
    {
        _rig.velocity = new Vector2(_moveDir.x * _moveSpeed, _rig.velocity.y);
    }
}
