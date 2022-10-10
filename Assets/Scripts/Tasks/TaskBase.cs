using System;
using Controllers;
using Inventory;

namespace Tasks
{
    public struct TaskBase
    {
        public string TaskText;
        public TaskController.TaskType TaskType;
        public Type ItemType;
        public bool Completed;

        public TaskBase(string taskText, TaskController.TaskType taskType, Type itemType)
        {
            TaskText = taskText;
            TaskType = taskType;
            Completed = false;
            ItemType = itemType;
        }
    }
}