using UnityEngine;

public class Object : MonoBehaviour
{
    private float fallSpeed = 1f;
    private float yScreenLimit = -5f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
        Vector2 position = transform.position;

        OffScreen(position);
    }

    void OffScreen(Vector2 position) {
        if(position.y < yScreenLimit) {
            Destroy(gameObject);
        }
    }
}
