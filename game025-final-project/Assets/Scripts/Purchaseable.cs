using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Purchaseable : Interactable
{
    public GameObject HUDIcon;
    public Sprite spriteIfSwitching;
    public LevelManager levelManager;
    public enum UpgradeType { Dash, Sword, Health }
    public int price;
    public GameObject priceText;
    public UpgradeType upgrade;
    // Start is called before the first frame update
     void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }
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
    }
}
