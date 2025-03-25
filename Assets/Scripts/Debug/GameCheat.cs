using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameCheat : MonoBehaviour
{
    public bool onDebugMode;
    
    [Header("Cheat Events")]
    public UnityEvent onF2Pressed;
    public UnityEvent onF3Pressed;
    public UnityEvent onF4Pressed;
    public UnityEvent onF5Pressed;

    private void Update()
    {
        if (onDebugMode)
        {
            if (Input.GetKeyDown(KeyCode.F2))
                onF2Pressed?.Invoke();
            
            if (Input.GetKeyDown(KeyCode.F3))
                onF3Pressed?.Invoke();
            
            if (Input.GetKeyDown(KeyCode.F4))
                onF4Pressed?.Invoke();
            
            if (Input.GetKeyDown(KeyCode.F5))
                onF5Pressed?.Invoke();
        }
    }
}
