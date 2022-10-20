using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Controllers
{
    [Serializable]
    public struct Achievement
    {
        public string achievementName;

        [Tooltip("Name used to reference the prefs values")]
        public string achievementPrefsCodeName;

        [HideInInspector] public bool completed;
    }

    public class AchievementController : MonoBehaviour
    {
        [Tooltip("For each achievement, code will need to be written to match it (for simplicity)")]
        public List<Achievement> allAchievements;

        private ControllerManager _cm;

        public void Start()
        {
            _cm = FindObjectOfType<ControllerManager>();
            // This will mark off any tasks that should be marked off as being completed prior to this session.
            CheckCompletedTasks();
        }

        public void CompleteTask()
        {
            AddInt("TotalCompletedTasks", 1);
        }

        public void CollectItem(string itemName)
        {
            AddInt("Collect" + itemName, 1);
        }

        public void RetrieveItem(string itemName)
        {
            AddInt("Retrieve" + itemName, 1);
        }

        private void GetCaught()
        {
            SetInt("TasksWithoutCaught", 0);
            AddInt("TimesCaught", 1);
        }

        private static void SetInt(string keyName, int value)
        {
            PlayerPrefs.SetInt(keyName, value);
        }

        private static int GetInt(string keyName)
        {
            return PlayerPrefs.GetInt(keyName, 0);
        }

        private void AddInt(string keyName, int value)
        {
            PlayerPrefs.SetInt(keyName, GetInt(keyName) + value);
            if (_cm.GameController.debugMode)
            {
                Debug.Log("Task updated: " + keyName + " - New value = " + GetInt(keyName));
            }
        }

        private void SetBool(string boolName, bool value)
        {
            PlayerPrefs.SetInt(boolName, value ? 1 : 0);
        }

        private bool GetBool(string boolName)
        {
            return PlayerPrefs.GetInt(boolName) == 1;
        }

        public List<Achievement> GetCompletedTasks()
        {
            return allAchievements.Where(achievement => achievement.completed).ToList();
        }

        // This will perform a check to look for any incomplete tasks and see if they have been completed or not.
        // TODO either make this occur during game over one time -or live update as a player plays to inform them
        private List<Achievement> CheckCompletedTasks()
        {
            // Search through each of the achievements and see if it is done or not. 
            var toCheck = allAchievements.Where(achievement => !achievement.completed).ToList();
            var nowComplete = new List<Achievement>();

            foreach (Achievement ach in toCheck)
            {
                // Add all possible achievements here and check conditions
                switch (ach.achievementPrefsCodeName)
                {
                    default:
                        nowComplete.Add(ach);
                        break;
                    case "CollectToiletPaper1":
                        if (GetInt("CollectToiletPaper") >= 1)
                        {
                            nowComplete.Add(ach);
                        }

                        break;
                    case "CollectToothpaste1":
                        if (GetInt("CollectToothpaste") >= 1)
                        {
                            nowComplete.Add(ach);
                        }

                        break;
                }
            }

            if (_cm.GameController.debugMode)
            {
                foreach (Achievement ach in nowComplete)
                {
                    Debug.Log("Task marked as completed: " + ach.achievementName);
                }
            }

            return nowComplete;
        }
    }
}