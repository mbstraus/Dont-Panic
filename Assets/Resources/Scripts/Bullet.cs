using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

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
        Debug.Log("ME HIT BAD GUY!");
        IDamagable hitObject = (IDamagable) collision.gameObject.GetComponent(typeof(IDamagable));
        if (hitObject != null) {
            hitObject.TakeDamage();
            Destroy(gameObject);
        }
    }
}
