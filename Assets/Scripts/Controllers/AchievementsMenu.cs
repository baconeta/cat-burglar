using TMPro;
using UI;
using UnityEngine;

namespace Controllers
{
    public class AchievementsMenu : MonoBehaviour
    {
        private AchievementController _ac;
        public GameObject achievementSlotBox;
        public GameObject achievementPanelPrefab;

        private void Awake()
        {
            _ac = FindObjectOfType<AchievementController>();
        }

        public void ShowAchievements()
        {
            foreach (Achievement achievement in _ac.allAchievements)
            {
                GameObject itemPanelBox = Instantiate(achievementPanelPrefab, achievementSlotBox.transform, true);
                itemPanelBox.GetComponent<AchievementPanel>().achievementPrefCode =
                    achievement.achievementPrefsCodeName;
            }
        }

        public void RemoveAchievementPanels()
        {
            // Clear all inventory images first
            foreach (Transform child in achievementSlotBox.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}