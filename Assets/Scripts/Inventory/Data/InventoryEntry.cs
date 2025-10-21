using System;

[Serializable]
public class InventoryEntry
{
    [NonSerialized] public InventoryItemBase item;

    // serializable fields
    public string id;
    public string itemName;
    public bool usable;
    public int quantity;

    public InventoryEntry(InventoryItemBase item, int quantity)
    {
        this.item = item;

        // serialized values which are saved to JSON
        this.id = item.id;
        this.itemName = item.itemName;
        this.usable = item.Usable;
        this.quantity = quantity;
    }
}
