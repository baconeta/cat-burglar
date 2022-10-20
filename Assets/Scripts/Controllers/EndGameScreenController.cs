using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers
{
    public class EndGameScreenController : MonoBehaviour
    {
        [Header("Achievements")] [SerializeField]
        private TMP_Text achievementsUnlockedText;

        [SerializeField] private Color normalTextColour;
        [SerializeField] private Color highlightedTextColour;

        private ControllerManager _cm;

        public void Start()
        {
            _cm = FindObjectOfType<ControllerManager>();
        }

        public void GoToMenu()
        {
            SceneManager.LoadScene("TitleScene");
        }

        public void Replay()
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }

        public void ShowAchievements(List<Achievement> achievementsAchieved)
        {
            if (achievementsAchieved.Count <= 0) return;

            achievementsUnlockedText.text = "";
            foreach (Achievement achievement in achievementsAchieved)
            {
                achievementsUnlockedText.text = achievementsUnlockedText.text + achievement.achievementName + "\n";
            }
        }

        // public void ShowHighScores()
        // {
        //     var scores = _cm.ScoreController.GetScores();
        //     highscore1.text = scores[0].ToString();
        //     highscore2.text = scores[1].ToString();
        //     highscore3.text = scores[2].ToString();
        //     highscore4.text = scores[3].ToString();
        //     highscore5.text = scores[4].ToString();
        //     
        //     // Highlight the latest score for clarity
        //     var latest = _cm.ScoreController.LatestScore.ToString();
        //     highscore1.color = highscore1.text == latest
        //         ? highlightedTextColour
        //         : normalTextColour;
        //     highscore2.color = highscore2.text == latest
        //         ? highlightedTextColour
        //         : normalTextColour;
        //     highscore3.color = highscore3.text == latest
        //         ? highlightedTextColour
        //         : normalTextColour;
        //     highscore4.color = highscore4.text == latest
        //         ? highlightedTextColour
        //         : normalTextColour;
        //     highscore5.color = highscore5.text == latest
        //         ? highlightedTextColour
        //         : normalTextColour;
        // }
    }
}