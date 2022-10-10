using System.Collections.Generic;
using System.Linq;
using Inventory;
using Tasks;
using UnityEngine;

namespace Controllers
{
    /// <summary>
    /// Handles the assignment and completion of tasks in the game
    /// </summary>
    public class TaskController : MonoBehaviour
    {
        public enum TaskType
        {
            CollectItem,
            ReturnItem

            // TODO add other task types here?
        }

        private ControllerManager _cm;

        private int _numTasksCompleted;
        private List<TaskBase> _allTasks;

        private void Awake()
        {
            _cm = FindObjectOfType<ControllerManager>();
            _allTasks = new List<TaskBase>();
        }

        public void AddTask(TaskBase task)
        {
            _allTasks.Add(task);
            _cm.HUDController.UpdateHUD();

            if (_cm.GameController.debugMode) Debug.Log("New task - " + task.TaskText + " - added.");
        }

        public IEnumerable<TaskBase> GetTasks()
        {
            return _allTasks;
        }

        /// <summary>
        /// Efficient check to see if all tasks are completed or not
        /// </summary>
        /// <returns> true if all tasks are completed, false if not </returns>
        public bool AllTasksCompleted()
        {
            return _allTasks.All(taskBase => taskBase.Completed);
        }

        /// <summary>
        /// Called publicly when something happens in game that might mark a task as complete - now we check
        /// all incomplete tasks and mark any newly completed tasks as completed
        /// </summary>
        /// <param name="type"> The task types to check for completion. </param>
        /// <param name="item"> The item type relevant to this call </param>
        public void CheckTasks(TaskType type, InventoryItem item)
        {
            foreach (TaskBase task in _allTasks)
            {
                // If we have an incomplete task of this type
                if (type == task.TaskType && !task.Completed)
                {
                    // Mark it as complete if the item type is correct
                    if (item.ObjectType == task.ItemType)
                    {
                        // TODO mark task as completed internally, display to HUD, check if game loop completed
                        if (_cm.GameController.debugMode) Debug.Log("Task completed - " + task.TaskText);
                    }
                }
            }
        }
    }
}