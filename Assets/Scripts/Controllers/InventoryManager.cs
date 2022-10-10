using System.Collections.Generic;
using Inventory;
using Inventory.Items;
using UnityEngine;

namespace Controllers
{
    public class InventoryManager : MonoBehaviour
    {
        // A list of all currently carried items in the inventory
        private List<InventoryItem> _inInventory;
        private ControllerManager _cm;

        private void Awake()
        {
            _inInventory = new List<InventoryItem>();

            // Get a reference to the controller manager for game update purposes
            _cm = FindObjectOfType<ControllerManager>();
        }

        public void AddToInventory(CollectibleBase item)
        {
            // We will build and add an inventory representation of this object to the inventory
            InventoryItem newItem = new(item.itemName, item, 1);
            _inInventory.Add(newItem);
            _cm.HUDController.UpdateHUD();

            if (_cm.GameController.debugMode) Debug.Log("Added " + item.itemName + " to the inventory.");
        }

        public IEnumerable<InventoryItem> GetInventory()
        {
            return _inInventory;
        }
    }
}