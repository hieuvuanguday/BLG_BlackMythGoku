using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float maxHealth;
    float currHealth;


    public GameObject deathEffect;

    //khai bao cac bien de tao thanh mau cho enemy
    public Slider enemyHealthSlider;

    void Start()
    {
        currHealth = maxHealth;

        enemyHealthSlider.maxValue = maxHealth;
        enemyHealthSlider.value = maxHealth;
    }


    void Update()
    {

    }


    public void TakeDamage (float damage)
    {
        currHealth -= damage;

        enemyHealthSlider.value = currHealth;

        if (currHealth <= 0)
            Die();
    }

    void Die()
    {
        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 0.5f);
        Destroy(gameObject);
    }
}
