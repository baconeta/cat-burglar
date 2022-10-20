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
        private List<TaskBase> _currentTasks;

        public List<TaskBase> possibleTasks;

        private void Awake()
        {
            _cm = FindObjectOfType<ControllerManager>();
            _currentTasks = new List<TaskBase>();

            foreach (TaskBase t in possibleTasks)
            {
                t.PrepareTaskObjects();
            }
        }

        public void AddTask(TaskBase task)
        {
            if (_cm.GameController.debugMode) Debug.Log("New task - " + task.taskText + " - added.");
            _currentTasks.Add(task);
            _cm.HUDController.UpdateHUD();
        }

        public IEnumerable<TaskBase> GetTasks()
        {
            return _currentTasks;
        }

        /// <summary>
        /// Efficient check to see if all tasks are completed or not
        /// </summary>
        /// <returns> true if all tasks are completed, false if not </returns>
        private bool AllTasksCompleted()
        {
            return _currentTasks.All(taskBase => taskBase.completed);
        }

        /// <summary>
        /// Called publicly when something happens in game that might mark a task as complete - now we check
        /// all incomplete tasks and mark any newly completed tasks as completed
        /// </summary>
        /// <param name="type"> The task types to check for completion. </param>
        /// <param name="item"> The item type relevant to this call </param>
        public int CheckTasksOfType(TaskType type, InventoryItem item)
        {
            var tasksMarkedComplete = 0;
            foreach (TaskBase task in _currentTasks)
            {
                // Check if we have completed any of the incomplete tasks
                if (type == task.taskType && !task.completed && item.ObjectType == task.ItemType)
                {
                    if (_cm.GameController.debugMode) Debug.Log("Task completed - " + task.taskText);
                    task.MarkDone();
                    AchievementController.CompleteTask();
                    _numTasksCompleted += 1;
                    tasksMarkedComplete += 1;
                }
            }

            _cm.HUDController.UpdateHUD();
            return tasksMarkedComplete;
        }

        public void CheckCompletion()
        {
            if (AllTasksCompleted() && _currentTasks.Count > 0)
            {
                _currentTasks.Clear();
                _cm.GameController.EndRound();
            }
        }
    }
}