using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinSystem : MonoBehaviour {
    public int totalCoins;
    public Text coinDisplay;
    public Slider coinSlider;                          // Reference to the UI's health bar.

    // Use this for initialization
    void Start () {
        totalCoins = PlayerPrefs.GetInt("AmountCoins", 0);
        coinDisplay = GameObject.Find("TextCoin").GetComponent<Text>();
        coinSlider = GameObject.Find("CoinUI").GetComponent<Slider>();
        coinDisplay.text = "Coins: " + totalCoins;
        coinSlider.value = totalCoins;
    }
    

    public void CoinsAdd(int coinsToAdd)
    {
        totalCoins += coinsToAdd;
        if (totalCoins != 0)
        {
            PlayerPrefs.SetInt("AmountCoins", totalCoins);
        }
        
        coinDisplay.text = "Coins: " + totalCoins;
        coinSlider.value = totalCoins;
    }

    private void Update()
    {
        if (Input.GetKeyDown("m"))
        {
            totalCoins = 0;
        }
    }
}
