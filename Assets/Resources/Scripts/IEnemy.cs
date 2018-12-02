using UnityEngine;
using System.Collections;

public abstract class IEnemy : MonoBehaviour {

    public int Health = 1;
    public int Shields = 0;
    public float MoveSpeed = 5f;
    public float FireSpeed = 1f;
    public float InvulnerableDuration = 0.1f;

    protected BulletContainer BulletContainer;
    protected bool isInvulnerable = false;

    public void TakeDamage() {
        if (!isInvulnerable) {
            if (Shields > 0) {
                Shields -= 1;
            } else {
                Health -= 1;
            }
            if (Health == 0) {
                GameController.instance.KillEnemy();
                Destroy(gameObject);
            }
        }
    }

    public bool DetectAndDestroyOutOfBoundsEnemy(Camera mainCamera) {
        float horzExtent = mainCamera.orthographicSize * Screen.width / Screen.height;
        float vertExtent = mainCamera.orthographicSize;

        if (transform.position.x < (mainCamera.transform.position.x - horzExtent)
            || transform.position.y < (mainCamera.transform.position.y - horzExtent)
            || transform.position.y > (mainCamera.transform.position.y + horzExtent)) {
            Destroy(gameObject);
            return true;
        }
        return false;
    }

    public void SetInvulernable() {
        isInvulnerable = true;
        StartCoroutine(InvulernablePeriod());
    }

    IEnumerator InvulernablePeriod() {
        yield return new WaitForSeconds(InvulnerableDuration);
        isInvulnerable = false;
    }

    void OnCollisionEnter2D(Collision2D collision) {
        Player hitObject = collision.gameObject.GetComponent<Player>();
        if (hitObject != null) {
            hitObject.TakeDamage();
            Destroy(gameObject);
        }
    }
}
