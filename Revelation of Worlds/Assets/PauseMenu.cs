using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

    // Use this for initialization
    public GameObject mainMenuHolder;
    public GameObject optionsMenuHolder;
    public GameObject mainOnPause;


    public Toggle[] resolutionToggles;
    public Toggle fullscreenToggle;
    public int[] screenWidths;
    int activeScreenResIndex;

    void Start()
    {
        activeScreenResIndex = PlayerPrefs.GetInt("screen res index");
        bool isFullscreen = (PlayerPrefs.GetInt("fullscreen") == 1) ? true : false;



        for (int i = 0; i < resolutionToggles.Length; i++)
        {
            resolutionToggles[i].isOn = i == activeScreenResIndex;
        }

        //Need to be fix
        //fullscreenToggle.isOn = isFullscreen;
    }


    public void Play()
    {
        //SceneManager.LoadScene("Level01");
        //Loads the next level(next Scene)
         SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        //Prints Quit to the console, checks if the quit button is working
        //Debug.Log("Quit");
        //Closes the program
        Application.Quit();
    }
    public void OptionMenu()
    {
        mainMenuHolder.SetActive(false);
        optionsMenuHolder.SetActive(true);
        mainMenuHolder.SetActive(false);
    }

    public void MainMenu()
    {
        mainMenuHolder.SetActive(true);
        optionsMenuHolder.SetActive(false);

    }
    public void MainMenuPause()
    {
        mainOnPause.SetActive(true);
        optionsMenuHolder.SetActive(false);
        mainMenuHolder.SetActive(false);
    }
    public void Back()
    {
        mainMenuHolder.SetActive(true);
        optionsMenuHolder.SetActive(false);
        mainOnPause.SetActive(false);
    }
    public void SetScreenResolution(int i)
    {
        if (resolutionToggles[i].isOn)
        {
            activeScreenResIndex = i;
            float aspectRatio = 16 / 9f;
            Screen.SetResolution(screenWidths[i], (int)(screenWidths[i] / aspectRatio), false);
            PlayerPrefs.SetInt("screen res index", activeScreenResIndex);
            PlayerPrefs.Save();
        }
    }
    public void SetFullScreen(bool isFullscreen)
    {
        for (int i = 0; i < resolutionToggles.Length; i++)
        {
            resolutionToggles[i].interactable = !isFullscreen;
        }

        if (isFullscreen)
        {
            Resolution[] allResolutions = Screen.resolutions;
            Resolution maxResolution = allResolutions[allResolutions.Length - 1];
            Screen.SetResolution(maxResolution.width, maxResolution.height, true);
        }
        else
        {
            SetScreenResolution(activeScreenResIndex);
        }
        PlayerPrefs.SetInt("fullscreen", ((isFullscreen) ? 1 : 0));
        PlayerPrefs.Save();
    }
}
