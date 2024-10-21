using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float jumpForce = 3.5f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    private Rigidbody2D rb;
    private bool isGrounded;
    private Animator animator;

    public GameObject idle;
    public GameObject run;
    public GameObject jump;
    public GameObject kick;
    public GameObject punch;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        ShowIdle();
    }

    private void Update()
    {
        Move();
        Jump();
        Kick();
        Punch();

        UpdateAnimationState();
    }

    void Move()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        animator.SetBool("isRunning", moveInput != 0);
    }

    void Jump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetBool("isJumping", true);
            ShowJump();
        }
        else if (Input.GetButtonUp("Jump"))
        {
            animator.SetBool("isJumping", false);
        }
    }

    void Kick()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            animator.SetBool("isKicking", true);
            ShowKick();
        }
        else if (Input.GetKeyUp(KeyCode.K))
        {
            animator.SetBool("isKicking", false);
        }
    }

    void Punch()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            animator.SetBool("isPunching", true);
            ShowPunch();
        }
        else if (Input.GetKeyUp(KeyCode.P))
        {
            animator.SetBool("isPunching", false);
        }
    }

    void UpdateAnimationState()
    {
        if (animator.GetBool("isJumping"))
        {
            ShowJump();
        }
        else if (animator.GetBool("isKicking"))
        {
            ShowKick();
        }
        else if (animator.GetBool("isPunching"))
        {
            ShowPunch();
        }
        else if (animator.GetBool("isRunning"))
        {
            ShowRun();
        }
        else
        {
            ShowIdle();
        }
    }

    void ShowIdle()
    {
        SetActiveState(idle);
    }

    void ShowRun()
    {
        SetActiveState(run);
    }

    void ShowJump()
    {
        SetActiveState(jump);
    }

    void ShowKick()
    {
        SetActiveState(kick);
    }

    void ShowPunch()
    {
        SetActiveState(punch);
    }

    void SetActiveState(GameObject activeObject)
    {
        Vector3 currentPosition = Vector3.zero;

        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf)
            {
                currentPosition = child.position;
                break;
            }
        }

        idle.SetActive(false);
        run.SetActive(false);
        jump.SetActive(false);
        kick.SetActive(false);
        punch.SetActive(false);

        activeObject.SetActive(true);
        activeObject.transform.position = currentPosition;
    }
}
