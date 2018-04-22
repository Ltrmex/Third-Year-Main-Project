using UnityEngine;

//  Raising enemy difficulty
public class DifficultyRaise : MonoBehaviour {
    //  Variables
    public static bool isLevelUp;
    MapGenerator generate;
    public EnemyHealth enemy;

    // Use this for initialization
    void Start () {
        generate = FindObjectOfType<MapGenerator>();    //  reference to map generation
        enemy.startingHealth = 100; //  set enemy starting health
    }
	
	// Update is called once per frame
	void Update () {
        if (isLevelUp == true) {    //  check if leveled up
            generate.GenerateMap(); //  generate new map
            enemy.startingHealth += ((int)(enemy.startingHealth * 0.1));    //  increase enemy's health
            isLevelUp = false;  //  set to false
        }   //  Update()
	}
}
