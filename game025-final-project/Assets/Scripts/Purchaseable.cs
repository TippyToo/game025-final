using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Purchaseable : Interactable
{
    public GameObject HUDIcon;
    public Sprite spriteIfSwitching;
    public enum UpgradeType { Dash, Sword, Health }
    public int price;
    public GameObject signIcon;
    public UpgradeType upgrade;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interact") && inRange && active)
        {
            if (levelManager.coins >= price)
            {
                levelManager.coins -= price;
                PurchaseUpgrade();
            }
        }
    }

    private void PurchaseUpgrade()
    {
        switch (Convert.ToInt32(upgrade))
        {
            case 0:
                player.unlockedDash = true;
                HUDIcon.SetActive(true);
                break;
            case 1:
                player.GiveSwordUpgrade();
                HUDIcon.GetComponent<Image>().sprite = spriteIfSwitching;
                break;
            case 2:
                player.GiveHealthUpgrade();
                break;
        }
        active = false;
        interactText.SetActive(false);
        spriteShow.sprite = baseSprite;
        signIcon.SetActive(false);
    }
}
