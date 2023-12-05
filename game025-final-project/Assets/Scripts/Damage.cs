using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [Tooltip("Amount to damage player")]
    public int damageAmount;
    private PlayerController player;
    private Rigidbody2D playerBody;
    public bool doKnockback = true;
    public float knockbackTime;
    private float knockbackTimeLeft;
    public float knockbackSpeed;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        playerBody = player.GetComponent<Rigidbody2D>();
        knockbackTimeLeft = knockbackTime;
    }

    void Update()
    {
        if (doKnockback)
        {
            if (knockbackTimeLeft > 0) 
            {
                player.LockControls();
                if (PlayerToRight())
                {
                    playerBody.velocity = new Vector2(knockbackSpeed, knockbackSpeed / 2);
                }
                else
                {
                    playerBody.velocity = new Vector2(-knockbackSpeed, knockbackSpeed / 2);
                }
                knockbackTimeLeft -= Time.deltaTime;
                if (knockbackTime < 0) 
                {
                    player.UnlockControls();
                }
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player") && player.isAlive && !player.controlLock)
        {
            other.gameObject.GetComponent<PlayerController>().Damage(damageAmount);
        }
    }

    private bool PlayerToRight()
    {
        return (player.transform.position.x > transform.position.x);
    }
}
