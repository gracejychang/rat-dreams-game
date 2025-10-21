using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/PowerUp Item")]
public class PowerUpItem : InventoryItemBase
{
    public float duration; // duration of power up effect
    
    public override ItemType Type => ItemType.PowerUp;
    public override bool Usable => true;
}
