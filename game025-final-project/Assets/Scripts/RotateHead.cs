using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateHead : MonoBehaviour
{
    public float rotationSpeed;
    private Rigidbody2D head;
    // Start is called before the first frame update
    void Start()
    {
        head = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        head.rotation += rotationSpeed;
    }
}
