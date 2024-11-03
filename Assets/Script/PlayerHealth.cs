using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public GameObject player;
    public HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.HandleHealthBar(currentHealth, maxHealth);
    }

    public void AddDamage(float damage)
    {
        if (damage <= 0) return;

        currentHealth -= damage;
        healthBar.HandleHealthBar(currentHealth, maxHealth);

        if (currentHealth <= 0)
            Die();
    }

    public IEnumerable Die()
    {
        healthBar.HandleHealthBar(0, maxHealth);
        Score.scoreValue = 0;
        player.SetActive(false);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
