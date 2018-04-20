using UnityEngine;
using UnityEngine.UI;

public class GameStats : MonoBehaviour {
    public Text[] stats;

	public void Update () {
        stats[0].text = ScoreManager.score.ToString();
        stats[1].text = LevelSystem.currentLevel.ToString();
        stats[3].text = EnemyHealth.enemyCount.ToString();
        stats[2].text = CoinSystem.collectedCoins.ToString();
        //stats[4].text = ;
        //stats[5].text = ;
        //stats[6].text = ;

    }
}
