using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    private bool paused;
    private PlayerController player;
    public float gameTimeScale;
    public GameObject settingsMenu;
  
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
        }
        else
        {
            Time.timeScale = gameTimeScale;
        }
        pauseMenu.SetActive(paused);

        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            TogglePause(); 
            settingsMenu.SetActive(false);
        }
    }

    public void TogglePause() 
    {
        player.ToggleLockControls();
        paused = !paused;
    }

    public void Pause()
    {
        player.LockControls();
        paused = true; 
    }

    public void Resume()
    {
        player.UnlockControls();
        paused = false; 
    }

    public bool IsPaused() { return paused; }

    public void Settings()
    {
        settingsMenu.SetActive(!settingsMenu.activeSelf);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
