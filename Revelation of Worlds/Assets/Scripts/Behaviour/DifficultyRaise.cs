using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyRaise : MonoBehaviour {
    public static bool isLevelUp;
    MapGenerator generate;

    // Use this for initialization
    void Start () {
        generate = FindObjectOfType<MapGenerator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (isLevelUp == true) {
            generate.GenerateMap();
            isLevelUp = false;
        }   //  Update()
	}
}
