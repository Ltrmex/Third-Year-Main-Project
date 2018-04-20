using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class StarRating : MonoBehaviour
{
    private int noStars, level;
    private PlayerHealth playerHealth;
    private InitialiseStars initStars;
    private float totalTime;
    //public static bool isLevelUp;

    // Use this for initialization
    void Start()
    {
        initStars = FindObjectOfType<InitialiseStars>();
        playerHealth = (PlayerHealth)FindObjectOfType(typeof(PlayerHealth));
    }

    // Update is called once per frame
    private void Update()
    {

        level = LevelSystem.currentLevel;
        //if (isLevelUp == true)
        //{
        //    isLevelUp = false;
        //    totalTime = 0;
        //}

        if (level > 0)
        {
            totalTime += Time.deltaTime;
            calculateStars(ScoreManager.score);
            // StarsCountFromTime(totalTime);
        }

        if (playerHealth.isDead == true)
        {
            ShowStars(noStars);
        }



    } // Update

    private void ShowStars(int noStars)
    {
        switch (noStars)
        {
            case 3:
                initStars.Star1.GetComponent<Image>().enabled = false;
                initStars.Star2.GetComponent<Image>().enabled = false;

                initStars.Star3.GetComponent<Image>().enabled = true;
                break;
            case 2:

                initStars.Star1.GetComponent<Image>().enabled = false;
                initStars.Star3.GetComponent<Image>().enabled = false;

                initStars.Star2.GetComponent<Image>().enabled = true;
                break;
            case 1:
                initStars.Star3.GetComponent<Image>().enabled = false;
                initStars.Star2.GetComponent<Image>().enabled = false;

                initStars.Star1.GetComponent<Image>().enabled = true;
                break;

        }
    }

    private void calculateStars(int score)
    {
        if (score >= 1000)
            noStars = 3;
        else if (score < 1000 && score > 500)
            noStars = 2;
        else
            noStars = 1;

    }

}