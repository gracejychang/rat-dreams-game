using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float xScreenLimit = 2.0f;

    private bool canAttack = false;
    private float powerUpTimer = 0;
    private InventoryItemBase powerUpItem = null;
    [SerializeField] private GameObject fireAttackPrefab;

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;
        Move(position);
        
        if(Keyboard.current.spaceKey.wasPressedThisFrame) {
            HandlePowerUpInput();
        }
        UpdatePowerUpTimer();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            GameStateManager.Instance.GameOver();
        }
    }


    void Move (Vector3 position) {
        if(Keyboard.current.leftArrowKey.wasPressedThisFrame && position.x > -xScreenLimit) {
            position.x -= 1.0f;
        }

        if(Keyboard.current.rightArrowKey.wasPressedThisFrame && position.x < xScreenLimit) {
            position.x += 1.0f;
        }

        transform.position = position;
    }

    void HandlePowerUpInput() {
        if (!canAttack && InventoryManager.Instance.GetTotalPowerUps() <= 0) return;

        if (canAttack) {
            Attack();
        } else {
            powerUpItem = GetPowerUpItem();
            if (powerUpItem is PowerUpItem powerUp)
            {
                // update item entries
                InventoryManager.Instance.UseItem(powerUp);
                // activate power
                ActivatePowerUp(powerUp.duration);
                Attack();
            }
            else
            {
                Debug.LogWarning("Item is not a power up or does not exist");
                return;
            }

        }
    }

    void ActivatePowerUp(float duration) {
        canAttack = true;
        powerUpTimer = duration;
    }

    void DeactivatePowerUp() {
        canAttack = false;
    }

    void UpdatePowerUpTimer() {
        if (!canAttack) return;

        powerUpTimer -= Time.deltaTime;
        if(powerUpTimer <= 0f) {
            DeactivatePowerUp();
        }
    }

    // Get power up item 
    // In the future, we could switch which type of item based on user selection
    // But we just get the 'hot sauce' power up for now since that is the only power up item
    InventoryItemBase GetPowerUpItem() {
        InventoryEntry inventoryEntry = InventoryManager.Instance.GetItemById("hot_sauce");

        return inventoryEntry.item; // return the actual item
    }

    void Attack() {
        Vector3 spawnPosition = transform.position + new Vector3(0, 1f, 0);
        Quaternion spawnRotation = Quaternion.identity;
        switch(powerUpItem.id) {
            case "hot_sauce":
                GameObject fireAttack = Instantiate(fireAttackPrefab, spawnPosition, spawnRotation);
                fireAttack.SetActive(true);
                break;
        }
    }

}
