using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    [SerializeField] private InventoryDatabase database;

    private Dictionary<string, InventoryEntry> inventoryEntriesObj = new();
    private int totalPoints = 0;
    private int totalPowerUps = 0;

    private string SavePath => Path.Combine(Application.persistentDataPath, "inventory.json");

    void Awake() {
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            database.Initialize();

            // load from existing file/game
            if(File.Exists(SavePath)) {
                LoadFromFile();
            }
        } else {
            Destroy(gameObject);
        }
    }
    
    // Getters & Setters
    public int GetTotalPoints() => totalPoints;

    public int GetTotalPowerUps() => totalPowerUps;

    public InventoryEntry GetItemById(string itemId) {
        if(inventoryEntriesObj.ContainsKey(itemId)) {
            return inventoryEntriesObj[itemId];
        } else {
            Debug.LogWarning($"No item by {itemId} found");
            return null;
        }
    }

    // Quantitiy logic
    public void AddItem(InventoryItemBase item) {
        // add update quantity of item if exists in inventory entires,
        // if not create new entry with quantity = 1     
        if(inventoryEntriesObj.ContainsKey(item.id)) {
            inventoryEntriesObj[item.id].quantity += 1;
        } else {
            inventoryEntriesObj[item.id] = new InventoryEntry(item, 1);
        }

        // perform item specific behaviour
        switch(item) {
            case FoodItem food:
                AddPoints(food.pointValue);
                InventoryUI.Instance?.UpdatePoints();
                break;
            case PowerUpItem powerUp:
                AddPowerUp();
                InventoryUI.Instance?.UpdatePowerUps();
                break;
        }
    }

    public void UseItem(InventoryItemBase item) {
        // don't do anything if item is not usable 
        // or does not exist in entries
        if (!item.Usable || !inventoryEntriesObj.ContainsKey(item.id)) return;

        var itemQuantity =  inventoryEntriesObj[item.id].quantity;

        // remove from items if quantity is 1
        if (itemQuantity <= 1) {
            inventoryEntriesObj.Remove(item.id);
        } else {
            inventoryEntriesObj[item.id].quantity -= 1;
        }

        // perform item specific behaviour
        switch(item) {
            case PowerUpItem powerUp:
                RemovePowerUp();
                InventoryUI.Instance?.UpdatePowerUps();
                break;
        }
    }

    private void AddPoints(int amount) {
        totalPoints += amount;
    }

    private void AddPowerUp() {
        totalPowerUps++;
    }
    
    private void RemovePowerUp() {
        totalPowerUps--;
    }

    // Reset inventory
    public void ResetInventory() {
        totalPoints = 0;
        totalPowerUps = 0;
        inventoryEntriesObj.Clear();
    }

    // Save/Load logic
    public void SaveToFile()
    {
        var data = SaveInventoryData();
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(SavePath, json, Encoding.UTF8);
        Debug.Log($"Inventory saved to file path: {SavePath}");
    }

    public void LoadFromFile()
    {
        string json = File.ReadAllText(SavePath, Encoding.UTF8);
        var data = JsonUtility.FromJson<InventoryData>(json);
        LoadInventoryData(data);
        Debug.Log($"Inventory loaded from existing file path: {SavePath}");
    }

    // save inventoryEntries to JSON as items, as well as total points and powerups
    private InventoryData SaveInventoryData() {
        var data = new InventoryData {
            totalPoints = totalPoints,
            totalPowerUps = totalPowerUps,
            items = new List<InventoryEntry>()
        };

        // correctly serialize each item to an entry
        foreach (var inventoryEntry in inventoryEntriesObj.Values) {
            data.items.Add(new InventoryEntry(inventoryEntry.item, inventoryEntry.quantity));
        }

        return data;
    }

    // load items from JSON to inventoryEntiresObj, as well as total points and powerups
    private void LoadInventoryData(InventoryData data)
    {
        totalPoints = data.totalPoints;
        totalPowerUps = data.totalPowerUps;

        inventoryEntriesObj.Clear();

        foreach (var savedEntry in data.items)
        {
            var item = database.GetItemById(savedEntry.id);
            if (item != null)
            {
                // correctly deserialize the item from json and add it to the entries dict
                inventoryEntriesObj[item.id] = new InventoryEntry(item, savedEntry.quantity);
            }
            else
            {
                Debug.LogWarning($"Item with ID {savedEntry.id} not found in database, please ensure item asset exists in the database.");
            }
        }
    }
    
    private void OnApplicationQuit() {
        SaveToFile();
    }

}
