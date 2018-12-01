using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {

    public float MoveRate = 10f;
    public float MoveVariation = 2f;
    public float lifeSpan = 2f;

    private float calculatedMoveSpeed = 0f;

	// Use this for initialization
	void Start () {
        calculatedMoveSpeed = Random.Range(MoveRate - MoveVariation, MoveRate + MoveVariation);
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 movement = new Vector3(-Time.deltaTime * calculatedMoveSpeed, 0, 0);
        transform.Translate(movement);

        // TODO: Destroy the bullet
        lifeSpan -= Time.deltaTime;
        if (lifeSpan <= 0) {
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
