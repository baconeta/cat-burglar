using System.Collections.Generic;
using System.Linq;
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
            
            if (_cm.GameController.debugMode) Debug.Log("New task - " + task.taskText + " - added.");
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
            return _allTasks.All(taskBase => taskBase.completed);
        }
    }
}