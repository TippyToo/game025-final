using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Movement variables
    [Tooltip("How fast the character moves")]
    public float moveSpeed;
    [Tooltip("How high the character jumps")]

    // Jump variables
    public float jumpStrength;
    [Tooltip("The transform used to denote the positioning of the ground check")]
    public Transform groundCheck;
    [Tooltip("How close the groundcheck has to be to the ground to qualify as grounded")]
    public float groundCheckSensitivty;
    private bool isGrounded;
    [Tooltip("How long after jumping before jumps can refresh. Used for ground check leniency to prevent unintended double jumps")]
    public float jumpRefreshCooldown;
    private float initJumpRefreshCooldown;
    private int jumpsLeft;
    [Tooltip("How many jumps the character can perform before needing to land")]
    public int totalJumps;
    [Tooltip("What layer is counted as ground, for the purpose of determining ground check")]
    public LayerMask groundLayer;

    // Dash variables
    [Tooltip("How long in seconds to dash for")]
    public float dashTime;
    [Tooltip("Dashing speed is moveSpeed times this")]
    public float dashSpeedMultiplier;
    [Tooltip("How long to wait before dash is available again")]
    public float dashCooldown;
    private float initDashCooldown;
    private float dashTimeLeft;
    private float dashInitVelocity;
    [Tooltip("Text field to display dash status in")]
    public Text dashText;

    // Attack variables
    public CapsuleCollider2D attackRange;
    public float attackCooldown;
    private float attackCooldownLeft;
    public float attackDuration;
    public int attackDamage;
    public Transform attackLocation;
    private float attackTimeLeft;

    // Animation variables
    private Animator myAnim;

    // Health variables
    [Tooltip("How much health the player currently has")]
    public int currentHealth;
    [Tooltip("Maximum health the player can have")]
    public int maxHealth;
    [Tooltip("Text Element to display health in")]
    public Text healthDisplay;

    // Utility variables
    private Rigidbody2D myRigidbody;
    private bool controlLock = false;
    private enum direction { Left, Right } 
    private direction facing;
    private static float YLIMIT = -100;

    void Start()
    {
        attackCooldownLeft = attackCooldown;
        attackRange.enabled = false;
        myRigidbody = GetComponent<Rigidbody2D>();
        initJumpRefreshCooldown = jumpRefreshCooldown;
        initDashCooldown = dashCooldown;
        dashCooldown = 0f;
        myAnim = GetComponent<Animator>();
        if (moveSpeed <= 0) { Debug.LogWarning("character moveSpeed is set to 0 or less"); }
        if (jumpStrength <= 0) { Debug.LogWarning("character jumpStrength is set to 0 or less"); }

    }

    void Update()
    {
        if (transform.localScale.x < 0) facing = direction.Left; else facing = direction.Right;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckSensitivty, groundLayer);
        myAnim.SetBool("Dashing", dashTimeLeft > 0);
        Debug.Log("dashing = " + (dashTimeLeft > 0));
        if (!controlLock)
        {
            // Horizontal movement
            if (Input.GetAxis("Horizontal") < 0) // Left
            {
                myRigidbody.velocity = new Vector2(-moveSpeed, myRigidbody.velocity.y);
                transform.localScale = new Vector2(-1, 1);
            }
            else if (Input.GetAxis("Horizontal") > 0) // Right
            {
                myRigidbody.velocity = new Vector2(moveSpeed, myRigidbody.velocity.y);
                transform.localScale = new Vector2(1, 1);

            }
            else // Stop
            {
                myRigidbody.velocity = new Vector2(0, myRigidbody.velocity.y);
            }

            // Jump
            if (Input.GetButtonDown("Jump") && jumpsLeft > 0)
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpStrength);
                myAnim.SetTrigger("Jump");
                jumpsLeft--;
            }

            // Dash
            if (Input.GetButtonDown("Dash") && dashTimeLeft == -1 && dashCooldown <= 0)
            {
                dashTimeLeft = dashTime;
                dashCooldown = initDashCooldown;
            }
        }
        // Dash handler
        if (dashTimeLeft == dashTime)
        {
            dashInitVelocity = myRigidbody.velocity.x;
            controlLock = true;
            dashTimeLeft -= Time.deltaTime;
        }
        else if (dashTimeLeft > 0f)
        {
            myRigidbody.velocity = facing == direction.Left ? new Vector2(-moveSpeed * dashSpeedMultiplier, 0f) : new Vector2(moveSpeed * dashSpeedMultiplier, 0f);
            dashTimeLeft -= Time.deltaTime;
            if (dashTimeLeft < 0f) dashTimeLeft = 0f;
        }
        else if (dashTimeLeft == 0f)
        {
            myRigidbody.velocity = new Vector2(dashInitVelocity, 0f);
            controlLock = false;
            dashTimeLeft = -1f;
        }
        if (dashCooldown > 0f)
        {
            dashCooldown -= Time.deltaTime;
            dashText.text = "Dash: " + MathF.Round(dashCooldown, 1) + "s";
        }
        else
        {
            dashCooldown = 0f;
            dashText.text = "Dash: Ready!";
        }

        

        // Double jump handler
        if (isGrounded)
        {
            if (jumpRefreshCooldown <= 0f)
            {
                jumpsLeft = totalJumps;
                jumpRefreshCooldown = initJumpRefreshCooldown;
            }
            else { jumpRefreshCooldown -= Time.deltaTime; }
        }

        // Attack handler
        if (Input.GetButtonDown("Attack") && attackTimeLeft <= 0 && attackCooldownLeft <= 0)
        {
            myAnim.SetTrigger("Attack");
            attackRange.enabled = true;
            attackCooldownLeft = attackCooldown;
            attackTimeLeft = attackDuration;

        }
        if (attackTimeLeft > 0f)
        {
            attackTimeLeft -= Time.deltaTime;
        }
        else
        {
            attackTimeLeft = 0f;
            attackRange.enabled = false;
            attackCooldownLeft = attackCooldown;
        }
        if (attackCooldownLeft > 0f)
        {
            attackCooldownLeft -= Time.deltaTime;
            if (attackCooldownLeft < 0f) attackCooldownLeft = 0f;
        }

        // Softlock prevention
        if (transform.position.y < YLIMIT) { Kill(); }

        // Animator handler
        myAnim.SetFloat("SpeedX", Math.Abs(myRigidbody.velocity.x));
        myAnim.SetFloat("SpeedY", myRigidbody.velocity.y);
        myAnim.SetBool("Grounded", isGrounded);

        // Health handler
        if (currentHealth <= 0) Kill();
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        healthDisplay.text = "Health: " + currentHealth;

    }

    public void Kill()
    {
        Vector3 spawnpoint = GameObject.FindGameObjectWithTag("PlayerSpawn").transform.position;
        transform.position = spawnpoint;
        myRigidbody.velocity = Vector2.zero;
        currentHealth = maxHealth;
    }

    public void Damage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;
    }

    public void ToggleLockControls() { controlLock = !controlLock; }

    public void LockControls() { controlLock = true; }

    public void UnlockControls() { controlLock = false; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerCanDamage"))
        {
            collision.GetComponent<PlayerDamageable>().Damage(attackDamage);
        }
    }
}
