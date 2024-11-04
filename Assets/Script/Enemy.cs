using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float maxHealth = 200f;
    float currHealth;
    public GameObject deathEffect;
    public ManaBar manaBar;
    public PlayerMovement playerMovement;
    public PlayerHealth playerHealth;
    public HealthBar healthBar;

    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody2D rb;
    private Animator anim;
    private Transform currPoint;
    public EnemyHealth enemyHealth;

    public int count = 0;

    public float speed = 0.01f;

    void Start()
    {
        manaBar = FindObjectOfType<ManaBar>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        playerHealth = FindObjectOfType<PlayerHealth>();
        healthBar = FindObjectOfType<HealthBar>();
        enemyHealth = FindObjectOfType<EnemyHealth>();

        currHealth = maxHealth;
        count = 0;
        enemyHealth.HandleEnemyHealth(currHealth, maxHealth);

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currPoint = pointB.transform;
    }

    void Update()
    {
        Vector2 point = currPoint.position - transform.position;
        if (currPoint == pointB.transform)
            rb.velocity = new Vector2(speed, 0);
        else
            rb.velocity = new Vector2(-speed, 0);

        if (Vector2.Distance(transform.position, currPoint.position) < 0.5f && currPoint == pointB.transform)
        {
            Flip();
            currPoint = pointA.transform;
        }

        if (Vector2.Distance(transform.position, currPoint.position) < 0.5f && currPoint == pointA.transform)
        {
            Flip();
            currPoint = pointB.transform;
        }
    }

    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Flip();
            currPoint = currPoint == pointA.transform ? pointB.transform : pointA.transform;
            HitPlayer();
        }
    }

    public void HitPlayer()
    {
        PlayerHealth playerHealth = playerMovement.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.AddDamage(2f);
        }
    }

    public void TakeDamage (float damage)
    {
        currHealth -= damage;
        enemyHealth.HandleEnemyHealth(currHealth, maxHealth);

        if (currHealth <= 0)
        {
            enemyHealth.HandleEnemyHealth(0, maxHealth);
            Die();
            count++;
        }
    }

    void Die()
    {
        Score.scoreValue += 50;
        if (playerMovement.currentStamina + 300 > 400)
            playerMovement.currentStamina = 400;
        else
            playerMovement.currentStamina += 300;

        if (playerHealth.currentHealth + 100 > 200)
            playerHealth.currentHealth = 200;
        else
            playerHealth.currentHealth += 100;
        
        healthBar.HandleHealthBar(playerHealth.currentHealth, playerHealth.maxHealth);
        manaBar.HandleManaBar(playerMovement.currentStamina, playerMovement.maxStamina);
        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 0.5f);
        Destroy(gameObject);
    }
}
