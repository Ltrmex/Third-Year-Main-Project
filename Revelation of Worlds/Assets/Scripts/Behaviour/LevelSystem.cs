using UnityEngine;
using UnityEngine.UI;

//  Responsible of keeping track of players level
public class LevelSystem : MonoBehaviour {
    //  Variables
    public static int experiencePoints;
    public static int currentLevel;
    public Text displayLevel;
    public Text experienceDisplay;
    public Slider experienceSlider;

    // Use this for initialization
    void Start () {
        experiencePoints = 0;   //  initilize experience
    }

	// Update is called once per frame
	void Update () {
        ExperienceUpdate(); //  update experience

        //  Update values
        experienceDisplay.text = "EXP: " + experiencePoints;
        displayLevel.text = "Level: " + currentLevel;
        experienceSlider.value = experiencePoints;
    }

    void ExperienceUpdate() {
        int ourLevel = (int)(0.1f * Mathf.Sqrt(experiencePoints));  //  calculate level

        if (ourLevel != currentLevel) { //  check if level raised
            currentLevel = ourLevel;    //  set new level
            DifficultyRaise.isLevelUp = true;
        }   //  if

    }   //  ExperienceUpdate()
}
