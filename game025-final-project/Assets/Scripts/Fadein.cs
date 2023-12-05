using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fadein : MonoBehaviour
{
    private Animator fadeAnim;
    // Start is called before the first frame update
    void Start()
    {
        fadeAnim = GetComponent<Animator>();
        FadeToEmpty();
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
}
