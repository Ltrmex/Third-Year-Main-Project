using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Author:Kamila Michel
//Source codeadapted from: https://www.youtube.com/watch?v=zc8ac_qUXQY&t=605s
public class MainMenu : MonoBehaviour {

    //Medhod created for Play option from main menu
    public void PlayGame() {

        //Loads the next level(next Scene)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //Mehod created for Quit option from main menu
    public void QuitGame()
    {
        //Prints Quit to the console, checks if the quit button is working
        Debug.Log("Quit");
        //Closes the program
        Application.Quit();
    }

}

