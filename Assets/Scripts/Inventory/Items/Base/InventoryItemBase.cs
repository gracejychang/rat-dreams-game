using UnityEngine;

public enum ItemType
{
    Food,
    PowerUp
}

public abstract class InventoryItemBase : ScriptableObject
{
    [Header("Item Identity")]
    [Tooltip("Unique ID used to save/load this item")]
    public string id;
    
    public string itemName;
    public Sprite icon;
    
    public abstract ItemType Type { get; }
    public virtual bool Usable => false;
}
