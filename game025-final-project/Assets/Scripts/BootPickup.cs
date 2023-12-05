using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootPickup : Interactable
{
    public GameObject bootIcon;
    void Update()
    {
        if (Input.GetButtonDown("Interact") && inRange && active)
        {
            InteractAction();
        }
    }
    public void InteractAction()
    {
        player.hasBoots = true;
        bootIcon.SetActive(true);
        Destroy(gameObject);
    } 
}
