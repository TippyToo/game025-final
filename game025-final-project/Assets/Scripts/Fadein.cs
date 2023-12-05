using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fadein : MonoBehaviour
{
    private Animator fadeAnim;
    public float secs;
    // Start is called before the first frame update
    void Start()
    {
        fadeAnim = GetComponent<Animator>();
        StartCoroutine(WaitThenFadeIn());
    }

    public void FadeToBlack()
    {
        fadeAnim.SetTrigger("FadeToBlack");
    }

    public void FadeToEmpty()
    {
        fadeAnim.SetTrigger("FadeToEmpty");
    }

    public void InstantFadeBlack()
    {
        fadeAnim.SetTrigger("InstantFadeBlack");
    }

    public void InstantFadeEmpty()
    {
        fadeAnim.SetTrigger("InstantFadeEmpty");
    }

    IEnumerator WaitThenFadeIn()
    {
        yield return new WaitForSeconds(secs);
        FadeToEmpty();
        yield return 0;
    }
}
