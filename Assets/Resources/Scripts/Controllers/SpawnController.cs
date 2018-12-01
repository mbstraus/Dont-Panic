using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class SpawnController : MonoBehaviour {

    public static SpawnController instance;
    public GameObject[] EnemyPrefabs;
    public GameObject EnemyContainer;
    public GameObject PlayerSpawnLocation;
    public GameObject PlayerPrefab;

    private GameObject PlayerObject;

    void Awake() {
        instance = this;
    }

    // Use this for initialization
    void Start() {
        GameController.instance.CurrentGameState.RecalculateStats();
        StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    void Update() {
    }

    public IEnumerator SpawnEnemies() {

        GameController.instance.StartNewWave();

        PlayerObject = Instantiate(PlayerPrefab, PlayerSpawnLocation.transform.position, Quaternion.identity);

        while (GameController.instance.IsAnimatingGoWindow) {
            yield return null;
        }

        while (!IsWaveComplete()) {
            int enemyToSpawn = Random.Range(0, EnemyPrefabs.Length);

            float horzExtent = Camera.main.orthographicSize * Screen.width / Screen.height;
            float vertExtent = Camera.main.orthographicSize;

            float enemySpawnY = Random.Range(-vertExtent / 2, vertExtent / 2);
            Vector3 spawnPosition = new Vector3(horzExtent + 1, enemySpawnY, 0f);

            Instantiate(EnemyPrefabs[enemyToSpawn], spawnPosition, Quaternion.identity, EnemyContainer.transform);

            float nextSpawn = GameController.instance.CurrentGameState.DelayBetweenSpawnsSec
                + Random.Range(-GameController.instance.CurrentGameState.SpawnVariationSec, GameController.instance.CurrentGameState.SpawnVariationSec);
            Debug.Log("Time till next spawn: " + nextSpawn);

            yield return new WaitForSeconds(nextSpawn);
        }

        foreach (Transform enemy in EnemyContainer.transform) {
            Destroy(enemy.gameObject);
        }
        Destroy(PlayerObject);

        RouletteController.instance.ShowRoulette();
    }

    public bool IsWaveComplete() {
        return GameController.instance.CurrentGameState.KilledEnemies >= GameController.instance.CurrentGameState.NumberOfEnemiesToSpawn;
    }

    public void StartNextWave() {
        Debug.Log("Starting next wave");
        GameController.instance.CurrentGameState.KilledEnemies = 0;
        StartCoroutine(SpawnEnemies());
    }
}
