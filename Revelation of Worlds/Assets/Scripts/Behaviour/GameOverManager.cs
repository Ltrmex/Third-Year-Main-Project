using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Kamila Michel, Maciej Majchrzak
//Student: G00340498
//Code adapted from: https://unity3d.com/learn/tutorials/projects/survival-shooter/game-over?playlist=17144

public class GameOverManager : MonoBehaviour {

    // Reference to the player's health.
    public PlayerHealth playerHealth;
    // Time to wait before restarting the level
    public float restartDelay = 5f;
    // Timer to count up to restarting the level
    float restartTimer;
    public GameObject GameOverPanel;
    public GameObject HudCanvas;
    private DataController dataController;
    bool isClicked = false;

    // Use this for initialization
    void Start()
    {
        restartTimer = Time.time;
        dataController = FindObjectOfType<DataController>();
    }

    public void Clicked() {
        isClicked = true;
    }

    void Update()
    {
        // If the player has run out of health...
        if (playerHealth.currentHealth <= 0)
        {
            if (Time.time > restartTimer + restartDelay)
            {
                //Increment a timer to count up to restarting.
                restartTimer += Time.deltaTime;
                GameOverPanel.SetActive(true);
                HudCanvas.SetActive(false);

                if(isClicked)
                    dataController.Submit(LevelSystem.currentLevel, EnemyHealth.enemyCount, ScoreManager.score);
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
}
