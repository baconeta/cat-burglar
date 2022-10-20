using UnityEngine;

namespace Controllers
{
    public class AchievementController : MonoBehaviour
    {
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

        private void SetInt(string keyName, int value)
        {
            PlayerPrefs.SetInt(keyName, value);
        }

        private int GetInt(string keyName)
        {
            return PlayerPrefs.GetInt(keyName, 0);
        }

        private void AddInt(string keyName, int value)
        {
            PlayerPrefs.SetInt(keyName, GetInt(keyName) + value);
        }

        private void SetBool(string boolName, bool value)
        {
            PlayerPrefs.SetInt(boolName, value ? 1 : 0);
        }

        private bool GetBool(string boolName)
        {
            return PlayerPrefs.GetInt(boolName) == 1;
        }
    }
}