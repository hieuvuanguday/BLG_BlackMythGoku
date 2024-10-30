using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10;
    public Rigidbody2D rb;
    public int damage = 50;
    public GameObject bulletEffect;

    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        GameObject bullet = Instantiate(bulletEffect, transform.position, transform.rotation);
        Destroy(bullet, 0.5f);
        Destroy(gameObject);
    }
}
