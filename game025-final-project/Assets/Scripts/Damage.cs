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
    public float knockbackSpeed;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        playerBody = player.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().Damage(damageAmount);
        }
    }
}
