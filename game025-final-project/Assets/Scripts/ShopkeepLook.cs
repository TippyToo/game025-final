using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopkeepLook : MonoBehaviour
{
    private PlayerController player;
    private SpriteRenderer spriteRenderer;
    private enum Facing { Left, Forward , Right}
    //private Facing facing = Facing.Forward;
    public Sprite[] sprites = new Sprite[3];
    public float distanceForFront;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.sprite = sprites[Convert.ToInt32(DirectionToFace())];
    }

    private Facing DirectionToFace()
    {
        if (Mathf.Abs(player.transform.position.x - transform.position.x) < distanceForFront) return Facing.Forward;
        else if (player.transform.position.x > transform.position.x) return Facing.Right;
        else return Facing.Left;
    }
}
