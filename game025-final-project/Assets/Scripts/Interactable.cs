using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour 
{
    public float interactRadius;
    public GameObject interactText;
    public SpriteRenderer spriteShow;
    public Sprite baseSprite;
    public Sprite interactSprite;
    //public String interactKey = "E";
    private CircleCollider2D circleCollider;
    public PlayerController player;
    public LevelManager levelManager;
    public bool inRange;
    public bool active;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        spriteShow = GetComponent<SpriteRenderer>();
        baseSprite = spriteShow.sprite;
        levelManager = FindObjectOfType<LevelManager>();
        try
        {
            circleCollider = GetComponent<CircleCollider2D>();
        }
        catch (System.Exception)
        {
            circleCollider = gameObject.AddComponent<CircleCollider2D>();
            circleCollider.radius = interactRadius;
            circleCollider.isTrigger = true;
        }
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (active && collision.CompareTag("Player"))
        {
            inRange = true;
            spriteShow.sprite = interactSprite;
            if (interactText != null) interactText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (active && collision.CompareTag("Player"))
        {
            inRange = false;
            spriteShow.sprite = baseSprite;
            if (interactText != null) interactText.SetActive(false);
        }
    }


    //public void InteractAction();
}
