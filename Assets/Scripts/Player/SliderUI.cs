using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderUI : MonoBehaviour
{
    [SerializeField] private Image hpSlider;
    [SerializeField] private float updateSpeed = 5f;
    
    private UnitStatus targetStatus;
    
    private float targetFillAmount;
    
    private void Start()
    {
        if (targetStatus != null)
            UpdateHealthBar();
    }
    
    private void Update()
    {
        if (targetStatus != null)
        {
            float currentFillAmount = hpSlider.fillAmount;
            float newFillAmount = Mathf.Lerp(currentFillAmount, targetFillAmount, Time.deltaTime * updateSpeed);
            hpSlider.fillAmount = newFillAmount;
        }
    }
    
    public void SetTargetStatus(UnitStatus status)
    {
        targetStatus = status;
        UpdateHealthBar();
    }
    
    public void UpdateHealthBar()
    {
        if (targetStatus != null && hpSlider != null)
        {
            targetFillAmount = (float)targetStatus.currentHealth / targetStatus.maxHealth;
            
            targetFillAmount = Mathf.Clamp01(targetFillAmount);
        }
    }
    
    public void OnHealthChanged()
    {
        UpdateHealthBar();
    }
}
