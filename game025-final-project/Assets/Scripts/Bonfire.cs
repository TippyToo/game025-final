using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonfire : Interactable
{
    public Transform playerSpawn;
    public Animator fireAnim;
    // Update is called once per frame
    void Update()
    {
        fireAnim.SetBool("inRange", inRange);
        if (Input.GetButtonDown("Interact") && inRange && active)
        {
            player.currentHealth = player.maxHealth;
            playerSpawn.position = transform.position;
        }
    }
}
