using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamageable : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public Text healthDisplay;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthDisplay.text = currentHealth.ToString();
    }

    public void Damage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;
    }
}
