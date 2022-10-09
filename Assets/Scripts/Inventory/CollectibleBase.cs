using System;
using UnityEngine;

namespace Inventory
{
    [RequireComponent(typeof(Sprite))] // this is the inventory representation of the item
    public class CollectibleBase : MonoBehaviour
    {
        [SerializeField] private string itemName;

        private InventoryManager _im;
        public bool canBePickedUp = true;

        private void Start()
        {
            _im = FindObjectOfType<InventoryManager>();
        }

        public virtual void PickupItem() // can be overridden if we want special/multi-stacking items for some reason
        {
            // When collected add it to the inventory then remove it from the game world
            _im.AddToInventory(this);

            // TODO remove item from game world
        }
    }
}