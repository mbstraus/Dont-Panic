using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightForwardEnemy : IEnemy {

    public GameObject EnemyBulletPrefab;

    private float timeSinceLastBullet = 0f;

    private Camera mainCamera;

    // Use this for initialization
    void Start () {
        // Start with an initial delay so that it doesn't shoot off-screen
        // timeSinceLastBullet = FireSpeed;
        Shields = (Shields + GameController.instance.CurrentGameState.EnemyShieldsModifier) * GameController.instance.CurrentGameState.EnemyShieldsMultiplier;
        MoveSpeed = (MoveSpeed + GameController.instance.CurrentGameState.EnemyMoveRateModifier) * GameController.instance.CurrentGameState.EnemyMoveRateMultiplier;
        FireSpeed = (FireSpeed + GameController.instance.CurrentGameState.EnemyFireRateModifier) * GameController.instance.CurrentGameState.EnemyFireRateMultiplier;

        BulletContainer = FindObjectOfType<BulletContainer>();
        mainCamera = Camera.main;

        SetInvulernable();
    }

    void Update() {
        if (isExploding) {
            return;
        }
        // Custom logic for how this enemy moves
        Vector3 moveVector = new Vector3(-Time.deltaTime * MoveSpeed, 0, 0);
        transform.Translate(moveVector);
        bool didDestroy = DetectAndDestroyOutOfBoundsEnemy(mainCamera);
        if (didDestroy) {
            return;
        }

        // Custom logic for how this enemy shoots a bullet
        if (timeSinceLastBullet > 0) {
            timeSinceLastBullet -= Time.deltaTime;
        } else {
            SoundController.instance.PlayEnemyLaserShoot();
            Instantiate(EnemyBulletPrefab, transform.position, transform.rotation, BulletContainer.transform);
            timeSinceLastBullet = FireSpeed;
        }
    }
}
