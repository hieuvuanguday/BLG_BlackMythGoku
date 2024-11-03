using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public float fillAmount;

    public Image content;

    private void Start()
    {
        content.fillAmount = fillAmount;
    }

    public  void HandleHealthBar(float currentHP, float maxHP)
    {
        content.fillAmount = Mapping(currentHP, 0, maxHP, 0, 1);
    }

    public static float Mapping(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
}
