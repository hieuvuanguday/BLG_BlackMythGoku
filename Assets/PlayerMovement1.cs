using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Pipes;
using UnityEngine;

public class PlayerMovement1 : MonoBehaviour
{

    public CharacterController2D controller;

    public Animator animator;
    public float runSpeed = 20f;
    public float horizontalMove = 0f;
    public bool isJump = false;
    public bool isCrouch = false;

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));


        if (Input.GetButtonDown("Jump"))
        {
            isJump = true;
            animator.SetBool("isJumping", true);
        }

        if (Input.GetButtonDown("Crouch"))
        {
            isCrouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            isCrouch = false;
        }
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
        controller.Move(horizontalMove * Time.fixedDeltaTime, isCrouch, isJump);
        isJump = false;
    }

}