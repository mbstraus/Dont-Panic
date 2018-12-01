using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable {

    public int Health = 1;

    public void TakeDamage() {
        Debug.Log("I TOOK SOME DAMAGE!");
        Health -= 1;

        if (Health <= 0) {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
