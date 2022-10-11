using System;
using Controllers;
using Inventory;

namespace Tasks
{
    public class TaskBase
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

        public void MarkDone()
        {
            Completed = true;
        }
    }
}