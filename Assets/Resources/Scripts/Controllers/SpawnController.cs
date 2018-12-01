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

    public GameObject[] StarPrefabs;
    public float StarDelayBetweenSpawnsSec = 0.01f;
    public float StarSpawnVariationSec = 0.02f;

    public BulletContainer BulletContainer;

    void Awake() {
        instance = this;
    }

    // Use this for initialization
    void Start() {
        GameController.instance.CurrentGameState.RecalculateStats();
        StartCoroutine(SpawnEnemies());
        StartCoroutine(SpawnStars());
        BulletContainer = FindObjectOfType<BulletContainer>();
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

        while (!IsWaveComplete() && !GameController.instance.CurrentGameState.IsGameOver) {
            int enemyToSpawn = Random.Range(0, EnemyPrefabs.Length);

            float horzExtent = Camera.main.orthographicSize * Screen.width / Screen.height;
            float vertExtent = Camera.main.orthographicSize;

            float enemySpawnY = Random.Range(-vertExtent / 2, vertExtent / 2);
            Vector3 spawnPosition = new Vector3(horzExtent + 1, enemySpawnY, 0f);

            Instantiate(EnemyPrefabs[enemyToSpawn], spawnPosition, Quaternion.identity, EnemyContainer.transform);

            float nextSpawn = GameController.instance.CurrentGameState.DelayBetweenSpawnsSec
                + Random.Range(-GameController.instance.CurrentGameState.SpawnVariationSec, GameController.instance.CurrentGameState.SpawnVariationSec);

            yield return new WaitForSeconds(nextSpawn);
        }

        foreach (Transform enemy in EnemyContainer.transform) {
            Destroy(enemy.gameObject);
        }
        foreach (Transform bullet in BulletContainer.transform) {
            Destroy(bullet.gameObject);
        }
        if (PlayerObject != null) {
            Destroy(PlayerObject);
        }

        if (!GameController.instance.CurrentGameState.IsGameOver) {
            RouletteController.instance.ShowRoulette();
        }
    }
	
    IEnumerator SpawnStars() {
         while (true) {
            int starToSpawn = Random.Range(0, StarPrefabs.Length);
            float horzExtent = Camera.main.orthographicSize * Screen.width / Screen.height;
            float vertExtent = Camera.main.orthographicSize;
            float starSpawnY = Random.Range(-vertExtent, vertExtent);

            Vector3 spawnPosition = new Vector3(10, starSpawnY, 0f);

            Instantiate(StarPrefabs[starToSpawn], spawnPosition, Quaternion.identity);

            float nextSpawn = StarDelayBetweenSpawnsSec + Random.Range(-StarSpawnVariationSec, StarSpawnVariationSec);
            yield return new WaitForSeconds(nextSpawn);
        }
    }

    public bool IsWaveComplete() {
        return GameController.instance.CurrentGameState.KilledEnemies >= GameController.instance.CurrentGameState.NumberOfEnemiesToSpawn;
    }

    public void StartNextWave() {
        GameController.instance.CurrentGameState.KilledEnemies = 0;
        StartCoroutine(SpawnEnemies());
    }
}
