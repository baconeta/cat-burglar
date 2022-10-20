using TMPro;
using UnityEngine;

namespace Controllers
{
    public class AchievementsMenu : MonoBehaviour
    {
        private AchievementController _ac;
        public TMP_Text achievementTest;

        private void Awake()
        {
            _ac = FindObjectOfType<AchievementController>();
        }

        public void ShowAchievements()
        {
            foreach (Achievement ach in _ac.GetCompletedAchievements())
            {
                achievementTest.text = achievementTest.text + ach.achievementName + "\n";
            }
        }
    }
}