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
    public Animator fadeoutAnim;
    public GameObject fadeoutScreen;
    public GameObject loadingScreen;
    public GameObject settingsScreen;
    public GameObject creditsScreen;
    public float timeBeforeCredits;
    private DisplayText textScript;
    private Slider volumeSetting;
    private Slider difficultySetting;
    public int levelToLoad;
    // Start is called before the first frame update
    void Start()
    {
        loadingScreen.SetActive(false);
        settingsScreen.SetActive(false);
        creditsScreen.SetActive(false);
        fadeoutScreen.SetActive(false);
        textScript = creditsScreen.transform.Find("Text").GetComponent<DisplayText>();
        volumeSetting = settingsScreen.transform.Find("Volume").Find("Volume Slider").GetComponent<Slider>();
        difficultySetting = settingsScreen.transform.Find("Difficulty").Find("Difficulty Slider").GetComponent<Slider>();

    }

    // Update is called once per frame
    void Update()
    {
        if (settingsScreen.activeSelf)
        {
            PlayerPrefs.SetFloat("Volume", volumeSetting.value);
            PlayerPrefs.SetInt("Difficulty", Convert.ToInt32(difficultySetting.value));
        }
    }

    public void StartGame()
    {
        fadeoutScreen.SetActive(true);
        fadeoutAnim.SetTrigger("Begin");
        StartCoroutine(ToLoadingScreen());
    }

    public void ToggleSettings()
    {
        settingsScreen.SetActive(!settingsScreen.activeSelf);
        volumeSetting.value = PlayerPrefs.GetFloat("Volume");
        difficultySetting.value = PlayerPrefs.GetInt("Difficulty");
        creditsScreen.SetActive(false);
        
    }

    public void RollCredits()
    {
        creditsScreen.SetActive(!creditsScreen.activeSelf);
        settingsScreen.SetActive(false);
        textScript.ClearText();
        StartCoroutine(StartCredits());
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

    IEnumerator StartCredits()
    {
        yield return new WaitForSeconds(timeBeforeCredits);
        textScript.startCreditsRoll();
        yield return 0;
    }
}
