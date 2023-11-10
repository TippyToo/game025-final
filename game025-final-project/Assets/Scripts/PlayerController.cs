using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Public variables
    [Tooltip("How fast the character moves")]
    public float moveSpeed;
    [Tooltip("How high the character jumps")]
    public float jumpStrength;

    // Private variables
    private Rigidbody2D myRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Horizontal movement
        if (Input.GetAxis("Horizontal") < 0) // Left
        {
            myRigidbody.velocity = new Vector2(-moveSpeed * Time.deltaTime, myRigidbody.velocity.y);
            Debug.Log("moving left");
        }

        if (Input.GetAxis("Horizontal") > 0) // Right
        {
            myRigidbody.velocity = new Vector2(moveSpeed * Time.deltaTime, myRigidbody.velocity.y);
            Debug.Log("moving right");
        }
    }
}
