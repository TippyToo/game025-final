using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [Tooltip("Amount to damage player")]
    public int damageAmount;
    private PlayerController player;
    public bool doKnockback = true;
    public float knockbackTime;
    public float knockbackSpeed;

    void Start()
    {
        player = GetComponent<PlayerController>();
    }

    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Player")) other.gameObject.GetComponent<PlayerController>().Damage(damageAmount);
    }
}
