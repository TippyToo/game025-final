using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public float timeToLoadingScreen;
    public float timeInLoadingScreen;
    public GameObject loadingScreen;
    public int levelToLoad;
    // Start is called before the first frame update
    void Start()
    {
        loadingScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        StartCoroutine(ToLoadingScreen());
    }

    public void ToggleSettings()
    {

    }

    public void RollCredits()
    {

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
