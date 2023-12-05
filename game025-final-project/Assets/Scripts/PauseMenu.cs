using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    private bool paused;
    private PlayerController player;
    public float gameTimeScale;
    public GameObject settingsMenu;
    public float timeToMenu = 0.517f;
    private Fadein fader;
  
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        fader = FindObjectOfType<Fadein>();
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
        settingsMenu.transform.Find("Volume").Find("Volume Slider").GetComponent<Slider>().value = PlayerPrefs.GetFloat("Volume");
    }

    public void Quit()
    {
        Resume();
        fader.FadeToBlack();
        StartCoroutine(LoadMenu());
    }

    IEnumerator LoadMenu()
    {
        yield return new WaitForSeconds(timeToMenu);
        SceneManager.LoadScene(0);
        yield return 0;
    }
}
