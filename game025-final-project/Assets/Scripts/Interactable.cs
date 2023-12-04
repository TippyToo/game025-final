using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour 
{
    public float interactRadius;
    public Text interactText;
    private SpriteRenderer spriteRenderer;
    private Sprite baseSprite;
    public Sprite interactSprite;
    //public String interactKey = "E";
    private CircleCollider2D circleCollider;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        baseSprite = spriteRenderer.sprite;
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
        HingeJoint2D joint2D = GetComponent<HingeJoint2D>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Interact")) { InteractAction(); }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            spriteRenderer.sprite = interactSprite;
            if (interactText != null) interactText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            spriteRenderer.sprite = baseSprite;
            if (interactText != null) interactText.gameObject.SetActive(false);
        }
    }

    
    public void InteractAction() { }
}
