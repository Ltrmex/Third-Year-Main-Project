using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class StarRating : MonoBehaviour
{
    private int noStars, level, i, currentLevel;
    private LevelSystem levelSystem;
    private InitialiseStars initStars;
    private float totalTime;
    public static bool isLevelUp;

    // Use this for initialization
    void Start()
    {
        levelSystem = FindObjectOfType<LevelSystem>();
        initStars = FindObjectOfType<InitialiseStars>();
        i = 1;
    }


    // Update is called once per frame
    private void Update()
    {

        level = levelSystem.currentLevel;
        if (isLevelUp == true)
        {
            isLevelUp = false;
            totalTime = 0;
        }

        if (level > 0)
        {
            totalTime += Time.deltaTime;
            StarsCount(totalTime);
            //  CalculateRating(noStars); 
        }


    } // Update

    /*private void CalculateRating(int noStars)
    {
        switch (noStars) {
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

    } */ // CalculateRating

    public void StarsCount(float totalTime)
    {

        
        // if time < 5 then 3 stars
        if (totalTime <= 5) {
            noStars = 3;

            initStars.Star1.GetComponent<Image>().enabled = false;
            initStars.Star2.GetComponent<Image>().enabled = false;

            initStars.Star3.GetComponent<Image>().enabled = true;
        } 
        // if time between 5 and 10 then 2 stars
        else if (totalTime > 5 && totalTime <= 10) {            
            noStars = 2;

            initStars.Star1.GetComponent<Image>().enabled = false;
            initStars.Star3.GetComponent<Image>().enabled = false;

            initStars.Star2.GetComponent<Image>().enabled = true;
        }
        // if time > 10 then 3 stars
        else {
            noStars = 1;


            initStars.Star3.GetComponent<Image>().enabled = false;
            initStars.Star2.GetComponent<Image>().enabled = false;

            initStars.Star1.GetComponent<Image>().enabled = true;
        }

    } // StarsCount
}