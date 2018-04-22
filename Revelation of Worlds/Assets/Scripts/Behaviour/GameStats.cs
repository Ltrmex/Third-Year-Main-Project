using UnityEngine;
using UnityEngine.UI;

public class GameStats : MonoBehaviour {
    //  Variables
    public Text[] stats;

    //  Death stats being updated
	public void Update () {
        stats[0].text = ScoreManager.score.ToString();
        stats[1].text = LevelSystem.currentLevel.ToString();
        stats[3].text = EnemyHealth.enemyCount.ToString();
        stats[2].text = CoinSystem.collectedCoins.ToString();
    }
}
