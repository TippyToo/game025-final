using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlane : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) { collision.GetComponent<PlayerController>().Kill(); }
        else { Destroy(collision.gameObject); }
    }
}
