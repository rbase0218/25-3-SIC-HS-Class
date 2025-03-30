using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    public int gold = 0;
    public bool onShopFree = false;
    
    [Header("보물 목록")]
    public List<Transform> treasures;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void ToggleShopFree()
    {
        onShopFree = !onShopFree;
    }

    public void AddTreasure(Transform newTreasure)
    {
        treasures.Add(newTreasure);
    }

    public Transform GetRandTreasure()
    {
        return treasures[Random.Range(0, treasures.Count)];
    }
}
