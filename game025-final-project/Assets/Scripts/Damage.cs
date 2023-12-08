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
    public float knockbackTimeLeft;
    public float knockbackSpeed;
    private FlashSprite spriteFlasher;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        playerBody = player.GetComponent<Rigidbody2D>();
        spriteFlasher = player.GetComponent<FlashSprite>();
    }

    void Update()
    {
        if (doKnockback)
        {
            if (knockbackTimeLeft > 0f) 
            {
                player.LockControls();
                if (PlayerToRight())
                {
                    playerBody.velocity = new Vector2(knockbackSpeed, knockbackSpeed / 2f);
                }
                else
                {
                    playerBody.velocity = new Vector2(-knockbackSpeed, knockbackSpeed / 3f);
                }
                knockbackTimeLeft -= Time.deltaTime;
                if (knockbackTimeLeft < 0f && player.isAlive) 
                {
                    Debug.Log("controls unlocked");
                    player.UnlockControls();
                }
            }
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Player") && player.isAlive && !player.controlLock && !spriteFlasher.shouldFlash)
        {
            player.Damage(damageAmount);
            if (doKnockback && player.currentHealth > damageAmount) knockbackTimeLeft = knockbackTime;
        }
    }

    private bool PlayerToRight()
    {
        return (player.transform.position.x > transform.position.x);
    }
}
