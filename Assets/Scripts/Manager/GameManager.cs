using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [Header("게임 내에 있는 보물 목록")]
    public List<Transform> treasures = new List<Transform>();

    [Header("치트 목록")]
    public bool isFreeShopping = false;

    public int gold = 0;
    
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
    
    public void ToggleFreeShopping()
    {
        isFreeShopping = !isFreeShopping;
    }

    public Transform GetTreasure()
    {
        return treasures[Random.Range(0, treasures.Count)];
    }
}
