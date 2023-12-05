using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayText : MonoBehaviour
{
    [TextArea]
    public String textToDisplay;
    private Text textElement;
    private int position;
    public float displaySpeed;
    private float advanceCooldown;
    // Start is called before the first frame update
    void Start()
    {
        position = textToDisplay.Length;
        textElement = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (position < textToDisplay.Length)
        {
            if (advanceCooldown > 0) { advanceCooldown -= Time.deltaTime; }
            else
            {
                textElement.text += textToDisplay[position++];
                advanceCooldown = displaySpeed;
            }
        }
    }

    public void startCreditsRoll()
    {
        ClearText();
        position = 0;
    }

    public void ClearText()
    {
        textElement = GetComponent<Text>();
        textElement.text = "";
        position = textToDisplay.Length;
    }
}
