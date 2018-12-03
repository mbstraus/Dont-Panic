using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour {

    public GameObject[] BulletPrefab;
    public GameObject[] ShieldSprites;
    public GameObject[] ExplosionSprites;
    public SpriteRenderer PlayerGraphics;
    public Sprite ShipSprite;
    public Sprite WhaleSprite;

    private Camera mainCamera;
    private float timeSinceLastBullet = 0f;
    private bool IsShieldAnimating = false;
    private float gunJamTimeRemaining = 0f;
    private bool IsDestroying = false;
    private float DestroyingTimeRemaining = 1f;

	// Use this for initialization
	void Start () {
        mainCamera = Camera.main;

        if (GameController.instance.CurrentGameState.IsPlayerAWhale) {
            PlayerGraphics.sprite = WhaleSprite;
        } else {
            PlayerGraphics.sprite = ShipSprite;
        }

        if (GameController.instance.CurrentGameState.IsPlayerGunJamming) {
            StartCoroutine(JamGun());
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (!GameController.instance.CurrentGameState.IsDoingRoulette && !IsDestroying) {
            MoveCharacter();
            ShootBullet();
        }
    }

    private void MoveCharacter() {
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

    private void ShootBullet() {
        if (GameController.instance.CurrentGameState.IsPlayerGunJammed) {
            gunJamTimeRemaining -= Time.deltaTime;
            if (gunJamTimeRemaining <= 0f) {
                GameController.instance.UnjamGun();
            } else {
                Debug.Log(gunJamTimeRemaining);
                return;
            }
        }

        if (timeSinceLastBullet > 0) {
            timeSinceLastBullet -= Time.deltaTime;
        } else {
            if (Input.GetAxisRaw("Fire1") > 0) {
                SoundController.instance.PlayPlayerShoot();
                Instantiate(BulletPrefab[GameController.instance.CurrentGameState.CurrentBullet], transform.position, transform.rotation);
                timeSinceLastBullet += GameController.instance.CurrentGameState.PlayerFireDelay;
            }
        }
    }

    public void TakeDamage() {
        if (IsShieldAnimating) {
            return;
        }

        bool isTakingShieldDamage = GameController.instance.TakePlayerDamage();
        if (isTakingShieldDamage) {
            SoundController.instance.PlayShieldHit();
            IsShieldAnimating = true;
            StartCoroutine(PlayShieldAnimation());
        }
        if (GameController.instance.CurrentGameState.CurrentHullStrength <= 0) {
            IsDestroying = true;
            StartCoroutine(PlayDeathAnimation());
        }
    }

    IEnumerator PlayShieldAnimation() {
        ShieldSprites[0].SetActive(true);
        yield return new WaitForSeconds(0.4f);
        ShieldSprites[0].SetActive(false);
        ShieldSprites[1].SetActive(true);
        yield return new WaitForSeconds(0.4f);
        ShieldSprites[1].SetActive(false);
        ShieldSprites[2].SetActive(true);
        yield return new WaitForSeconds(0.4f);
        ShieldSprites[2].SetActive(false);

        IsShieldAnimating = false;
    }

    IEnumerator PlayDeathAnimation() {
        SoundController.instance.PlayPlayerDeath();
        PlayerGraphics.gameObject.SetActive(false);

        ExplosionSprites[0].SetActive(true);
        yield return new WaitForSeconds(0.3f);
        ExplosionSprites[0].SetActive(false);
        ExplosionSprites[1].SetActive(true);
        yield return new WaitForSeconds(0.3f);
        ExplosionSprites[1].SetActive(false);
        ExplosionSprites[2].SetActive(true);
        yield return new WaitForSeconds(0.3f);

        GameController.instance.GameOver();
        SoundController.instance.StopPlayerDeathSound();
        Destroy(gameObject);
    }

    IEnumerator JamGun() {
        while (true) {
            float nextGunJam = Random.Range(GameController.instance.CurrentGameState.PlayerGunJamFrequencySec - GameController.instance.CurrentGameState.PlayerGunJamVariationSec,
                GameController.instance.CurrentGameState.PlayerGunJamFrequencySec + GameController.instance.CurrentGameState.PlayerGunJamVariationSec);
            yield return new WaitForSeconds(nextGunJam);

            GameController.instance.JamGun();
            Debug.Log("Jamming Gun!");
            gunJamTimeRemaining = GameController.instance.CurrentGameState.PlayerGunJamDurationSec;
        }
    }
}
