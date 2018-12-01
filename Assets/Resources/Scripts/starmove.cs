using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class starmove : MonoBehaviour {
    public float MoveFactor = 5f;
    private Camera mainCamera;



    void Start()
    {
        mainCamera = Camera.main;
    }
    // Update is called once per frame
    void Update () {
        moveStarLeft();
	}

    private void moveStarLeft()
    {
        float x = -5;
        float y = 0;

        Vector3 translate = new Vector2(x * Time.deltaTime * MoveFactor, y);
        transform.Translate(translate);

        float horzExtent = mainCamera.orthographicSize * Screen.width / Screen.height;
        float vertExtent = mainCamera.orthographicSize;

              float starXpos = transform.position.x;
        if(starXpos < (mainCamera.transform.position.x - horzExtent) )
        {
            Destroy(gameObject);
        }


    }




}
