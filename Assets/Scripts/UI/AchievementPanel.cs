using System;
using Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class AchievementPanel : MonoBehaviour
    {
        public string achievementDescription;
        public string achievementPrefCode;

        public void Select()
        {
            Achievement achievement = FindObjectOfType<AchievementController>().allAchievements
                .Find(x => x.achievementPrefsCodeName == achievementPrefCode);

            if (achievement.completed)
            {
                achievementDescription = achievement.achievementName + "\n" + achievement.subMessage;
            }
            else
            {
                achievementDescription = achievement.achievementName + "\n???";
            }

            FindObjectOfType<AchievementLabel>().AchievementText(achievementDescription);
        }
    }
}