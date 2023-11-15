using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoundsMessage : MonoBehaviour
{
    [Tooltip("Text Element to display message in")]
    public Text textDisplay;

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player"))
    //    {
    //        textDisplay.enabled = true;
    //        Debug.Log("on-player");
    //    }
    //    Debug.Log("on");
    //}

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.transform.CompareTag("Player")) textDisplay.enabled = false;
    //    Debug.Log("off");
    //}

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            textDisplay.enabled = true;
        }
    }

    //private void FixedUpdate()
    //{
    //    textDisplay.enabled = false;
    //}
}
