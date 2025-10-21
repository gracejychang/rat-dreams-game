using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] private FoodItem foodItem; // assign values in Inspector

    void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.CompareTag("Player")) {
            InventoryManager.Instance.AddItem(foodItem);
            Destroy(gameObject);
        }
    }

}
