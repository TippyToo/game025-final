using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public int maxHealth = 1;
    public int currentHealth = 1;
    private Transform green;
    private Transform red;
    private RectTransform greenRect;
    private RectTransform redRect;
    public Vector3 greenPos;
    private float initScale;
    public bool forPlayer;
    // Start is called before the first frame update
    void Start()
    {
        if (forPlayer)
        {
            greenRect = (RectTransform)transform.Find("Green");
            redRect = (RectTransform)transform.Find("Red");
            initScale = greenRect.localScale.x;
        }
        else
        {
            green = transform.Find("Green");
            red = transform.Find("Red");
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth < 0) currentHealth = 0;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        if (maxHealth == 0) maxHealth = 1;

        float currentHealthPercent = currentHealth / (float) maxHealth;
        if (forPlayer)
        {
            greenPos = greenRect.localPosition;
            greenRect.localScale = new Vector3(currentHealthPercent * initScale, initScale, initScale);
            //greenRect.localPosition = new Vector3(PosFromScale(currentHealthPercent), redRect.localPosition.y, redRect.localPosition.z);
        }
        else
        {
            green.localScale = new Vector3(currentHealthPercent, 1, 0);
            green.position = new Vector3(red.position.x + PosFromScale(currentHealthPercent), red.position.y, 0);
        }
    }

    private float PosFromScale(float x)
    {
        return 0.5f * x - 0.5f;
    }
}
