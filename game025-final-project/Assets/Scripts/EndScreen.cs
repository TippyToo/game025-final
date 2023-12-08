using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    private Fadein fader;
    // Start is called before the first frame update
    void Start()
    {
        fader = FindObjectOfType<Fadein>();  
        fader.FadeToEmpty();
    }

    
}
