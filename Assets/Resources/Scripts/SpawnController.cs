using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class SpawnController : MonoBehaviour {

    public static SpawnController instance;

    public int NumberOfEnemiesToSpawn = 50;
    public float DelayBetweenSpawnsSec = 0.5f;
    public float SpawnVariationSec = 0.2f;

    public Text EnemiesRemainingText;

    public GameObject[] EnemyPrefabs;

    private int killedEnemies = 0;

    void Awake() {
        instance = this;
    }

    // Use this for initialization
    void Start() {
        StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    void Update() {
        EnemiesRemainingText.text = "Enemies Remaining: " + Mathf.Clamp(NumberOfEnemiesToSpawn - killedEnemies, 0, NumberOfEnemiesToSpawn);
    }

    IEnumerator SpawnEnemies() {
        while (true) {
            Debug.Log("Spawning an enemy!");
            if ((NumberOfEnemiesToSpawn - killedEnemies) > 0) {
                int enemyToSpawn = Random.Range(0, EnemyPrefabs.Length);

                float horzExtent = Camera.main.orthographicSize * Screen.width / Screen.height;
                float vertExtent = Camera.main.orthographicSize;

                float enemySpawnY = Random.Range(-vertExtent / 2, vertExtent / 2);
                Vector3 spawnPosition = new Vector3(horzExtent + 1, enemySpawnY, 0f);

                Instantiate(EnemyPrefabs[enemyToSpawn], spawnPosition, Quaternion.identity);

                float nextSpawn = DelayBetweenSpawnsSec + Random.Range(-SpawnVariationSec, SpawnVariationSec);
                Debug.Log("Time till next spawn: " + nextSpawn);

                yield return new WaitForSeconds(nextSpawn);
            } else {
                // An enemy may come back into the pool if it makes it to the other side of the screen, so don't exit
                // if there are no enemies left.
                yield return null;
            }
        }
    }

    public void KillEnemy() {
        killedEnemies += 1;
    }

    public bool IsWaveComplete() {
        return killedEnemies >= NumberOfEnemiesToSpawn;
    }
}
