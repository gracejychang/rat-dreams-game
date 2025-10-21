using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Food Item")]
public class FoodItem : InventoryItemBase
{
    public int pointValue; // number of points you can receive
    
    public override ItemType Type => ItemType.Food;
    public override bool Usable => false;
}
