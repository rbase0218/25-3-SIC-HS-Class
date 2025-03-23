using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class UnitStatus
{
    public int maxHealth;
    public int currentHealth;

    public int damage;
    public float attackSpeed;
    public float attackRange;

    public float moveSpeed;
    public float jumpPower;
    
    public event Action OnHealthChanged;
    
    public UnitStatus(int maxHealth, int currentHealth, int damage, float attackSpeed, float attackRange, float moveSpeed, float jumpPower)
    {
        this.maxHealth = maxHealth;
        this.currentHealth = currentHealth;
        this.damage = damage;
        this.attackSpeed = attackSpeed;
        this.attackRange = attackRange;
        this.moveSpeed = moveSpeed;
        this.jumpPower = jumpPower;
    }

    public void Initialize() => currentHealth = maxHealth;
    
    public void TakeDamage(int damageAmount)
    {
        if (damageAmount <= 0) return;
        
        currentHealth -= damageAmount;
        
        if (currentHealth < 0)
            currentHealth = 0;
            
        OnHealthChanged?.Invoke();
    }
    
    public void Heal(int healAmount)
    {
        if (healAmount <= 0) return;
        
        currentHealth += healAmount;
        
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
            
        OnHealthChanged?.Invoke();
    }
    
    public float GetHealthRatio()
    {
        return (float)currentHealth / maxHealth;
    }
}
