using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFinder : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float arrivalDistance = 0.1f;

    private void Awake()
    {
        Initialize();
    }

    private void Update()
    {
        if (target == null)
            return;
            
        Vector2 currentPosition = transform.position;
        Vector2 targetPosition = target.position;
        Vector2 direction = targetPosition - currentPosition;
        
        if (direction.magnitude <= arrivalDistance)
        {
            Initialize();
            return;
        }
        
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
    
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        OnFinder();
    }

    public void Initialize()
    {
        target = null;
        transform.rotation = Quaternion.identity;
        gameObject.SetActive(false);
    }

    private void OnFinder()
    {
        gameObject.SetActive(true);
    }
}
