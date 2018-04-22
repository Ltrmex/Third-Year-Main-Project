using System.Collections;
using UnityEngine;

// Author: Maciej
public class EnemyManager : MonoBehaviour {
    //  Vairables
    public PlayerHealth playerHealth;
    public Transform []spawnPoints;
    public GameObject enemy;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    private int waveNumber;

    private void Start() {
        playerHealth = (PlayerHealth)FindObjectOfType(typeof(PlayerHealth));
        StartCoroutine("SpawnWave");    //  start intervals
    }   //  Start()

    IEnumerator SpawnWave() {
        if (playerHealth.isDead)    //  check if playe is dead
            StopCoroutine("SpawnWave"); //  if yes then stop spawning enemies

        int randPosition = Random.Range(0, 9);  //  get random number

        yield return new WaitForSeconds(startWait); //  start wait before spawn

        while (true) {
            for (int i = 1; i <= Random.Range(1, 10); i++) {    //  spawn from one to ten enemies
                Instantiate(enemy, spawnPoints[randPosition].position, spawnPoints[randPosition].rotation); //  instantiate new enemy
                yield return new WaitForSeconds(spawnWait); //  wait for next spawn
            }   //  for
            yield return new WaitForSeconds(waveWait);  //  wave wait
            ++waveNumber;   //  increase wave number

            if (waveNumber % 10 == 0) { //  check if wave number increased by 10
                if (!(waveWait <= 3))   //  set limitation
                    waveWait -= 0.5f;   //  decrease wave wait, thus making game tougher
            }   //  if
        }   //  while
    }   //   SpawnWave()
}