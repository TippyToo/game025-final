using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    private bool paused;
    private PlayerController player;
    public float gameTimeScale;
  
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (paused)
        {
            Time.timeScale = 0f;
            player.LockControls();
        }
        else
        {
            Time.timeScale = gameTimeScale;
            player.UnlockControls();
        }
        pauseMenu.SetActive(paused);

        if (Input.GetKeyDown(KeyCode.Escape)) { TogglePause(); }
    }

    public void TogglePause() { paused = !paused; }

    public void Pause() { paused = true; }

    public void Resume() { paused = false; }

    public bool IsPaused() { return paused; }
}
