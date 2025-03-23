using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MonoBehaviour가 있으면 인스펙터에서 오브젝트에 추가할 수 있다.
[System.Serializable]
public class UnitStatus
{
    public int maxHp;
    public int currentHp;

    public int damage;
    public float attackSpeed;
    public float attackRange;

    public float moveSpeed;
    public float jumpPower;

    public void Initialize()
    {
        currentHp = maxHp;
    }
}
