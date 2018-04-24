using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Author: Kamila Michel
public class SplashScreen : MonoBehaviour {

   //Create an object for menu and splash screen
   public GameObject mainMenuHolder;
   public GameObject splashScreenHolder;


    // Use this for initialization
    void Start () {
        StartCoroutine(LoadSplashScreen());
        mainMenuHolder.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
     IEnumerator LoadSplashScreen()
    {   //Load main menu after 10 sec
        yield return new WaitForSeconds(10f);
        mainMenuHolder.SetActive(true);
        splashScreenHolder.SetActive(false);
    }
}
