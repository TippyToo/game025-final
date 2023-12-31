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
    public float easymodeMoveSpeedBonus = 0.5f;

    // Jump variables
    [Tooltip("How high the character jumps")]
    public float jumpStrength;
    [Tooltip("The transform used to denote the positioning of the ground check")]
    public Transform groundCheck;
    [Tooltip("How close the groundcheck has to be to the ground to qualify as grounded")]
    public float groundCheckSensitivty;
    private bool isGrounded;
    [Tooltip("How long after jumping before jumps can refresh. Used for ground check leniency to prevent unintended double jumps")]
    public float jumpRefreshCooldown;
    //private float initJumpRefreshCooldown;
    //private int jumpsLeft;
    //[Tooltip("How many jumps the character can perform before needing to land")]
    //public int totalJumps;
    [Tooltip("What layer is counted as ground, for the purpose of determining ground check")]
    public LayerMask groundLayer;
    public LayerMask jumpLayer;
    public bool hasBoots;
    private bool hasDoubleJump;

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
    public bool unlockedDash;
    //[Tooltip("Text field to display dash status in")]
    //public Text dashText;

    // Attack variables
    public CapsuleCollider2D attackRange;
    public float attackCooldown;
    private float attackCooldownLeft;
    public float attackDuration;
    public int attackDamage;
    public Transform attackLocation;
    private float attackTimeLeft;
    public bool canAttackMidair;

    // Animation variables
    private Animator myAnim;

    // Health variables
    [Tooltip("How much health the player currently has")]
    public int currentHealth;
    [Tooltip("Maximum health the player can have")]
    public int maxHealth;
    [Tooltip("Text Element to display health in")]
    //public Text healthDisplay;
    //public GameObject healthBar;
    public HealthBar hp;

    // Death variables
    public GameObject head;
    public Transform headLocation;
    public bool isAlive;
    public float respawnDelay;
    private Transform respawnLocation;
    public Vector2 headFlyVariety;

    // Utility variables
    private Rigidbody2D myRigidbody;
    public bool controlLock = false;
    private enum direction { Left, Right } 
    private direction facing;
    private static float YLIMIT = -100;
    public CapsuleCollider2D mainCollider;
    public CapsuleCollider2D deadCollider;
    private FlashSprite spriteFlasher;
    private LevelManager levelManager;

    // Sound variables
    public AudioSource footstepSource;
    public AudioSource gameSource;
    public AudioClip attackWhif;
    public AudioClip bootsActivate;

    void Start()
    {
        //healthBar = Instantiate(healthBar, new Vector3(transform.position.x - 0.12f, transform.position.y + 0.5f, 0f), new Quaternion(), transform);
        //hp = healthBar.GetComponent<HealthBar>();
        hp.maxHealth = maxHealth;
        hp.currentHealth = currentHealth;
        respawnLocation = GameObject.FindGameObjectWithTag("PlayerSpawn").transform;
        isAlive = true;
        attackCooldownLeft = attackCooldown;
        attackRange.enabled = false;
        myRigidbody = GetComponent<Rigidbody2D>();
        spriteFlasher = GetComponent<FlashSprite>();
        levelManager = FindObjectOfType<LevelManager>();
        footstepSource = GetComponent<AudioSource>();
        gameSource = levelManager.GetComponent<AudioSource>();
        //initJumpRefreshCooldown = jumpRefreshCooldown;
        initDashCooldown = dashCooldown;
        dashCooldown = 0f;
        myAnim = GetComponent<Animator>();
        if (moveSpeed <= 0) { Debug.LogWarning("character moveSpeed is set to 0 or less"); }
        if (jumpStrength <= 0) { Debug.LogWarning("character jumpStrength is set to 0 or less"); }
        if (PlayerPrefs.GetInt("Difficulty") == 0) moveSpeed += easymodeMoveSpeedBonus;

    }

    void Update()
    {
        if (transform.localScale.x < 0) facing = direction.Left; else facing = direction.Right;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckSensitivty, groundLayer);
        bool onSurface = Physics2D.OverlapCircle(groundCheck.position, groundCheckSensitivty, jumpLayer);
        isGrounded = isGrounded || onSurface;
        myAnim.SetBool("Dashing", dashTimeLeft > 0);
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
            if (Input.GetButtonDown("Jump"))
            {
                if (isGrounded)
                {
                    myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpStrength);
                    myAnim.SetTrigger("Jump");
                }
                else if (hasBoots && hasDoubleJump)
                {
                    myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpStrength);
                    myAnim.SetTrigger("DoubleJump");
                    gameSource.PlayOneShot(bootsActivate);
                    hasDoubleJump = false;
                }
            }

            // Dash
            if (Input.GetButtonDown("Dash") && dashTimeLeft == -1 && dashCooldown <= 0 && unlockedDash)
            {
                dashTimeLeft = dashTime;
                dashCooldown = initDashCooldown;
            }

            // Force Kill
            if (Input.GetKeyDown(KeyCode.RightBracket) && isAlive)
            {
                currentHealth = 0;
                Kill();
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
        }
        else
        {
            dashCooldown = 0f;
        }

        

        // Double jump handler
        if (isGrounded)
        {
            hasDoubleJump = true;
        }

        // Attack handler
        if (Input.GetButtonDown("Attack") && attackTimeLeft <= 0 && attackCooldownLeft <= 0 && (canAttackMidair || isGrounded) && !controlLock)
        {
            myAnim.SetTrigger("Attack");
            attackRange.enabled = true;
            attackCooldownLeft = attackCooldown;
            attackTimeLeft = attackDuration;
            gameSource.PlayOneShot(attackWhif);

        }
        if (attackTimeLeft > 0f)
        {
            attackTimeLeft -= Time.deltaTime;
            if (attackTimeLeft < 0f) attackTimeLeft = 0f;
        }
        else if (attackTimeLeft == 0f)
        {
            attackTimeLeft = -1f;
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
        myAnim.SetBool("Alive", isAlive);

        // Health handler
        if (currentHealth <= 0 && isAlive) Kill();
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        hp.currentHealth = currentHealth;
        hp.maxHealth = maxHealth;

        // Footstep handler
        if (Math.Abs(myRigidbody.velocity.x) > 0.1 && isGrounded && !footstepSource.isPlaying)
        {
            footstepSource.Play();
        }
        if (Math.Abs(myRigidbody.velocity.x) <= 0.1 || !isGrounded)
        {
            footstepSource.Stop();
        }

    }

    public void Kill()
    {
        isAlive = false;
        //myRigidbody.constraints = RigidbodyConstraints2D.FreezePositionX;
        LockControls();
        myAnim.SetTrigger("Die");
        Rigidbody2D newHead = Instantiate(head, headLocation.position, Quaternion.identity).GetComponent<Rigidbody2D>();
        newHead.velocity = new Vector2(UnityEngine.Random.Range(-headFlyVariety.x, headFlyVariety.x), headFlyVariety.y);
        deadCollider.enabled = true;
        mainCollider.enabled = false;

        // De-aggro all enemies
        EnemyAI[] enemies = GameObject.FindObjectsOfType<EnemyAI>();
        foreach (EnemyAI enemy in enemies)
        {
            enemy.chasePlayer = false;
        }

        StartCoroutine(RespawnAfterSeconds(respawnDelay));
        
    }

    public IEnumerator RespawnAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        transform.position = respawnLocation.position;
        transform.localScale = new Vector2(1, 1);
        myRigidbody.velocity = Vector2.zero;
        currentHealth = maxHealth;
        myAnim.SetTrigger("Respawn");
        UnlockControls();
        isAlive = true;
        mainCollider.enabled = true;
        deadCollider.enabled = false;
        yield return 0;
    }

    public void Damage(int damage)
    {
        if (!spriteFlasher.shouldFlash)
        {
            currentHealth -= damage;
            if (currentHealth < 0) currentHealth = 0;
            spriteFlasher.StartFlash();
        }
    }

    public void ToggleLockControls() { controlLock = !controlLock; }

    public void LockControls() { controlLock = true; }

    public void UnlockControls() { controlLock = false; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerCanDamage"))
        {
            collision.GetComponent<PlayerDamageable>().Damage(attackDamage);
            Debug.Log("damaged enemy");
        }
    }

    public void GiveSwordUpgrade()
    {
        attackDamage++;
        attackCooldown -= 0.1f;

    }

    public void GiveHealthUpgrade()
    {

    }

    public void ReplenishDoubleJump() { hasDoubleJump = true; }
}
