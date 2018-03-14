using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionOnP : MonoBehaviour {

    public GameObject optionPanel;
    private bool isShowing;

    public void Update()
    {

        if (Input.GetKeyDown("p"))
        {
            isShowing = !isShowing;

            //Pause the game on p key
            if (Time.timeScale == 1 && isShowing == true)
            {
                //When timeScale is set to zero the game is basically paused
                Time.timeScale = 0;
            }//End of if
           
            //Used for press button
            //pauseButton.SetActive(false);
            //Time.timeScale = 0;



        }//End of if

    }//End of update 

    //Methon UnPause when user press p key second time
   
}
