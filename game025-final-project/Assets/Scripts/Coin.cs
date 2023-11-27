using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int value;
    public float jumpAmount;
    private Rigidbody2D myRigidbody;
    private bool collected;
    private Vector3 initPosition;
    private LevelManager levelManager;
    // Start is called before the first frame update
    void Start()
    {
        initPosition = transform.position;
        myRigidbody = GetComponent<Rigidbody2D>();
        levelManager = GameObject.FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < initPosition.y)
        {
            levelManager.addCoins(value);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collected)
        {
            collected = true;
            myRigidbody.isKinematic = false;
            myRigidbody.velocity = new Vector2(0, jumpAmount);
        }
    }
}
