using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFinder : MonoBehaviour
{
    // ItemFinder : 보물 위치를 가리키는 역할을 한다.
    // Position : 보물 위치
    [SerializeField] private Transform _target;

    [SerializeField] private float rotationSpeed = .0f;
    [SerializeField] private float arrivalDistance = .0f;

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        if (_target == null)
            return;

        Vector2 currentPosition = transform.position;
        Vector2 targetPosition = _target.position;
        Vector2 dir = targetPosition - currentPosition;
        
        if(dir.magnitude <= arrivalDistance)
        {
            Initialize();
            return;
        }

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

    public void Initialize()
    {
        _target = null;
        transform.rotation = Quaternion.identity;
        gameObject.SetActive(false);
    }

    public void SetTarget(Transform newTarget)
    {
        _target = newTarget;
        OnFinder();
    }

    private void OnFinder()
    {
        gameObject.SetActive(true);
    }
}
