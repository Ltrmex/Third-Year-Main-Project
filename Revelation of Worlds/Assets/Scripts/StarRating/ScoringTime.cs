using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Author Cristina
public class ScoringTime : MonoBehaviour {
    public int level;                                                                       // Variable holding the level of the game
    public Text text;                                                                       // Reference to the Text component.
    //public static bool isLevelUp;                                                         // Checks if the player leveled up or not
    private float totalRatingTime;                                                          // The amount of time in which the rating stars are calculated


    private void Awake()
    {
        text = GetComponent<Text>();

    }


    // Update is called once per frame
    private void Update()
    {
   
        level = LevelSystem.currentLevel;                                                   // It gets the level of the game from the LevelSystem script

        // It resets the time every time the player levels up
        //if (isLevelUp == true)
        //{
        //    isLevelUp = false;
        //    totalRatingTime = 0;
        //}

        // As long as the level is above 0
        if (level > 0)
        {
            totalRatingTime += Time.deltaTime;                                              // It calculates the time from when the player starts shooting until the game ends
            outputTime(totalRatingTime);                                                    
            
        }
     

    }

    public void outputTime(float totalRatingTime)
    {
        text.text = "Time: " + totalRatingTime.ToString("f2");                              // It outputs the time to the screen with 2 decimal places - f2
    }
}
