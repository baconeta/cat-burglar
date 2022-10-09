using System.Collections.Generic;
using Inventory;
using UnityEngine;

namespace Controllers
{
    public class InventoryManager : MonoBehaviour
    {
        // A list of all currently carried items in the inventory
        private List<CollectibleBase> _inInventory;
        private ControllerManager _cm;

        private void Start()
        {
            _inInventory = new List<CollectibleBase>();

            // Get a reference to the controller manager for game update purposes
            _cm = FindObjectOfType<ControllerManager>();
        }

        public void AddToInventory(CollectibleBase item)
        {
            _inInventory.Add(item);
            _cm.HUDController.UpdateHUD();

            if (_cm.GameController.debugMode) Debug.Log("Added " + item.itemName + " to the inventory.");
        }

        public IEnumerable<CollectibleBase> GetInventory()
        {
            return _inInventory;
        }
    }
}