using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoringTime : MonoBehaviour {
    public int level;
    public Text text;                                                                       // Reference to the Text component.
    public static bool isLevelUp;
    private int currentLevel;
    private float totalRatingTime;


    private void Awake()
    {
        text = GetComponent<Text>();

    }


    // Update is called once per frame
    private void Update()
    {
   
        level = LevelSystem.currentLevel;                                                  // starts at;
        
        //if (isLevelUp == true)
        //{
        //    isLevelUp = false;
        //    totalRatingTime = 0;
        //}

        if (level > 0)
        {
            totalRatingTime += Time.deltaTime;
            outputTime(totalRatingTime);
            
        }
     

    }

    public void outputTime(float totalRatingTime)
    {
        text.text = "Time: " + totalRatingTime.ToString("f2");
    }
}
