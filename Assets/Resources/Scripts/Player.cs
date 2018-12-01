using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public GameObject BulletPrefab;
    private Camera mainCamera;
    private float timeSinceLastBullet = 0f;

	// Use this for initialization
	void Start () {
        mainCamera = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {

        if (!GameController.instance.CurrentGameState.IsDoingRoulette) {
            moveCharacter();
            shootBullet();
        }
    }

    private void moveCharacter() {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 translate = new Vector2(x * Time.deltaTime * GameController.instance.CurrentGameState.PlayerMoveRate,
            y * Time.deltaTime * GameController.instance.CurrentGameState.PlayerMoveRate);

        float horzExtent = mainCamera.orthographicSize * Screen.width / Screen.height;
        float vertExtent = mainCamera.orthographicSize;

        Vector3 newPosition = gameObject.transform.position + translate;
        // TODO: Figure out how to clamp this without having to account for the other half of the player.
        transform.position = new Vector3(
            Mathf.Clamp(newPosition.x, mainCamera.transform.position.x - horzExtent + 0.5f, mainCamera.transform.position.x + horzExtent - 0.5f),
            Mathf.Clamp(newPosition.y, mainCamera.transform.position.y - vertExtent + 0.5f, mainCamera.transform.position.y + vertExtent - 0.5f),
            transform.position.z
        );
    }

    private void shootBullet() {
        if (timeSinceLastBullet > 0) {
            timeSinceLastBullet -= Time.deltaTime;
        } else {
            if (Input.GetAxisRaw("Fire1") > 0) {
                Instantiate(BulletPrefab, transform.position, transform.rotation, transform);
                timeSinceLastBullet += GameController.instance.CurrentGameState.PlayerFireDelay;
            }
        }
    }

    public void TakeDamage() {
        GameController.instance.TakePlayerDamage();
        if (GameController.instance.CurrentGameState.CurrentHullStrength <= 0) {
            GameController.instance.GameOver();
            Destroy(gameObject);
        }
    }
}
