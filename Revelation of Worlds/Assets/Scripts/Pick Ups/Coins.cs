using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author Cristina
public class Coins : MonoBehaviour {
    public bool playerInRange;                                                  // Whether player is within the trigger collider and can be attacked.
    public int totalCoins = 0;                                                  // The total amount of couns
    public int valueCoin = 10;                                                  // One coin has a value of 10 
    public CoinSystem coinSystem;                                               // Reference to CoinSystem

    private void Start()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.yellow;      // It sets the object colour to yellow
        coinSystem = FindObjectOfType<CoinSystem>();                            // It references the CoinSystem script
    }

    void OnTriggerEnter(Collider player)
    {
        // If the entering collider is the player...
        if (player.gameObject.CompareTag("Player"))
        {
            // ... the player is in range.
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider player)
    {
        // If the exiting collider is the player...
        if (player.gameObject.CompareTag("Player"))
        {
            // ... the player is no longer in range.
            playerInRange = false;
        }
    }
    
    void Update()
    {
        if (playerInRange /*&& Input.GetKeyDown("e")*/)
        {
            // gameObject.SetActive(false);
            Destroy(gameObject);                                            // It calls OnDestroy method           
        }   
    }
    
    private void OnDestroy()
    {        
        coinSystem.CoinsAdd(valueCoin);                                     // It sends the coin value to the CoinsAdd method in Coin System
    }
}
