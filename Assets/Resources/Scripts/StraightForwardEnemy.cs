using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightForwardEnemy : MonoBehaviour, IEnemy {

    public int Health = 1;
    public int Shields = 0;
    public float MoveSpeed = 5f;
    public float FireSpeed = 1f;
    public GameObject EnemyBulletPrefab;
    public float InvulnerableDuration = 0.1f;

    private float timeSinceLastBullet = 0f;
    private bool isInvulnerable = false;

    public void TakeDamage() {
        if (!isInvulnerable) {
            if (Shields > 0) {
                Shields -= 1;
            } else {
                Health -= 1;
            }
            if (Health <= 0) {
                GameController.instance.KillEnemy();
                Destroy(gameObject);
            }
        }
    }

    // Use this for initialization
    void Start () {
        // Start with an initial delay so that it doesn't shoot off-screen
        // timeSinceLastBullet = FireSpeed;
        isInvulnerable = true;
        Shields = (Shields + GameController.instance.CurrentGameState.EnemyShieldsModifier) * GameController.instance.CurrentGameState.EnemyShieldsMultiplier;
        MoveSpeed = (MoveSpeed + GameController.instance.CurrentGameState.EnemyMoveRateModifier) * GameController.instance.CurrentGameState.EnemyMoveRateMultiplier;
        FireSpeed = (FireSpeed + GameController.instance.CurrentGameState.EnemyFireRateModifier) * GameController.instance.CurrentGameState.EnemyFireRateMultiplier;
	}
	
	// Update is called once per frame
	void Update () {
        if (isInvulnerable) {
            InvulnerableDuration -= Time.deltaTime;
            if (InvulnerableDuration <= 0) {
                isInvulnerable = false;
            }
        }
        Vector3 moveVector = new Vector3(-Time.deltaTime * MoveSpeed, 0, 0);
        transform.Translate(moveVector);

        if (timeSinceLastBullet > 0) {
            timeSinceLastBullet -= Time.deltaTime;
        } else {
            // Shoot bullet
            Instantiate(EnemyBulletPrefab, transform.position, transform.rotation);
            timeSinceLastBullet = FireSpeed;
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        Player hitObject = collision.gameObject.GetComponent<Player>();
        if (hitObject != null) {
            Debug.Log("StraightForwardEnemy: CRASH!");
            hitObject.TakeDamage();
            Destroy(gameObject);
        }
    }
}
