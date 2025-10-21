using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private PowerUpItem powerUpItem; // assign values in Inspector

    void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.CompareTag("Player")) {
            Destroy(gameObject);
            InventoryManager.Instance.AddItem(powerUpItem);
        }
    }
}
