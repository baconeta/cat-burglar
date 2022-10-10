using Controllers;
using Inventory;
using UnityEngine;

namespace Tasks
{
    /// <summary>
    /// Will be used as a drop off spot for collected items during tasks and will also handle letting the TaskManager
    /// know when an item has been dropped off.
    /// </summary>
    public class ReturnSpot : MonoBehaviour
    {
        private ControllerManager _cm;

        private void Start()
        {
            _cm = FindObjectOfType<ControllerManager>();
        }

        private void OnTriggerEnter(Collider other)
        {
            // When the player runs into this - drop off all held items
            if (other.gameObject.CompareTag("Player"))
            {
                _cm.InventoryManager.DropOffItems();
                // TODO Perhaps we want to do something like play a sound, send a message to HUD, etc
            }
        }
    }
}