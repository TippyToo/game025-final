using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum AI_Type { AggroPlayer, RandomMovement }
    public AI_Type aIType;
    public float moveSpeed;
    private Rigidbody2D myRigidbody;
    private Transform wallCheck;
    public float wallCheckSensitivity;
    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;
    private PlayerController player;
    public bool chasePlayer;
    public float aggroRadius;
    [Tooltip("Check this box if the sprite begins facing to the right")]
    public bool facingRight;
    // Start is called before the first frame update
    void Start()
    {
        aggroRadius = GetComponent<CircleCollider2D>().radius;
        player = GameObject.FindObjectOfType<PlayerController>();
        myRigidbody = GetComponent<Rigidbody2D>();
        wallCheck = transform.Find("WallDetector");
        wallCheckSensitivity = wallCheck.GetComponent<CircleCollider2D>().radius;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector2(ModifierOf(facingRight) * -1, 1);

        if (chasePlayer) facingRight = PlayerToRight();

        //if (PlayerToRight()) Debug.Log("Player to right"); else Debug.Log("Player to left");

        myRigidbody.velocity = new Vector2(moveSpeed * ModifierOf(facingRight), myRigidbody.velocity.y);
        if (Physics2D.OverlapCircle(wallCheck.position, wallCheckSensitivity, whatIsGround) && aIType == AI_Type.RandomMovement) ChangeDirection();
        if (Physics2D.OverlapCircle(transform.position, aggroRadius, whatIsPlayer) && aIType == AI_Type.AggroPlayer && player.isAlive) chasePlayer = true;

    }

    private int ModifierOf(bool q)
    {
        if (q) return 1;
        else return -1;
    }

    private void ChangeDirection()
    {
        facingRight = !facingRight;
    }

    private bool PlayerToRight()
    {
        return player.transform.position.x > transform.position.x;
    }
}
