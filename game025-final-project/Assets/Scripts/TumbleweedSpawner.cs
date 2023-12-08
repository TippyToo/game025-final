using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TumbleweedSpawner : MonoBehaviour
{
    public GameObject tumbleweedPrefab;
    public float spawnFrequency;
    private PlayerController player;
    public float distanceToActivate;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(player.transform.position.x - transform.position.x) >= distanceToActivate)
        {
            if (Random.value < spawnFrequency)
            {
                Instantiate(tumbleweedPrefab, transform.position, new Quaternion());
            }
        }
    }
}
