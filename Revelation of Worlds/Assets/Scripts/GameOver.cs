using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameOver : MonoBehaviour {

    // Use this for initialization
    public GameObject mainMenuHolder;
    public GameObject mainOnGameOver;

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
 
    public void MainMenuGameOver()
    {
        mainOnGameOver.SetActive(true);
        mainMenuHolder.SetActive(false);
    }
   
}
