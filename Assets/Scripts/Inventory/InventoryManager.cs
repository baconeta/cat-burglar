using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        // A list of all currently carried items in the inventory
        private List<CollectibleBase> _inInventory;

        private void Start()
        {
            _inInventory = new List<CollectibleBase>();
        }

        public void AddToInventory(CollectibleBase item)
        {
            _inInventory.Add(item);
        }

        public List<CollectibleBase> GetInventory()
        {
            return _inInventory;
        }
    }
}