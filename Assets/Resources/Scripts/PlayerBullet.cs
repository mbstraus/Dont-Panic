using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour {

    private float PlayerBulletLifeSpan = 2f;

    // Update is called once per frame
    void Update () {
        Vector3 movement = new Vector3(Time.deltaTime * GameController.instance.CurrentGameState.PlayerBulletMoveRate, 0, 0);
        transform.Translate(movement);
        PlayerBulletLifeSpan -= Time.deltaTime;
        if (PlayerBulletLifeSpan <= 0) {
            Destroy(gameObject);
        }
	}

    void OnCollisionEnter2D(Collision2D collision) {
        IEnemy hitObject = (IEnemy) collision.gameObject.GetComponent(typeof(IEnemy));
        if (hitObject != null) {
            Debug.Log("Player Bullet: I hit something!");
            hitObject.TakeDamage();
            Destroy(gameObject);
        }
    }
}
