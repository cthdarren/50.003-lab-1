using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    private float basePlayerScaleX = 3;
    private float basePlayerScaleY = 3;
    private float basePlayerScaleZ = 1;
    private float jumpForce = 10f;
    private float maxJumpHoldTime = 0.20f;
    private float holdJumpForceMultiplier = 1000;
    private float startJumpHoldThreshold = 0.06f;
    private float moveSpeed = 5f;
    private float? jumpStartTime = null;

    private Rigidbody2D rb;
    private Animator animator;
    private PlayerInput input;
    private PlayerHealth health;

    public bool isJumping = false;
    public bool isGrounded = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
        health = GetComponent<PlayerHealth>();
        rb.gravityScale = 5;
        rb.freezeRotation = true;
    }

    private void Update()
    {
        HandleFacingDirection(input.MoveDirectionX);
        HandleJump();
        animator.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));
        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("isFalling", rb.linearVelocityY < 0);
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(input.MoveDirectionX * moveSpeed, rb.linearVelocity.y);
    }

    private void HandleFacingDirection(float direction)
    {
        if (direction < 0)
            transform.localScale = new Vector3(-basePlayerScaleX, basePlayerScaleY, basePlayerScaleZ);
        else if (direction > 0)
            transform.localScale = new Vector3(basePlayerScaleX, basePlayerScaleY, basePlayerScaleZ);
    }
    public void Pogo()
    {
        isJumping = false;
        rb.totalForce = new Vector2(0, 0);
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce + 3); 
    }

    private void HandleJump()
    {
        if (input.JumpPressed && isGrounded)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            isJumping = true;
            jumpStartTime = Time.time;
        }
        if (input.JumpHeld && isJumping)
        {
            float currentTime = Time.time;
            if (!jumpStartTime.HasValue)
                return;

            float currentJumpTime = currentTime - jumpStartTime.Value;
            if (currentJumpTime < maxJumpHoldTime)
            { 
                if (currentJumpTime > startJumpHoldThreshold)
                {
                    rb.AddForce(new Vector2(0, jumpForce/(holdJumpForceMultiplier*currentJumpTime)), ForceMode2D.Impulse);
                }
            }
            else
            {
                isJumping = false;
            }
        }
        if (!input.JumpHeld)
        {
            isJumping = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.rigidbody.CompareTag("Enemy"))
        {
            health.TakeDamage();
            return;
        }

        if (collision.rigidbody.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("isGrounded", isGrounded);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.rigidbody.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
