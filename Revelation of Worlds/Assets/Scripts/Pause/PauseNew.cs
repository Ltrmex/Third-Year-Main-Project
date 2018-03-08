using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Kamila Michel
public class PauseNew : MonoBehaviour {

    public GameObject pausePanel;

     public void Update() { 
   
        if (Input.GetKeyDown("p"))
        {
            //Pause the game on p key
            if (Time.timeScale == 1)
            {
                //When timeScale is set to zero the game is basically paused
                Time.timeScale = 0;

                //Show the paused menu 
                pausePanel.SetActive(true);

                //Used for press button
                //pauseButton.SetActive(false);
                //Time.timeScale = 0;

            }//End of if
            
        }//End of if

     }//End of update 

    //Methon UnPause when user press p key second time
    public void UnPause()
    {
        //Doesn't show the paused menu
        pausePanel.SetActive(false);
        //Back to game.When timeScale is 1.0 the time is passing as fast as realtime.
        Time.timeScale = 1;

        
    }
}
