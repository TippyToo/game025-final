using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonfire : Interactable
{
    public Transform playerSpawn;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interact") && inRange && active)
        {
            player.currentHealth = player.maxHealth;
            playerSpawn.position = transform.position;
        }
    }
}
