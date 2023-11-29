using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitHazard : MonoBehaviour
{
    private Transform respawnAt;
    [Tooltip("Amount to damage player when falling in")]
    public int damageAmount;
    // Start is called before the first frame update
    void Start()
    {
        respawnAt = transform.Find("Respawn");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            PlayerController player = collision.transform.GetComponent<PlayerController>();
            if (player.currentHealth > 1)
            {
                collision.transform.position = respawnAt.position;
                collision.transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
            player.Damage(damageAmount);
        }
    }
}
