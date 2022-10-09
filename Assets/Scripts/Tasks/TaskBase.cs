using Controllers;
using UnityEngine;

namespace Tasks
{
    public class TaskBase : ScriptableObject
    {
        public string taskText;
        public TaskController.TaskType taskType;
        public bool completed;

        public TaskBase(string taskText, TaskController.TaskType taskType)
        {
            this.taskText = taskText;
            this.taskType = taskType;
            completed = false;
        }
    }
}