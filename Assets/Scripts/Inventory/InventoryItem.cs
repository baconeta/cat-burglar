using System;

namespace Inventory
{
    public struct InventoryItem
    {
        public string ItemName;
        public Type ObjectType;
        public int Number;

        public InventoryItem(string itemName, CollectibleBase item, int number)
        {
            ItemName = itemName;
            ObjectType = item.GetType();
            Number = number;
        }
    }
}