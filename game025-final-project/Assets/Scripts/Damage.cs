using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [Tooltip("Amount to damage player")]
    public int damageAmount;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Player")) other.gameObject.GetComponent<PlayerController>().Damage(damageAmount);
    }
}
