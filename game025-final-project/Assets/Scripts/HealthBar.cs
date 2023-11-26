using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public int maxHealth = 1;
    public int currentHealth = 1;
    private Transform green;
    private Transform red;
    // Start is called before the first frame update
    void Start()
    {
        green = transform.Find("Green");
        red = transform.Find("Red");

    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth < 0) currentHealth = 0;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        if (maxHealth == 0) maxHealth = 1;

        float currentHealthPercent = currentHealth / (float) maxHealth;
        green.localScale = new Vector3(currentHealthPercent, 1, 0);
        green.position = new Vector3(red.position.x + PosFromScale(currentHealthPercent), red.position.y, 0);
    }

    private float PosFromScale(float x)
    {
        return 0.5f * x - 0.5f;
    }
}
