using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance {get; private set;}

    [SerializeField] private TextMeshProUGUI pointsText;
    [SerializeField] private TextMeshProUGUI powerUpsText;
    [SerializeField] private GameObject inventoryPanel;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Prevent duplicate UI managers
        }
    }

    public void InitializeUI() 
    {
        inventoryPanel.SetActive(true);
        UpdatePoints();
        UpdatePowerUps();
    }

    public void UpdatePoints()
    {
        int points = InventoryManager.Instance.GetTotalPoints();
        pointsText.text = $"{points}";
    }

    public void UpdatePowerUps() 
    {
        int powerUps = InventoryManager.Instance.GetTotalPowerUps();
        powerUpsText.text = $"{powerUps}";
    }

    public void HideInventoryPanel() {
        inventoryPanel.SetActive(false);
    }
}
