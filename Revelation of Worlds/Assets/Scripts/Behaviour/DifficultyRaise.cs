using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyRaise : MonoBehaviour {
    public static bool isLevelUp;
    MapGenerator generate;
    public EnemyHealth enemy;

    // Use this for initialization
    void Start () {
        generate = FindObjectOfType<MapGenerator>();
        enemy.startingHealth = 100;
    }
	
	// Update is called once per frame
	void Update () {
        if (isLevelUp == true) {
            generate.GenerateMap();
            enemy.startingHealth += ((int)(enemy.startingHealth * 0.1));
            isLevelUp = false;
        }   //  Update()
	}
}
