using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Author Cristina
public class CoinSystem : MonoBehaviour {
    public int totalCoins;                                                                  // Total amount of coins collected
    public static int collectedCoins = 0;                                                   // The amount of coins collected
    public Text coinDisplay;                                                                // Reference to the coin text display
    public Slider coinSlider;                                                               // Reference to the coin bar.

    // Use this for initialization
    void Start () {
        totalCoins = PlayerPrefs.GetInt("AmountCoins", 0);                                  // It sets the value for the coins as 0 at the start
        coinDisplay = GameObject.Find("TextCoin").GetComponent<Text>();                     // It gets the text component for the coin
        coinSlider = GameObject.Find("CoinUI").GetComponent<Slider>();                      // It gets the slider component for the coin
        coinDisplay.text = "Coins: " + totalCoins;                                          // It displays the amount of coins to the scene
        coinSlider.value = totalCoins;                                                      // It give the slider a value so that it can change colour
    }
    

    public void CoinsAdd(int coinsToAdd)
    {
        totalCoins += coinsToAdd;                                                           // It gets the value from the Coins script and it adds it to the totalCoins to get the total value of the coins

        // As long as the total amount of coins is not 0 coint the amount of coins collected
        if (totalCoins != 0)
        {
            ++collectedCoins;
            PlayerPrefs.SetInt("AmountCoins", totalCoins);                                  // It saves the value of the coins
        }
        

    }

    private void Update()
    {
        coinDisplay.text = "Coins: " + totalCoins;                                          // It display the amount of coins in the text component in the scene
        coinSlider.value = totalCoins;                                                      // It changes the value of the coin slider so it has to move and change colour

        // The total value of coins gets reset to 0 when the player presses the "x" key 
        if (Input.GetKeyDown("x"))
        {
            totalCoins = 0;
        }
    }
}
