using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamageable : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public GameObject healthBar;
    private HealthBar hp;
    public AudioClip attackHit;
    public float attackHitVolume;
    public LevelManager levelManager;
    // Start is called before the first frame update
    void Start()
    {
        transform.tag = "PlayerCanDamage";
        currentHealth = maxHealth;
        healthBar = Instantiate(healthBar, new Vector3(transform.position.x, transform.position.y + 0.5f, 0f), new Quaternion(), transform);
        hp = healthBar.GetComponent<HealthBar>();
        hp.maxHealth = maxHealth;
        hp.currentHealth = currentHealth;
        levelManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        hp.currentHealth = currentHealth;
        if (currentHealth <= 0) Kill();
        
    }

    public void Damage(int damage)
    {
        levelManager.GetComponent<AudioSource>().PlayOneShot(attackHit, attackHitVolume);
        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;
    }

    public void Kill()
    {
        Destroy(gameObject);
    }
}
