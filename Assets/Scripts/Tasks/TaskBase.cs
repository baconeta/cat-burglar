using System;
using Controllers;
using UnityEngine;

namespace Tasks
{
    [Serializable, ExecuteInEditMode]
    public class TaskBase
    {
        public string taskText;
        public TaskController.TaskType taskType;
        public string itemName;
        public Type ItemType;
        public bool completed;

        /// <summary>
        /// Editor when built from script data manually
        /// </summary>
        public TaskBase(string taskText, TaskController.TaskType taskType, Type itemType)
        {
            this.taskText = taskText;
            this.taskType = taskType;
            completed = false;
            ItemType = itemType;
        }

        public void PrepareTaskObjects()
        {
            if (ItemType == null && itemName != default)
            {
                ItemType = Type.GetType("Inventory.Items." + itemName);
                Debug.Log(ItemType);
            }
            else
            {
                Debug.Log("Error - tried to create an object without an explicit type");
            }
        }

        public void MarkDone()
        {
            completed = true;
        }
    }
}