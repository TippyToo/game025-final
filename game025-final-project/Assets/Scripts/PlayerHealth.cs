using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private PlayerController player;
    private Transform red;
    private Transform green;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        red = transform.Find("Red");
        green = transform.Find("Green");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
