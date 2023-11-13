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
    private Vector2 dashInitVelocity;
    [Tooltip("Text field to display dash status in")]
    public Text dashText;

    // Utility variables
    private Rigidbody2D myRigidbody;
    private bool controlLock = false;
    private enum direction { Left, Right } 
    private direction facing;
    private static float YLIMIT = -200;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        initJumpRefreshCooldown = jumpRefreshCooldown;
        initDashCooldown = dashCooldown;
        dashCooldown = 0f;
        if (moveSpeed <= 0) { Debug.LogWarning("character moveSpeed is set to 0 or less"); }
        if (jumpStrength <= 0) { Debug.LogWarning("character jumpStrength is set to 0 or less"); }

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.x < 0) facing = direction.Left; else facing = direction.Right;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckSensitivty, groundLayer);

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
            dashInitVelocity = myRigidbody.velocity;
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
            myRigidbody.velocity = dashInitVelocity;
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

        // Softlock prevention
        if (transform.position.y < YLIMIT) { Kill(); }

    }

    public void Kill()
    {
        Vector3 spawnpoint = GameObject.FindGameObjectWithTag("PlayerSpawn").transform.position;
        transform.position = spawnpoint;
        myRigidbody.velocity = Vector2.zero;
    }
}
