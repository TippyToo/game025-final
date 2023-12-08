using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToEnd : MonoBehaviour
{
    private Fadein fader;

    private void Start()
    {
        fader = FindObjectOfType<Fadein>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            fader.FadeToBlack();
            StartCoroutine(WaitThenLoad());
        }
    }

    IEnumerator WaitThenLoad()
    {
        yield return new WaitForSeconds(0.517f);
        SceneManager.LoadScene(2);
        yield return 0;
    }
}
