using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinSystem : MonoBehaviour {
    public int totalCoins;
    public static int collectedCoins = 0;
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
            ++collectedCoins;
            PlayerPrefs.SetInt("AmountCoins", totalCoins);
        }
        

    }

    private void Update()
    {
        coinDisplay.text = "Coins: " + totalCoins;
        coinSlider.value = totalCoins;
        if (Input.GetKeyDown("x"))
        {
            totalCoins = 0;
        }
    }
}
