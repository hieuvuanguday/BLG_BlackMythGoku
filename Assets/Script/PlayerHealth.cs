using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    public GameObject bloodEffect;
    public Slider playerHealthSlider;

    void Start()
    {
        currentHealth = maxHealth;
        playerHealthSlider.maxValue = maxHealth;
        playerHealthSlider.value = maxHealth;
    }

    public void AddDamage(float damage)
    {
        if (damage <= 0) return;

        currentHealth -= damage;
        playerHealthSlider.value = currentHealth;

        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        Instantiate(bloodEffect, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
        Debug.Log("Player has died.");
    }
}
