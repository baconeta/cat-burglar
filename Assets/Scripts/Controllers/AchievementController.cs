using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Controllers
{
    [Serializable]
    public class Achievement
    {
        public string achievementName;

        [Tooltip("Name used to reference the prefs values")]
        public string achievementPrefsCodeName;

        public string subMessage = "???";

        [HideInInspector] public bool completed;
    }

    public class AchievementController : MonoBehaviour
    {
        [Tooltip("For each achievement, code will need to be written to match it (for simplicity)")]
        public List<Achievement> allAchievements;

        public void Start()
        {
            // This will mark off any tasks that should be marked off as being completed prior to this session.
            CheckCompletedAchievements();
        }

        public void CompleteTask()
        {
            AddInt("TotalCompletedTasks", 1);
            AddInt("TasksWithoutCaught", 1);
            if (GetInt("TasksWithoutCaught") >= 10)
            {
                SetBool("10InARow", true);
            }
        }

        public void CollectItem(string itemName)
        {
            AddInt("Collect" + itemName, 1);
            AddInt("CollectAny", 1);
        }

        public void RetrieveItem(string itemName)
        {
            AddInt("Retrieve" + itemName, 1);
            AddInt("RetrieveAny", 1);
        }

        public void GetCaught()
        {
            SetInt("TasksWithoutCaught", 0);
            AddInt("TimesCaught", 1);
        }

        public void Meow()
        {
            AddInt("Meow", 1);
        }

        public void HearMeMeow()
        {
            AddInt("HearMeMeow", 1);
        }

        public void RetrieveNotNeededItem(int tasksDone)
        {
            AddInt("RetrieveNotNeeded", tasksDone);
        }

        public void CheckGuardsAtOnce(int numGuards)
        {
            if (numGuards >= 3)
            {
                SetBool("AllGuardsHear", true);
            }
        }

        private static void SetInt(string keyName, int value)
        {
            PlayerPrefs.SetInt(keyName, value);
        }

        private static int GetInt(string keyName)
        {
            return PlayerPrefs.GetInt(keyName, 0);
        }

        private static void AddInt(string keyName, int value)
        {
            PlayerPrefs.SetInt(keyName, GetInt(keyName) + value);
        }

        private static void SetBool(string boolName, bool value)
        {
            PlayerPrefs.SetInt(boolName, value ? 1 : 0);
        }

        private static bool GetBool(string boolName)
        {
            return PlayerPrefs.GetInt(boolName) == 1;
        }

        public List<Achievement> GetCompletedAchievements()
        {
            return allAchievements.Where(achievement => achievement.completed).ToList();
        }

        // This will perform a check to look for any incomplete tasks and see if they have been completed or not.
        public List<Achievement> CheckCompletedAchievements()
        {
            // Search through each of the achievements and see if it is done or not. 
            var newlyCompleted = new List<Achievement>();

            for (var i = 0; i < allAchievements.Count; i++)
            {
                Achievement ach = allAchievements[i];
                if (ach.completed) continue;

                // Add all possible achievements here and check conditions
                switch (ach.achievementPrefsCodeName)
                {
                    case "CollectAllOnce":
                        if (GetInt("CollectToothpaste") >= 1 &&
                            GetInt("CollectApple") >= 1 &&
                            GetInt("CollectPumpkin") >= 1 &&
                            GetInt("CollectMouthwash") >= 1 &&
                            GetInt("CollectToiletPaper") >= 1 &&
                            GetInt("CollectPaperTowel") >= 1 &&
                            GetInt("CollectGreenApple") >= 1 &&
                            GetInt("CollectNails") >= 1 &&
                            GetInt("CollectSoap") >= 1 &&
                            GetInt("CollectTomato") >= 1 &&
                            GetInt("CollectAlcohol") >= 1 &&
                            GetInt("CollectBlueCan") >= 1 &&
                            GetInt("CollectBread") >= 1 &&
                            GetInt("CollectCorn") >= 1 &&
                            GetInt("CollectMeat") >= 1 &&
                            GetInt("CollectGreenDrink") >= 1 &&
                            GetInt("CollectRedCan") >= 1
                           )
                        {
                            newlyCompleted.Add(ach);
                            ach.completed = true;
                        }

                        break;
                    case "RetrieveAllOnce":
                        if (GetInt("RetrieveToothpaste") >= 1 &&
                            GetInt("RetrieveApple") >= 1 &&
                            GetInt("RetrievePumpkin") >= 1 &&
                            GetInt("RetrieveMouthwash") >= 1 &&
                            GetInt("RetrieveToiletPaper") >= 1 &&
                            GetInt("RetrievePaperTowel") >= 1 &&
                            GetInt("RetrieveGreenApple") >= 1 &&
                            GetInt("RetrieveNails") >= 1 &&
                            GetInt("RetrieveSoap") >= 1 &&
                            GetInt("RetrieveTomato") >= 1 &&
                            GetInt("RetrieveAlcohol") >= 1 &&
                            GetInt("RetrieveBlueCan") >= 1 &&
                            GetInt("RetrieveBread") >= 1 &&
                            GetInt("RetrieveCorn") >= 1 &&
                            GetInt("RetrieveMeat") >= 1 &&
                            GetInt("RetrieveGreenDrink") >= 1 &&
                            GetInt("RetrieveRedCan") >= 1
                           )
                        {
                            newlyCompleted.Add(ach);
                            ach.completed = true;
                        }

                        break;
                    case "CollectAny10":
                        if (GetInt("CollectAny") >= 10)
                        {
                            newlyCompleted.Add(ach);
                            ach.completed = true;
                        }

                        break;
                    case "RetrieveAny5":
                        if (GetInt("RetrieveAny") >= 5)
                        {
                            newlyCompleted.Add(ach);
                            ach.completed = true;
                        }

                        break;
                    case "TasksWithoutCaught":
                        if (GetBool("10InARow"))
                        {
                            newlyCompleted.Add(ach);
                            ach.completed = true;
                        }

                        break;
                    case "CompleteAny5":
                        if (GetInt("TotalCompletedTasks") >= 5)
                        {
                            newlyCompleted.Add(ach);
                            ach.completed = true;
                        }

                        break;
                    case "CompleteAny25":
                        if (GetInt("TotalCompletedTasks") >= 25)
                        {
                            newlyCompleted.Add(ach);
                            ach.completed = true;
                        }

                        break;
                    case "CompleteAny100":
                        if (GetInt("TotalCompletedTasks") >= 100)
                        {
                            newlyCompleted.Add(ach);
                            ach.completed = true;
                        }

                        break;
                    case "GetCaught":
                        if (GetInt("TimesCaught") >= 1)
                        {
                            newlyCompleted.Add(ach);
                            ach.completed = true;
                        }

                        break;
                    case "GetCaught10":
                        if (GetInt("TimesCaught") >= 10)
                        {
                            newlyCompleted.Add(ach);
                            ach.completed = true;
                        }

                        break;
                    case "Meow1":
                        if (GetInt("Meow") >= 1)
                        {
                            newlyCompleted.Add(ach);
                            ach.completed = true;
                        }

                        break;
                    case "Meow10":
                        if (GetInt("Meow") >= 10)
                        {
                            newlyCompleted.Add(ach);
                            ach.completed = true;
                        }

                        break;
                    case "HearMeMeow1":
                        if (GetInt("HearMeMeow") >= 1)
                        {
                            newlyCompleted.Add(ach);
                            ach.completed = true;
                        }

                        break;
                    case "HearMeMeow10":
                        if (GetInt("HearMeMeow") >= 10)
                        {
                            newlyCompleted.Add(ach);
                            ach.completed = true;
                        }

                        break;
                    case "RetrieveNotNeeded20":
                        if (GetInt("RetrieveNotNeeded") >= 20)
                        {
                            newlyCompleted.Add(ach);
                            ach.completed = true;
                        }

                        break;
                    case "3GuardsAtOnce":
                        if (GetBool("AllGuardsHear"))
                        {
                            newlyCompleted.Add(ach);
                            ach.completed = true;
                        }

                        break;
                }

                allAchievements[i] = ach;
            }

            return newlyCompleted;
        }

        public void ResetAllAchievements()
        {
            PlayerPrefs.DeleteAll();
            for (var i = 0; i < allAchievements.Count; i++)
            {
                Achievement ach = allAchievements[i];
                ach.completed = false;
                allAchievements[i] = ach;
            }

            CheckCompletedAchievements();
        }
    }
}