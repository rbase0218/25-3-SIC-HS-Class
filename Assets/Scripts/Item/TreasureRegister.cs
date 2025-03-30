using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureRegister : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.AddTreasure(transform);
    }
}
