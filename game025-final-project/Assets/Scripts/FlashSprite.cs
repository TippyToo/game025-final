using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashSprite : MonoBehaviour
{
    public float flashSpeed = 0.1f;
    private float timeSinceFlash;
    private SpriteRenderer spriteRenderer;
    public float flashTime;
    public bool shouldFlash;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldFlash)
        {
            if (timeSinceFlash >= flashSpeed)
            {
                ToggleSprite();
                timeSinceFlash = 0f;
            }
            else timeSinceFlash += Time.deltaTime;
        }
        else if (timeSinceFlash != 0f) { timeSinceFlash = 0f; }
    }

    private void ToggleSprite()
    {
        Debug.Log("ToggleFlash called");
        spriteRenderer.enabled = !spriteRenderer.enabled;
        //if (spriteRenderer.sprite != baseSprite) spriteRenderer.sprite = baseSprite;
        //else spriteRenderer.sprite = null;
    }

    public void StartFlash()
    {
        Debug.Log("StartFlash called");
        spriteRenderer.enabled = false;
        shouldFlash = true;
        StartCoroutine(EndFlash());
    }

    IEnumerator EndFlash()
    {
        yield return new WaitForSecondsRealtime(flashTime);
        Debug.Log("EndFlash called");
        shouldFlash = false;
        spriteRenderer.enabled = true; ;
        yield return 0;
    }
}
