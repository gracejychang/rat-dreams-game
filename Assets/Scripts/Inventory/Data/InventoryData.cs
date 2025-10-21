using System;
using System.Collections.Generic;

[Serializable]
public class InventoryData
{
    public int totalPoints;
    public int totalPowerUps;
    public List<InventoryEntry> items;
}
