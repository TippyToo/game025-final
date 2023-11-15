using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitHazard : MonoBehaviour
{
    private Vector2 respawnAt;
    [Tooltip("Amount to damage player when falling in")]
    public int damageAmount;
    // Start is called before the first frame update
    void Start()
    {
        respawnAt = transform.Find("Respawn").position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.GetComponent<PlayerController>().Damage(damageAmount);
            collision.transform.position = respawnAt;
            collision.transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
}
