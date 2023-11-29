using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    [Tooltip("How much constant force to bounce the player with")]
    public float bounceStrength;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Rigidbody2D player = collision.GetComponent<Rigidbody2D>();
            player.velocity = new Vector2(player.velocity.x, bounceStrength);
        }
    }
}
