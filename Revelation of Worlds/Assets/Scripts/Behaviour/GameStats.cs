using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStats : MonoBehaviour {
    public Text[] stats;
    private LevelSystem level;
    private CoinSystem coins;

    // Use this for initialization
    void Start () {
        level = FindObjectOfType<LevelSystem>();
        coins = FindObjectOfType<CoinSystem>();
    }
	
	public void CheckStats () {
        stats[0].text = ScoreManager.score.ToString();
        stats[1].text = level.currentLevel.ToString();
        stats[2].text = coins.collectedCoins.ToString();
        //stats[3].text = ;
        //stats[4].text = ;
        //stats[5].text = ;
        //stats[6].text = ;

    }
}
