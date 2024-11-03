using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float maxHealth = 200f;
    float currHealth;
    public GameObject deathEffect;

    public Transform pointA;
    public Transform pointB;
    public float speed = 10f;
    public ManaBar manaBar;
    public PlayerMovement playerMovement;
    private Vector3 targetPosition;

    void Start()
    {
        manaBar = FindObjectOfType<ManaBar>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        currHealth = maxHealth;

        targetPosition = pointB.position;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            targetPosition = targetPosition == pointA.position ? pointB.position : pointA.position;
        }
    }

    public void TakeDamage (float damage)
    {
        currHealth -= damage;

        if (currHealth <= 0)
            Die();
    }

    void Die()
    {
        Score.scoreValue += 20;
        playerMovement.currentStamina += 100;
        manaBar.HandleManaBar(playerMovement.currentStamina, playerMovement.maxStamina);
        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 0.5f);
        Destroy(gameObject);
    }
}
