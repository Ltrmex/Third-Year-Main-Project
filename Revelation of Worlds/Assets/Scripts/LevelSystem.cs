using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSystem : MonoBehaviour {

    public static int experiencePoints;
    public int currentLevel;
    public Text displayLevel;
    public Text experienceDisplay;
    public Slider experienceSlider;

    // Use this for initialization
    void Start () {
        currentLevel = 0;
        experiencePoints = 0;

    }

	// Update is called once per frame
	void Update () {
        ExperienceUpdate();

        experienceDisplay.text = "EXP: " + experiencePoints;
        displayLevel.text = "Level: " + currentLevel;
        experienceSlider.value = experiencePoints;
    }

    void ExperienceUpdate() {
        int ourLevel = (int)(0.1f * Mathf.Sqrt(experiencePoints));

        if (ourLevel != currentLevel)
            currentLevel = ourLevel;

        int experienceNeeded = 100 * (currentLevel + 1) * (currentLevel + 1);
        int difference = experienceNeeded - experiencePoints;

        int totalDifference = experienceNeeded - (100 * currentLevel * currentLevel);

        experienceSlider.maxValue = totalDifference;

    }   //  ExperienceUpdate()
}
