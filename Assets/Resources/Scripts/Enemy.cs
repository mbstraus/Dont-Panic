using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int Health = 1;

    private void Update() {
        if (Health <= 0) {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damageAmount) {
        Health -= damageAmount;
    }
}
