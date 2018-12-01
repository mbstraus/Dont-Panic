using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float MoveFactor = 5f;
    public float FireDelaySec = 0.5f;
    public GameObject BulletPrefab;

    private Camera mainCamera;
    private float timeSinceLastBullet = 0f;

	// Use this for initialization
	void Start () {
        mainCamera = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
        moveCharacter();
        shootBullet();
    }

    private void moveCharacter() {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 translate = new Vector2(x * Time.deltaTime * MoveFactor, y * Time.deltaTime * MoveFactor);

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

    private void shootBullet() {
        if (timeSinceLastBullet > 0) {
            timeSinceLastBullet -= Time.deltaTime;
        } else {
            if (Input.GetAxisRaw("Fire1") > 0) {
                Instantiate(BulletPrefab, transform.position, transform.rotation);
                timeSinceLastBullet += FireDelaySec;
            }
        }
    }
}
