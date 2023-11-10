using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Movement variables
    
    [Tooltip("How fast the character moves")]
    public float moveSpeed;
    [Tooltip("How high the character jumps")]
    public float jumpStrength;
    [Tooltip("The transform used to denote the positioning of the ground check")]
    public Transform groundCheck;
    [Tooltip("How close the groundcheck has to be to the ground to qualify as grounded")]
    public float groundCheckSensitivty;
    private bool isGrounded;
    public float jumpRefreshCooldown;
    public float initJumpRefreshCooldown;
    private int jumpsLeft;
    public int totalJumps;

    // Utility variables
    private Rigidbody2D myRigidbody;
    public LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        initJumpRefreshCooldown = jumpRefreshCooldown;
        if (moveSpeed <= 0) { Debug.LogWarning("character moveSpeed is set to 0 or less"); }
        if (jumpStrength <= 0) { Debug.LogWarning("character jumpStrength is set to 0 or less"); }

    }

    // Update is called once per frame
    void Update()
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

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckSensitivty, groundLayer);
        if (isGrounded)
        {
            if (jumpRefreshCooldown <= 0) { jumpsLeft = totalJumps; }
            else { jumpRefreshCooldown -= Time.deltaTime; }
        }
        else { jumpRefreshCooldown = initJumpRefreshCooldown; }
        if (Input.GetButtonDown("Jump") && jumpsLeft > 0)
        {
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpStrength);
            jumpsLeft--;
        }


        // if jumps:
        //      if isGrounded:
        //          jump
        //      else if !hasDoubleJumped:
        //          jump
        //          hasDoubleJumped = true
        //      
    }
}
