using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Pipes;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerController playerController;
    public Animator animator;
    public float runSpeed = 20f;
    public float horizontalMove = 0f;
    public bool isJump = false;
    public bool isCrouch = false;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int damagePunch = 1;
    public int damageKick = 2;
    public Transform ki;
    public GameObject bulletPrefab;

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));


        if (Input.GetButtonDown("Jump"))
        {
            isJump = true;
            animator.SetBool("isJumping", true);
        }
        
        if(Input.GetButtonDown("Crouch"))
            isCrouch = true;
        else if (Input.GetButtonUp("Crouch"))
            isCrouch = false;

        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetBool("isKame", true);
            Shoot();
        } else if (Input.GetButtonUp("Fire1"))
            animator.SetBool("isKame", false);

        if (Input.GetButton("Fire2"))
            Punch();

        if (Input.GetButton("Fire3"))
            Kick();
    }

    public void Punch()
    {
        animator.SetTrigger("isPunching");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(damagePunch);
        }
    }

    public void Kick()
    {
        animator.SetTrigger("isKicking");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(damageKick);
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, ki.position, ki.rotation);
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void OnLanding()
    {   
        animator.SetBool("isJumping", false);
    }

    public void OnCrouching(bool isCrouching)
    {
        animator.SetBool("isCrouching", isCrouching);
    }

    void FixedUpdate() 
    {
        playerController.Move(horizontalMove * Time.fixedDeltaTime, isCrouch, isJump);
        isJump = false;
    }
}
    