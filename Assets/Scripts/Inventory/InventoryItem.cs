using System;
using UnityEngine;

namespace Inventory
{
    public struct InventoryItem
    {
        public string ItemName;
        public Type ObjectType;
        public int Number;
        public Sprite InventoryImage;

        public InventoryItem(string itemName, CollectibleBase item, int number)
        {
            ItemName = itemName;
            ObjectType = item.GetType();
            Number = number;
            InventoryImage = item.inventorySprite;
        }
    }
}