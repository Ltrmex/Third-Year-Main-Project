using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

// Code adapted from https://www.youtube.com/watch?v=SdaicjQTI-Y&ab_channel=TheGameContriver
public class StarRating : MonoBehaviour
{
    private int noStars;                                                                        // Number of stars calculated during the course of the game
    private int level;                                                                          // Variable holding the level of the game
    private PlayerHealth playerHealth;                                                          // Reference variable to Player's health
    private InitialiseStars initStars;                                                          // Reference variable to the initialization of the star images
    private float totalTime;                                                                    // The amount of time in which the rating stars are calculated
    //public static bool isLevelUp;                                                             // Checks if the player leveled up or not

    // Use this for initialization
    void Start()
    {
        initStars = FindObjectOfType<InitialiseStars>();                                        // Reference to the initialization of the star images
        playerHealth = (PlayerHealth)FindObjectOfType(typeof(PlayerHealth));                    // Reference to Player's health
    }

    // Update is called once per frame
    private void Update()
    {

        level = LevelSystem.currentLevel;                                                       // It gets the level of the game from the LevelSystem script

        // It resets the time every time the player levels up
        //if (isLevelUp == true)
        //{
        //    isLevelUp = false;
        //    totalTime = 0;
        //}

        // As long as the level is above 0
        if (level > 0)
        {
            totalTime += Time.deltaTime;                                                        // It calculates the time from when the player starts shooting until the game ends
            calculateStars(ScoreManager.score);                                                 // Calculates the amount of stars the player wins in the game
            // StarsCountFromTime(totalTime);
        }

        // The stars show once the player is dead
        if (playerHealth.isDead == true)
        {
            ShowStars(noStars);                                                                 // It shows and hides the star images accordingly
        }



    } // Update

    private void ShowStars(int noStars)
    {
        switch (noStars)
        {
            case 3:
                // The image with 3 stars is enabled and the other 2 are disabled
                initStars.Star1.GetComponent<Image>().enabled = false;                      // The image with 1 star is disabled
                initStars.Star2.GetComponent<Image>().enabled = false;                      // The image with 2 stars is disabled

                initStars.Star3.GetComponent<Image>().enabled = true;                       // The image with 3 stars is enabled
                break;
            case 2:
                // The image with 2 stars is enabled and the other 2 are disabled
                initStars.Star1.GetComponent<Image>().enabled = false;                      // The image with 1 star is disabled
                initStars.Star3.GetComponent<Image>().enabled = false;                      // The image with 3 stars is disabled

                initStars.Star2.GetComponent<Image>().enabled = true;                       // The image with 2 stars is enabled
                break;
            case 1:
                // The image with 1 star is enabled and the other 2 are disabled
                initStars.Star3.GetComponent<Image>().enabled = false;                      // The image with 3 stars is disabled
                initStars.Star2.GetComponent<Image>().enabled = false;                      // The image with 2 stars is disabled

                initStars.Star1.GetComponent<Image>().enabled = true;                       // The image with 1 star is enabled
                break;

        }
    }

    private void calculateStars(int score)
    {
        // Star calculus
        //  - 3 stars if the score is above or equal to 1000
        //  - 2 stars if the score is between 500 and 1000
        //  - 1 star if the score is below 500

        if (score >= 1000)
            noStars = 3;
        else if (score < 1000 && score > 500)
            noStars = 2;
        else
            noStars = 1;

    }

}