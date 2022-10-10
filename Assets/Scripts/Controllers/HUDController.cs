using System.Collections.Generic;
using Tasks;
using UnityEngine;

namespace Controllers
{
    /// <summary>
    /// Will handle the updating of HUD elements on the game screen, such as tasks, inventory, etc
    /// </summary>
    public class HUDController : MonoBehaviour
    {
        private ControllerManager _cm;

        private void Awake()
        {
            _cm = FindObjectOfType<ControllerManager>();
        }

        public void UpdateHUD()
        {
            // Update various semi-dynamic HUD elements when this function is called

            // Update tasks
            UpdateTasks();

            // Update inventory
            UpdateInventory();
        }

        private void UpdateTasks()
        {
            var items = _cm.InventoryManager.GetInventory();
            // update each inventory slot sprite on screen
        }

        private void UpdateInventory()
        {
            var items = _cm.TaskController.GetTasks();
            // draw each task name on the screen
            // add strikethrough to the text component if task is completed
        }
    }
}