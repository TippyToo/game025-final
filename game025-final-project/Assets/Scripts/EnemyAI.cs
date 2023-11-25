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

    [Tooltip("Check this box if the sprite begins facing to the right")]
    public bool facingRight;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        wallCheck = transform.Find("WallDetector");
        wallCheckSensitivity = wallCheck.GetComponent<CircleCollider2D>().radius;
    }

    // Update is called once per frame
    void Update()
    {
        if (facingRight) transform.localScale = new Vector2(-1, 1);
        else transform.localScale = new Vector2(1, 1);

        myRigidbody.velocity = new Vector2(moveSpeed * ModifierOf(facingRight), myRigidbody.velocity.y);
        if (Physics2D.OverlapCircle(wallCheck.position, wallCheckSensitivity, whatIsGround)) ChangeDirection();

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
}
