using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
    //public PlayerHealth playerHealth;
    public GameObject enemy;
    public float spawnTime = 3f; // 3 seconds spawn time
    public Transform[] spawnPoints;

	// Use this for initialization
	void Start () {
        // repeats Spawn every 3 seconds
        // InvokeRepeating (function, wait before spawning, wait before repeating)
        InvokeRepeating("Spawn", spawnTime, spawnTime);
	}
	
	/* Update is called once per frame
	void Update () {
		
	}*/

    void Spawn()
    {
        // if player health is less than 0 nobody is spawned
        /*if(playerHealth.currentHealth <= 0f)
        {
            return;
        }*/

        // spawnPointIndex represents a point in the array
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        // create an instance of an object
        // Instantiate (what, where, rotation)
        Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
}
