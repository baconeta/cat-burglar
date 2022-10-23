using TMPro;
using UnityEngine;

namespace UI
{
    public class AchievementLabel : MonoBehaviour
    {
        private TMP_Text _achievementHint;

        private void Start()
        {
            _achievementHint = GetComponent<TMP_Text>();
        }

        public void AchievementText(string achText)
        {
            _achievementHint.text = achText;
        }
    }
}