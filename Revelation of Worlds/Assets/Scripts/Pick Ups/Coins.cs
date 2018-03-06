using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour {
    public bool playerInRange;                         // Whether player is within the trigger collider and can be attacked.
    public int totalCoins = 0;
    public int valueCoins = 10;
    public CoinSystem coinSystem;

    private void Start()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        coinSystem = FindObjectOfType<CoinSystem>();
}

    void OnTriggerEnter(Collider player)
    {
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
            Destroy(gameObject);            
        }
    }
    
    private void OnDestroy()
    {
        coinSystem.CoinsAdd(valueCoins);
    }
}
