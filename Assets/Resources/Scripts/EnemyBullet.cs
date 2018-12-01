using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {

    private float EnemyBulletLifeSpan = 4f;
    private float calculatedMoveSpeed = 0f;

	// Use this for initialization
	void Start () {
        calculatedMoveSpeed = Random.Range(GameController.instance.CurrentGameState.EnemyBulletMoveRate - GameController.instance.CurrentGameState.EnemyBulletMoveVariation,
            GameController.instance.CurrentGameState.EnemyBulletMoveRate + GameController.instance.CurrentGameState.EnemyBulletMoveVariation);
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 movement = new Vector3(-Time.deltaTime * calculatedMoveSpeed, 0, 0);
        transform.Translate(movement);

        EnemyBulletLifeSpan -= Time.deltaTime;
        if (EnemyBulletLifeSpan <= 0) {
            Destroy(gameObject);
        }
	}

    void OnCollisionEnter2D(Collision2D collision) {
        Player hitObject = collision.gameObject.GetComponent<Player>();
        if (hitObject != null) {
            Debug.Log("Enemy Bullet: I hit something!");
            hitObject.TakeDamage();
            Destroy(gameObject);
        }
    }
}
