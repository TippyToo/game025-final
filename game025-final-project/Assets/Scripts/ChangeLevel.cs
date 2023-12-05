using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLevel : MonoBehaviour
{
    private PlayerController player;
    private Fadein fader;
    public float timeBetweenStages;
    public Transform citySpawn;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        fader = FindObjectOfType<Fadein>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backslash)) { ForceNextStage(); }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ForceNextStage();
        }
    }

    public void ForceNextStage()
    {
        fader.FadeToBlack();
        player.LockControls();
        StartCoroutine(ToNextStage());
    }

    IEnumerator ToNextStage()
    {
        yield return new WaitForSeconds(0.517f);
        StartCoroutine(WaitNextStage());
        player.transform.position = citySpawn.position;
        yield return 0;
    }

    IEnumerator WaitNextStage()
    {
        yield return new WaitForSeconds(timeBetweenStages);
        fader.FadeToEmpty();
        StartCoroutine(EnterNextStage());
        yield return 0;
    }

    IEnumerator EnterNextStage()
    {
        yield return new WaitForSeconds(0.517f);
        player.UnlockControls();
        yield return 0;
    }
}
