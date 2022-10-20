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

        public void Start()
        {
            // This will mark off any tasks that should be marked off as being completed prior to this session.
            CheckCompletedTasks();
        }

        public static void CompleteTask()
        {
            AddInt("TotalCompletedTasks", 1);
            AddInt("TasksWithoutCaught", 1);
            if (GetInt("TasksWithoutCaught") >= 10)
            {
                SetBool("10InARow", true);
            }
        }

        public static void CollectItem(string itemName)
        {
            AddInt("Collect" + itemName, 1);
            AddInt("CollectAny", 1);
        }

        public static void RetrieveItem(string itemName)
        {
            AddInt("Retrieve" + itemName, 1);
            AddInt("RetrieveAny", 1);
        }

        public void GetCaught()
        {
            SetInt("TasksWithoutCaught", 0);
            AddInt("TimesCaught", 1);
        }

        public static void Meow()
        {
            AddInt("Meow", 1);
        }

        public static void HearMeMeow()
        {
            AddInt("HearMeMeow", 1);
        }

        public static void RetrieveNotNeededItem(int tasksDone)
        {
            AddInt("RetrieveNotNeeded", tasksDone);
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

        public List<Achievement> GetCompletedTasks()
        {
            return allAchievements.Where(achievement => achievement.completed).ToList();
        }

        // This will perform a check to look for any incomplete tasks and see if they have been completed or not.
        // TODO either make this occur during game over one time -or live update as a player plays to inform them
        public List<Achievement> CheckCompletedTasks()
        {
            // Search through each of the achievements and see if it is done or not. 
            var toCheck = allAchievements.Where(achievement => !achievement.completed).ToList();
            var nowComplete = new List<Achievement>();

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
                            nowComplete.Add(ach);
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
                            nowComplete.Add(ach);
                            ach.completed = true;
                        }

                        break;
                    case "CollectAny10":
                        if (GetInt("CollectAny") >= 10)
                        {
                            nowComplete.Add(ach);
                            ach.completed = true;
                        }

                        break;
                    case "RetrieveAny5":
                        if (GetInt("RetrieveAny") >= 5)
                        {
                            nowComplete.Add(ach);
                            ach.completed = true;
                        }

                        break;
                    case "TasksWithoutCaught":
                        if (GetBool("10InARow"))
                        {
                            nowComplete.Add(ach);
                            ach.completed = true;
                        }

                        break;
                    case "CompleteAny5":
                        if (GetInt("TotalCompletedTasks") >= 5)
                        {
                            nowComplete.Add(ach);
                            ach.completed = true;
                        }

                        break;
                    case "CompleteAny25":
                        if (GetInt("TotalCompletedTasks") >= 25)
                        {
                            nowComplete.Add(ach);
                            ach.completed = true;
                        }

                        break;
                    case "CompleteAny100":
                        if (GetInt("TotalCompletedTasks") >= 100)
                        {
                            nowComplete.Add(ach);
                            ach.completed = true;
                        }

                        break;
                    case "GetCaught":
                        if (GetInt("TimesCaught") >= 1)
                        {
                            nowComplete.Add(ach);
                            ach.completed = true;
                        }

                        break;
                    case "GetCaught10":
                        if (GetInt("TimesCaught") >= 10)
                        {
                            nowComplete.Add(ach);
                            ach.completed = true;
                        }

                        break;
                    case "Meow1":
                        if (GetInt("Meow") >= 1)
                        {
                            nowComplete.Add(ach);
                            ach.completed = true;
                        }

                        break;
                    case "Meow10":
                        if (GetInt("Meow") >= 10)
                        {
                            nowComplete.Add(ach);
                            ach.completed = true;
                        }

                        break;
                    case "HearMeMeow1":
                        if (GetInt("HearMeMeow") >= 1)
                        {
                            nowComplete.Add(ach);
                            ach.completed = true;
                        }

                        break;
                    case "HearMeMeow10":
                        if (GetInt("HearMeMeow") >= 10)
                        {
                            nowComplete.Add(ach);
                            ach.completed = true;
                        }

                        break;
                    case "RetrieveNotNeeded20":
                        if (GetInt("RetrieveNotNeeded") >= 20)
                        {
                            nowComplete.Add(ach);
                            ach.completed = true;
                        }

                        break;
                }

                allAchievements[i] = ach;
            }

            return nowComplete;
        }
    }
}