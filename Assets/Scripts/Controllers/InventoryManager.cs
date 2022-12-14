using System.Collections.Generic;
using Inventory;
using UnityEngine;

namespace Controllers
{
    /// <summary>
    /// This class will handle anything to do globally with the inventory system as well as storing and managing
    /// the list of objects (InventoryItem - struct) that are held by the player
    /// </summary>
    public class InventoryManager : MonoBehaviour
    {
        // A list of all currently carried items in the inventory
        private List<InventoryItem> _inInventory;
        private ControllerManager _cm;
        public int maxInventorySize = 6;

        private void Awake()
        {
            _inInventory = new List<InventoryItem>();

            // Get a reference to the controller manager for game update purposes
            _cm = FindObjectOfType<ControllerManager>();
        }

        public void AddToInventory(CollectibleBase item)
        {
            if (_cm.GameController.debugMode) Debug.Log("Added " + item.itemName + " to the inventory.");

            // We will build and add an inventory representation of this object to the inventory
            InventoryItem newItem = new(item.itemName, item, 1);
            _inInventory.Add(newItem);
            _cm.HUDController.UpdateHUD();

            // Inform the task controller to check the list of tasks for this particular task
            _cm.TaskController.CheckTasksOfType(TaskController.TaskType.CollectItem, newItem);
            _cm.TaskController.CheckCompletion();
        }

        public IEnumerable<InventoryItem> GetInventory()
        {
            return _inInventory;
        }

        public void DropOffItems()
        {
            if (_cm.GameController.debugMode) Debug.Log("Dropping all items to the return spot.");
            foreach (InventoryItem item in _inInventory)
            {
                if (_cm.GameController.debugMode) Debug.Log("Item " + item.ItemName + " returned.");
                var tasksDone = _cm.TaskController.CheckTasksOfType(TaskController.TaskType.ReturnItem, item);
                _cm.Achievements.RetrieveItem(item.ItemName);
                _cm.Achievements.RetrieveNotNeededItem(tasksDone);
            }

            // For now we assume everything is dropped off but maybe we can extend this to X items later
            _inInventory.Clear();
            _cm.TaskController.CheckCompletion();
            _cm.HUDController.UpdateHUD();
        }

        /// <summary>
        /// Called when you want the player to have to drop all collected items - maybe caught by a patrol or something?
        /// </summary>
        public void ItemsDropped()
        {
            // This will need to spawn the items back to their starting positions? Might need game controller to do that
            if (_cm.GameController.debugMode) Debug.Log("Dropping all items! Oops!");

            _inInventory.Clear();
            _cm.HUDController.UpdateHUD();
        }

        public bool PlayerHasSpace()
        {
            return _inInventory.Count < maxInventorySize;
        }
    }
}