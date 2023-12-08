using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tumbleweed : MonoBehaviour
{
    public float speed;
    private Rigidbody2D myRigidbody;
    public float lifetime;
    private float timeAlive;
    public float rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        timeAlive += Time.deltaTime;
        if (timeAlive > lifetime) { Destroy(gameObject); }
        myRigidbody.velocity = new Vector2(-speed, myRigidbody.velocity.y);
        myRigidbody.rotation += rotationSpeed;
    }
}
