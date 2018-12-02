using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlOfPetunias : MonoBehaviour {

    private float MoveSpeed;
    private Camera mainCamera;

	// Use this for initialization
	void Start () {
        MoveSpeed = (GameController.instance.CurrentGameState.BowlOfPetuniasDropSpeed
            + GameController.instance.CurrentGameState.BowlOfPetuniasDropSpeedRange) * GameController.instance.CurrentGameState.BowlOfPetuniasDropSpeedRange;
        mainCamera = Camera.main;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 movement = new Vector3(0, -Time.deltaTime * MoveSpeed, 0);
        transform.Translate(movement);

        float horzExtent = mainCamera.orthographicSize * Screen.width / Screen.height;

        if (transform.position.y > (mainCamera.transform.position.y + horzExtent)) {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        Player hitObject = collision.gameObject.GetComponent<Player>();
        if (hitObject != null) {
            hitObject.TakeDamage();
            Destroy(gameObject);
        }
    }
}
