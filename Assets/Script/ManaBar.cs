using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    public float fillAmount;

    public Image content;

    private void Start()
    {
        content.fillAmount = fillAmount;
    }

    public void HandleManaBar(float currentMana, float maxMana)
    {
        content.fillAmount = Mapping(currentMana, 0, maxMana, 0, 1);
    }

    public static float Mapping(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
}
