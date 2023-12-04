using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    public float timeToLoadingScreen;
    public float timeInLoadingScreen;
    public GameObject loadingScreen;
    public GameObject settingsScreen;
    public int levelToLoad;
    // Start is called before the first frame update
    void Start()
    {
        loadingScreen.SetActive(false);
        settingsScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (settingsScreen.activeSelf)
        {
            PlayerPrefs.SetFloat("Volume", settingsScreen.transform.Find("Volume").Find("Volume Slider").GetComponent<Slider>().value);
            PlayerPrefs.SetInt("Difficulty", Convert.ToInt32(settingsScreen.transform.Find("Difficulty").Find("Difficulty Slider").GetComponent<Slider>().value));
        }
    }

    public void StartGame()
    {
        StartCoroutine(ToLoadingScreen());
    }

    public void ToggleSettings()
    {
        settingsScreen.SetActive(!settingsScreen.activeSelf);
    }

    public void RollCredits()
    {
        settingsScreen.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
    IEnumerator ToLoadingScreen()
    {
        yield return new WaitForSeconds(timeToLoadingScreen);
        loadingScreen.SetActive(true);
        StartCoroutine(ToNextLevel());
        yield return 0;
    }

    IEnumerator ToNextLevel()
    {
        yield return new WaitForSeconds(timeInLoadingScreen);
        SceneManager.LoadScene(levelToLoad);
        yield return 0;
    }
}
