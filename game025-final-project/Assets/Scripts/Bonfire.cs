using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonfire : Interactable
{
    public Transform playerSpawn;
    public Animator fireAnim;
    public Fadein fader;
    private bool gotAnim = false;

    // Update is called once per frame
    void Update()
    {
        if (!gotAnim)
        {
            fireAnim = GetComponent<Animator>();
            gotAnim = true;
        }
        if (fader == null) { fader = FindObjectOfType<Fadein>(); }
        fireAnim.SetBool("inRange", inRange);
        if (Input.GetButtonDown("Interact") && inRange && active)
        {
            player.LockControls();
            fader.FadeToBlack();
            StartCoroutine(WaitThenFade());
        }
    }

    IEnumerator WaitThenFade()
    {
        yield return new WaitForSeconds(1f);
        player.currentHealth = player.maxHealth;
        playerSpawn.position = transform.position;
        fader.FadeToEmpty();
        StartCoroutine(WaitThenUnlock());
        yield return 0;
    }

    IEnumerator WaitThenUnlock()
    {
        yield return new WaitForSeconds(0.517f);
        player.UnlockControls();
        yield return 0;
    }
}
