using System;
using Controllers;
using UnityEngine;

namespace Tasks
{
    [Serializable]
    public class TaskBase
    {
        public string taskText;
        public TaskController.TaskType taskType;
        public string itemName;
        public Type ItemType;
        [HideInInspector] public bool completed;
        public int num = 1;
        [HideInInspector] public int numCompleted;

        /// <summary>
        /// Editor when built from script data manually
        /// </summary>
        public TaskBase(string taskText, TaskController.TaskType taskType, Type itemType)
        {
            this.taskText = taskText;
            this.taskType = taskType;
            completed = false;
            ItemType = itemType;
            num = 1;
        }

        public TaskBase()
        {
            completed = false;
        }

        /// <summary>
        /// Called during awake step to make sure any editor defined tasks have proper item objects attached
        /// </summary>
        public virtual void PrepareTaskObjects()
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
            numCompleted = num;
        }
    }
}