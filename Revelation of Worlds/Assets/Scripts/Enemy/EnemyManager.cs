using System.Collections;
using UnityEngine;

// Author: Maciej
public class EnemyManager : MonoBehaviour {
    public PlayerHealth playerHealth;
    public Transform []spawnPoints;
    public GameObject enemy;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    private int waveNumber;

    private void Start() {
        playerHealth = (PlayerHealth)FindObjectOfType(typeof(PlayerHealth));
        StartCoroutine("SpawnWave");
    }   //  Start()

    IEnumerator SpawnWave() {
        if (playerHealth.isDead)
            StopCoroutine("SpawnWave");

        int randPosition = Random.Range(0, 9);

        yield return new WaitForSeconds(startWait);

        while (true) {
            for (int i = 1; i <= Random.Range(1, 10); i++) {
                Instantiate(enemy, spawnPoints[randPosition].position, spawnPoints[randPosition].rotation);
                yield return new WaitForSeconds(spawnWait);
            }   //  for
            yield return new WaitForSeconds(waveWait);
            ++waveNumber;

            if (waveNumber % 10 == 0) {
                if (!(waveWait <= 3))
                    waveWait -= 0.5f;
            }   //  if
        }   //  while
    }   //   SpawnWave()
}