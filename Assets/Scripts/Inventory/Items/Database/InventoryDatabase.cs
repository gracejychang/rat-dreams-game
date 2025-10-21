using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Inventory Database")]
public class InventoryDatabase : ScriptableObject
{
    [SerializeField]
    private List<InventoryItemBase> items = new();

    private Dictionary<string, InventoryItemBase> lookup;

    public void Initialize()
    {
        lookup = new Dictionary<string, InventoryItemBase>();

        foreach (var item in items)
        {
            if (item == null || string.IsNullOrEmpty(item.id))
            {
                Debug.LogWarning($"Missing item or ID in InventoryDatabase: {item?.name ?? "null"}");
                continue;
            }

            if (!lookup.ContainsKey(item.id))
            {
                lookup.Add(item.id, item);
            }
            else
            {
                Debug.LogWarning($"Duplicate item ID in InventoryDatabase: {item.id}");
            }
        }
    }

    public InventoryItemBase GetItemById(string id)
    {
        if (lookup == null)
        {
            Initialize();
        }

        lookup.TryGetValue(id, out var item);
        return item;
    }

    public List<InventoryItemBase> GetAllItems() => items;
}

