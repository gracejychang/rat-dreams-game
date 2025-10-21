using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private float attackSpeed = 1f;
    [SerializeField] private float yScreenLimit = 5f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * attackSpeed * Time.deltaTime);
        Vector2 position = transform.position;

        OffScreen(position);
    }

    void OffScreen(Vector2 position) {
        if(position.y > yScreenLimit) {
            Destroy(gameObject);
        }
    }

    // Destroy the object if it collides with anything other than itself
    void OnCollisionEnter2D(Collision2D collision) {
        if(!collision.gameObject.CompareTag("Attack")) {
            Destroy(gameObject);
        }
    }
}
