using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flicker : MonoBehaviour
{
    private Light lightElement;
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            lightElement = GetComponent<Light>();
        } 
        catch (System.Exception) 
        {
            Debug.LogError("Object must have a light component");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
