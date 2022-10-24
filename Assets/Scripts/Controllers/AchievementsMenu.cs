using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class AchievementsMenu : MonoBehaviour
    {
        private AchievementController _ac;
        public GameObject achievementSlotBox;
        public GameObject achievementPanelPrefab;
        public Sprite unknownImage;
        public Sprite completedImage;

        private void Awake()
        {
            _ac = FindObjectOfType<AchievementController>();
        }

        public void ShowAchievements()
        {
            RemoveAchievementPanels();
            foreach (Achievement achievement in _ac.allAchievements)
            {
                GameObject achievementPanel = Instantiate(achievementPanelPrefab, achievementSlotBox.transform, true);
                achievementPanel.GetComponent<AchievementPanel>().achievementPrefCode =
                    achievement.achievementPrefsCodeName;
                achievementPanel.GetComponent<Image>().sprite = achievement.completed ? completedImage : unknownImage;
            }
        }

        public void RemoveAchievementPanels()
        {
            foreach (Transform child in achievementSlotBox.transform)
            {
                Destroy(child.gameObject);
            }
        }

        public void ResetAchievements()
        {
            FindObjectOfType<AchievementController>().ResetAllAchievements();
            RemoveAchievementPanels();
            ShowAchievements();
        }
    }
}