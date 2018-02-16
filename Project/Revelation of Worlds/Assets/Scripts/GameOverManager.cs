using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Kamila Michel
//Student: G00340498
//Code adapted from: https://unity3d.com/learn/tutorials/projects/survival-shooter/game-over?playlist=17144

public class GameOverManager : MonoBehaviour {

    // Reference to the player's health.
    public PlayerHealth playerHealth;
    // Time to wait before restarting the level
    public float restartDelay = 5f;

    // Reference to the animator component.
    Animator anim;
    // Timer to count up to restarting the level
    float restartTimer;                    


    void Awake()
    {
        // Set up the reference using GetComponent
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        // If the player has run out of health...
        if (playerHealth.currentHealth <= 0)
        {
            //Tell the animator the game is over.
            anim.SetTrigger("GameOver");

            //Increment a timer to count up to restarting.
            restartTimer += Time.deltaTime;

            // .. if it reaches the restart delay...
            //Can be use when we'll add different levels
           /* if (restartTimer >= restartDelay)
            {
                // .. then reload the currently loaded level.
                Application.LoadLevel(Application.loadedLevel);
            }*/
        }
    }
}
