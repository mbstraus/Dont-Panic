using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour {

    public float MoveRate = 25f;
    public float lifeSpan = 2f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 movement = new Vector3(Time.deltaTime * MoveRate, 0, 0);
        transform.Translate(movement);

        // TODO: Destroy the bullet
        lifeSpan -= Time.deltaTime;
        if (lifeSpan <= 0) {
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
